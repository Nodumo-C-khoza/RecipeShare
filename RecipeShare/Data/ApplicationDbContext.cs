using Microsoft.EntityFrameworkCore;

namespace RecipeShare.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Recipe entity
            modelBuilder.Entity<Recipe>(entity =>
            {
                // Add indexes for commonly queried fields
                entity.HasIndex(r => r.Title);
                entity.HasIndex(r => r.DifficultyLevel);
                entity.HasIndex(r => r.CreatedAt);

                // Configure dietary tags conversion
                entity
                    .Property(r => r.DietaryTags)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            });

            // Configure Ingredient entity
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasIndex(i => i.Name);
                entity.HasIndex(i => i.RecipeId);
            });

            // Seed data with real images from TheMealDB
            modelBuilder
                .Entity<Recipe>()
                .HasData(
                    new Recipe
                    {
                        Id = 1,
                        Title = "Classic Margherita Pizza",
                        Description = "A simple and delicious traditional Italian pizza",
                        Instructions =
                            "1. Preheat oven to 450°F\n2. Roll out pizza dough\n3. Add sauce and toppings\n4. Bake for 12-15 minutes",
                        PrepTimeMinutes = 20,
                        CookTimeMinutes = 15,
                        Servings = 4,
                        ImageUrl =
                            "https://www.themealdb.com/images/media/meals/x0lk931587671540.jpg",
                        CreatedAt = DateTime.UtcNow,
                        DietaryTags = new List<string> { "Vegetarian" }
                    },
                    new Recipe
                    {
                        Id = 2,
                        Title = "Chicken Stir Fry",
                        Description = "Quick and healthy Asian-inspired dish",
                        Instructions =
                            "1. Cut chicken into strips\n2. Stir fry vegetables\n3. Add chicken and sauce\n4. Serve over rice",
                        PrepTimeMinutes = 15,
                        CookTimeMinutes = 20,
                        Servings = 4,
                        ImageUrl = "https://www.themealdb.com/images/media/meals/1520084413.jpg",
                        CreatedAt = DateTime.UtcNow,
                        DietaryTags = new List<string> { "High-Protein", "Low-Carb" }
                    },
                    new Recipe
                    {
                        Id = 3,
                        Title = "Chocolate Chip Cookies",
                        Description = "Classic homemade cookies",
                        Instructions =
                            "1. Mix dry ingredients\n2. Cream butter and sugar\n3. Add chocolate chips\n4. Bake at 350°F for 10-12 minutes",
                        PrepTimeMinutes = 15,
                        CookTimeMinutes = 12,
                        Servings = 24,
                        ImageUrl = "https://www.themealdb.com/images/media/meals/1540441275.jpg",
                        CreatedAt = DateTime.UtcNow,
                        DietaryTags = new List<string> { "Vegetarian", "Dessert" }
                    }
                );
        }
    }
}
