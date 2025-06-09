using EasyCaching.Core;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Data;
using RecipeShare.Interfaces;

namespace RecipeShare.Services
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEasyCachingProvider _cache;
        private const int DefaultPageSize = 20;
        private const string RecipeListCacheKey = "recipe:list:{0}:{1}"; // {pageNumber}_{pageSize}
        private const string RecipeByIdCacheKey = "recipe:{0}"; // {id}
        private static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(5);

        public RecipeRepository(ApplicationDbContext context, IEasyCachingProvider cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<(IEnumerable<Recipe> Recipes, int TotalCount)> GetAllRecipesAsync(
            int pageNumber = 1,
            int pageSize = DefaultPageSize
        )
        {
            var cacheKey = string.Format(RecipeListCacheKey, pageNumber, pageSize);

            var cacheValue = await _cache.GetAsync<(IEnumerable<Recipe>, int)>(cacheKey);
            if (cacheValue.HasValue)
            {
                return cacheValue.Value;
            }

            var totalCount = await _context.Recipes.CountAsync();

            var recipes = await _context
                .Recipes.AsNoTracking()
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new Recipe
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    PrepTimeMinutes = r.PrepTimeMinutes,
                    CookTimeMinutes = r.CookTimeMinutes,
                    ImageUrl = r.ImageUrl,
                    DietaryTags = r.DietaryTags,
                    DifficultyLevel = r.DifficultyLevel,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .ToListAsync();

            var result = (recipes, totalCount);
            await _cache.SetAsync(cacheKey, result, DefaultCacheDuration);
            return result;
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var cacheKey = string.Format(RecipeByIdCacheKey, id);

            var cacheValue = await _cache.GetAsync<Recipe>(cacheKey);
            if (cacheValue.HasValue)
            {
                return cacheValue.Value;
            }

            var recipe = await _context
                .Recipes.AsNoTracking()
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe != null)
            {
                await _cache.SetAsync(cacheKey, recipe, DefaultCacheDuration);
            }

            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByDietaryTagAsync(string tag)
        {
            return await _context
                .Recipes.Include(r => r.Ingredients)
                .Where(r => r.DietaryTags.Contains(tag))
                .ToListAsync();
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            recipe.CreatedAt = DateTime.UtcNow;
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            // Invalidate all recipe list caches
            await _cache.RemoveByPrefixAsync("recipe:list:");

            return recipe;
        }

        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            var existingRecipe = await _context
                .Recipes.Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            if (existingRecipe == null)
                return null;

            existingRecipe.Title = recipe.Title;
            existingRecipe.Description = recipe.Description;
            existingRecipe.Instructions = recipe.Instructions;
            existingRecipe.PrepTimeMinutes = recipe.PrepTimeMinutes;
            existingRecipe.CookTimeMinutes = recipe.CookTimeMinutes;
            existingRecipe.Servings = recipe.Servings;
            existingRecipe.ImageUrl = recipe.ImageUrl;
            existingRecipe.DietaryTags = recipe.DietaryTags;
            existingRecipe.UpdatedAt = DateTime.UtcNow;

            // Update ingredients
            _context.Ingredients.RemoveRange(existingRecipe.Ingredients);
            existingRecipe.Ingredients = recipe.Ingredients;

            await _context.SaveChangesAsync();

            // Invalidate both recipe-specific and list caches
            await _cache.RemoveAsync(string.Format(RecipeByIdCacheKey, recipe.Id));
            await _cache.RemoveByPrefixAsync("recipe:list:");

            return existingRecipe;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            // Invalidate both recipe-specific and list caches
            await _cache.RemoveAsync(string.Format(RecipeByIdCacheKey, id));
            await _cache.RemoveByPrefixAsync("recipe:list:");

            return true;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByTotalTimeAsync(int maxMinutes)
        {
            return await _context
                .Recipes.Include(r => r.Ingredients)
                .Where(r => r.PrepTimeMinutes + r.CookTimeMinutes <= maxMinutes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByDifficultyLevelAsync(
            string difficultyLevel
        )
        {
            return await _context
                .Recipes.Include(r => r.Ingredients)
                .Where(r => r.DifficultyLevel == difficultyLevel)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> GetQuickRecipesAsync(int maxMinutes = 30)
        {
            return await _context
                .Recipes.Include(r => r.Ingredients)
                .Where(r => r.PrepTimeMinutes + r.CookTimeMinutes <= maxMinutes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByIngredientsAsync(
            IEnumerable<string> ingredients,
            bool matchAll = false
        )
        {
            var normalizedIngredients = ingredients.Select(i => i.ToLower().Trim()).ToList();

            var query = _context.Recipes.Include(r => r.Ingredients).AsQueryable();

            if (matchAll)
            {
                // Match all ingredients (AND condition)
                foreach (var ingredient in normalizedIngredients)
                {
                    query = query.Where(r =>
                        r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredient))
                    );
                }
            }
            else
            {
                // Match any ingredient (OR condition)
                query = query.Where(r =>
                    r.Ingredients.Any(i =>
                        normalizedIngredients.Any(searchIngredient =>
                            i.Name.ToLower().Contains(searchIngredient)
                        )
                    )
                );
            }

            return await query.ToListAsync();
        }
    }
}
