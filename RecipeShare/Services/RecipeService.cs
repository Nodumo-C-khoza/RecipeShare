using RecipeShare.Data;
using RecipeShare.Interfaces;
using RecipeShare.Models;

namespace RecipeShare.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<(
            IEnumerable<RecipeListViewModel> Recipes,
            int TotalCount
        )> GetAllRecipesAsync(int pageNumber = 1, int pageSize = 20)
        {
            var (recipes, totalCount) = await _recipeRepository.GetAllRecipesAsync(
                pageNumber,
                pageSize
            );
            return (recipes.Select(MapToRecipeListViewModel), totalCount);
        }

        public async Task<RecipeDetailViewModel> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            if (recipe == null)
                throw new KeyNotFoundException($"Recipe with ID {id} not found.");

            return MapToRecipeDetailViewModel(recipe);
        }

        public async Task<IEnumerable<RecipeListViewModel>> GetRecipesByDietaryTagAsync(string tag)
        {
            var recipes = await _recipeRepository.GetRecipesByDietaryTagAsync(tag);
            return recipes.Select(MapToRecipeListViewModel);
        }

        public async Task<RecipeDetailViewModel> CreateRecipeAsync(CreateRecipeViewModel viewModel)
        {
            // Business logic example: Validate total cooking time
            if (viewModel.PrepTimeMinutes + viewModel.CookTimeMinutes > 480) // 8 hours
            {
                throw new ArgumentException("Total cooking time cannot exceed 8 hours");
            }

            // Business logic example: Ensure dietary tags are valid
            var validTags = new HashSet<string>
            {
                "Vegetarian",
                "Vegan",
                "Gluten-Free",
                "High-Protein",
                "Low-Carb",
                "Dessert"
            };
            if (
                viewModel.DietaryTags != null
                && viewModel.DietaryTags.Any(tag => !validTags.Contains(tag))
            )
            {
                throw new ArgumentException("Invalid dietary tag provided");
            }

            var recipe = new Recipe
            {
                Title = viewModel.Title ?? string.Empty,
                Description = viewModel.Description ?? string.Empty,
                Instructions = viewModel.Instructions ?? string.Empty,
                PrepTimeMinutes = viewModel.PrepTimeMinutes,
                CookTimeMinutes = viewModel.CookTimeMinutes,
                Servings = viewModel.Servings,
                ImageUrl = viewModel.ImageUrl ?? string.Empty,
                DietaryTags = viewModel.DietaryTags ?? new List<string>(),
                Ingredients =
                    viewModel
                        .Ingredients?.Select(i => new Ingredient
                        {
                            Name = i.Name,
                            Amount = i.Amount,
                            Unit = i.Unit
                        })
                        .ToList() ?? new List<Ingredient>()
            };

            var createdRecipe = await _recipeRepository.CreateRecipeAsync(recipe);
            return MapToRecipeDetailViewModel(createdRecipe);
        }

        public async Task<RecipeDetailViewModel> UpdateRecipeAsync(
            int id,
            UpdateRecipeViewModel viewModel
        )
        {
            // Business logic example: Validate total cooking time
            if (viewModel.PrepTimeMinutes + viewModel.CookTimeMinutes > 480)
            {
                throw new ArgumentException("Total cooking time cannot exceed 8 hours");
            }

            // Business logic example: Ensure dietary tags are valid
            var validTags = new HashSet<string>
            {
                "Vegetarian",
                "Vegan",
                "Gluten-Free",
                "High-Protein",
                "Low-Carb",
                "Dessert"
            };
            if (
                viewModel.DietaryTags != null
                && viewModel.DietaryTags.Any(tag => !validTags.Contains(tag))
            )
            {
                throw new ArgumentException("Invalid dietary tag provided");
            }

            var recipe = new Recipe
            {
                Id = id,
                Title = viewModel.Title ?? string.Empty,
                Description = viewModel.Description ?? string.Empty,
                Instructions = viewModel.Instructions ?? string.Empty,
                PrepTimeMinutes = viewModel.PrepTimeMinutes,
                CookTimeMinutes = viewModel.CookTimeMinutes,
                Servings = viewModel.Servings,
                ImageUrl = viewModel.ImageUrl ?? string.Empty,
                DietaryTags = viewModel.DietaryTags ?? new List<string>(),
                Ingredients =
                    viewModel
                        .Ingredients?.Select(i => new Ingredient
                        {
                            Name = i.Name,
                            Amount = i.Amount,
                            Unit = i.Unit
                        })
                        .ToList() ?? new List<Ingredient>()
            };

            var updatedRecipe = await _recipeRepository.UpdateRecipeAsync(recipe);
            if (updatedRecipe == null)
                throw new KeyNotFoundException("Recipe not found.");

            return MapToRecipeDetailViewModel(updatedRecipe);
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            return await _recipeRepository.DeleteRecipeAsync(id);
        }

        public async Task<IEnumerable<RecipeListViewModel>> GetRecipesByTotalTimeAsync(
            int maxMinutes
        )
        {
            var recipes = await _recipeRepository.GetRecipesByTotalTimeAsync(maxMinutes);
            return recipes.Select(MapToRecipeListViewModel);
        }

        public async Task<IEnumerable<RecipeListViewModel>> GetRecipesByDifficultyLevelAsync(
            string difficultyLevel
        )
        {
            var validLevels = new HashSet<string> { "Beginner", "Intermediate", "Advanced" };
            if (!validLevels.Contains(difficultyLevel))
            {
                throw new ArgumentException(
                    "Invalid difficulty level. Must be one of: Beginner, Intermediate, Advanced"
                );
            }

            var recipes = await _recipeRepository.GetRecipesByDifficultyLevelAsync(difficultyLevel);
            return recipes.Select(MapToRecipeListViewModel);
        }

        public async Task<IEnumerable<RecipeListViewModel>> GetQuickRecipesAsync(
            int maxMinutes = 30
        )
        {
            var recipes = await _recipeRepository.GetQuickRecipesAsync(maxMinutes);
            return recipes.Select(MapToRecipeListViewModel);
        }

        public async Task<IEnumerable<RecipeListViewModel>> GetRecipesByIngredientsAsync(
            IEnumerable<string> ingredients,
            bool matchAll = false
        )
        {
            if (ingredients == null || !ingredients.Any())
            {
                throw new ArgumentException("At least one ingredient must be specified");
            }

            var recipes = await _recipeRepository.GetRecipesByIngredientsAsync(
                ingredients,
                matchAll
            );
            return recipes.Select(MapToRecipeListViewModel);
        }

        private static RecipeListViewModel MapToRecipeListViewModel(Recipe recipe)
        {
            return new RecipeListViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                PrepTimeMinutes = recipe.PrepTimeMinutes,
                CookTimeMinutes = recipe.CookTimeMinutes,
                ImageUrl = recipe.ImageUrl,
                DietaryTags = recipe.DietaryTags,
                DifficultyLevel = recipe.DifficultyLevel
            };
        }

        private static RecipeDetailViewModel MapToRecipeDetailViewModel(Recipe recipe)
        {
            return new RecipeDetailViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                PrepTimeMinutes = recipe.PrepTimeMinutes,
                CookTimeMinutes = recipe.CookTimeMinutes,
                Servings = recipe.Servings,
                ImageUrl = recipe.ImageUrl,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                DietaryTags = recipe.DietaryTags,
                DifficultyLevel = recipe.DifficultyLevel,
                Ingredients = recipe
                    .Ingredients?.Select(i => new IngredientViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Amount = i.Amount,
                        Unit = i.Unit
                    })
                    .ToList()
            };
        }
    }
}
