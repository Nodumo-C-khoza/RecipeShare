using Microsoft.AspNetCore.Mvc;
using RecipeShare.Interfaces;
using RecipeShare.Models;

namespace RecipeShare.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeService recipeService, ILogger<RecipesController> logger)
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)] // Cache for 1 minute
        public async Task<ActionResult<PaginatedResult<RecipeListViewModel>>> GetRecipes(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20
        )
        {
            try
            {
                if (pageNumber < 1)
                    pageNumber = 1;
                if (pageSize < 1 || pageSize > 100)
                    pageSize = 20;

                var result = await _recipeService.GetAllRecipesAsync(pageNumber, pageSize);

                var paginatedResult = new PaginatedResult<RecipeListViewModel>
                {
                    Items = result.Recipes,
                    TotalCount = result.TotalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize)
                };

                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recipes");
                return StatusCode(500, "An error occurred while retrieving recipes");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailViewModel>> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpGet("tag/{tag}")]
        public async Task<ActionResult<IEnumerable<RecipeListViewModel>>> GetRecipesByTag(
            string tag
        )
        {
            var recipes = await _recipeService.GetRecipesByDietaryTagAsync(tag);
            return Ok(recipes);
        }

        [HttpGet("time/{maxMinutes}")]
        public async Task<ActionResult<IEnumerable<RecipeListViewModel>>> GetRecipesByTime(
            int maxMinutes
        )
        {
            var recipes = await _recipeService.GetRecipesByTotalTimeAsync(maxMinutes);
            return Ok(recipes);
        }

        [HttpGet("difficulty/{level}")]
        public async Task<ActionResult<IEnumerable<RecipeListViewModel>>> GetRecipesByDifficulty(
            string level
        )
        {
            try
            {
                var recipes = await _recipeService.GetRecipesByDifficultyLevelAsync(level);
                return Ok(recipes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("quick")]
        public async Task<ActionResult<IEnumerable<RecipeListViewModel>>> GetQuickRecipes(
            [FromQuery] int maxMinutes = 30
        )
        {
            var recipes = await _recipeService.GetQuickRecipesAsync(maxMinutes);
            return Ok(recipes);
        }

        [HttpGet("ingredients")]
        public async Task<ActionResult<IEnumerable<RecipeListViewModel>>> GetRecipesByIngredients(
            [FromQuery] string[] ingredients,
            [FromQuery] bool matchAll = false
        )
        {
            try
            {
                var recipes = await _recipeService.GetRecipesByIngredientsAsync(
                    ingredients,
                    matchAll
                );
                return Ok(recipes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDetailViewModel>> CreateRecipe(
            CreateRecipeViewModel viewModel
        )
        {
            try
            {
                var recipe = await _recipeService.CreateRecipeAsync(viewModel);
                return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeDetailViewModel>> UpdateRecipe(
            int id,
            UpdateRecipeViewModel viewModel
        )
        {
            try
            {
                var recipe = await _recipeService.UpdateRecipeAsync(id, viewModel);
                if (recipe == null)
                {
                    return NotFound();
                }
                return Ok(recipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var result = await _recipeService.DeleteRecipeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
