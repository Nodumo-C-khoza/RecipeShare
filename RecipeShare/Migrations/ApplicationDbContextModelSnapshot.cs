﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeShare.Data;

#nullable disable

namespace RecipeShare.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeShare.Data.DietaryTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("DietaryTags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Contains no meat or fish",
                            DisplayName = "Vegetarian",
                            Name = "Vegetarian"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Contains no animal products",
                            DisplayName = "Vegan",
                            Name = "Vegan"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Contains no gluten",
                            DisplayName = "Gluten-Free",
                            Name = "GlutenFree"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Contains no dairy products",
                            DisplayName = "Dairy-Free",
                            Name = "DairyFree"
                        },
                        new
                        {
                            Id = 5,
                            Description = "High in protein content",
                            DisplayName = "High Protein",
                            Name = "HighProtein"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Low in carbohydrates",
                            DisplayName = "Low-Carb",
                            Name = "LowCarb"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Contains no nuts",
                            DisplayName = "Nut-Free",
                            Name = "NutFree"
                        });
                });

            modelBuilder.Entity("RecipeShare.Data.DifficultyLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("DifficultyLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Simple recipes with basic techniques and few ingredients",
                            DisplayName = "Beginner (Easy)",
                            Name = "Beginner"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Recipes requiring some cooking experience and moderate techniques",
                            DisplayName = "Intermediate (Medium)",
                            Name = "Intermediate"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Complex recipes requiring advanced techniques and experience",
                            DisplayName = "Advanced (Hard)",
                            Name = "Advanced"
                        });
                });

            modelBuilder.Entity("RecipeShare.Data.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = "1",
                            Name = "Pizza Dough",
                            RecipeId = 1,
                            Unit = "ball"
                        },
                        new
                        {
                            Id = 2,
                            Amount = "1/2",
                            Name = "Tomato Sauce",
                            RecipeId = 1,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 3,
                            Amount = "8",
                            Name = "Fresh Mozzarella",
                            RecipeId = 1,
                            Unit = "oz"
                        },
                        new
                        {
                            Id = 4,
                            Amount = "1/4",
                            Name = "Fresh Basil",
                            RecipeId = 1,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 5,
                            Amount = "2",
                            Name = "Olive Oil",
                            RecipeId = 1,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 6,
                            Amount = "1",
                            Name = "Chicken Breast",
                            RecipeId = 2,
                            Unit = "lb"
                        },
                        new
                        {
                            Id = 7,
                            Amount = "2",
                            Name = "Broccoli",
                            RecipeId = 2,
                            Unit = "cups"
                        },
                        new
                        {
                            Id = 8,
                            Amount = "2",
                            Name = "Bell Peppers",
                            RecipeId = 2,
                            Unit = "medium"
                        },
                        new
                        {
                            Id = 9,
                            Amount = "1/4",
                            Name = "Soy Sauce",
                            RecipeId = 2,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 10,
                            Amount = "1",
                            Name = "Ginger",
                            RecipeId = 2,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 11,
                            Amount = "2",
                            Name = "All-Purpose Flour",
                            RecipeId = 3,
                            Unit = "cups"
                        },
                        new
                        {
                            Id = 12,
                            Amount = "1",
                            Name = "Butter",
                            RecipeId = 3,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 13,
                            Amount = "3/4",
                            Name = "Brown Sugar",
                            RecipeId = 3,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 14,
                            Amount = "2",
                            Name = "Chocolate Chips",
                            RecipeId = 3,
                            Unit = "cups"
                        },
                        new
                        {
                            Id = 15,
                            Amount = "1",
                            Name = "Vanilla Extract",
                            RecipeId = 3,
                            Unit = "tsp"
                        },
                        new
                        {
                            Id = 51,
                            Amount = "1",
                            Name = "Quinoa",
                            RecipeId = 4,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 52,
                            Amount = "1",
                            Name = "Sweet Potato",
                            RecipeId = 4,
                            Unit = "medium"
                        },
                        new
                        {
                            Id = 53,
                            Amount = "1",
                            Name = "Chickpeas",
                            RecipeId = 4,
                            Unit = "can"
                        },
                        new
                        {
                            Id = 54,
                            Amount = "2",
                            Name = "Kale",
                            RecipeId = 4,
                            Unit = "cups"
                        },
                        new
                        {
                            Id = 55,
                            Amount = "2",
                            Name = "Tahini",
                            RecipeId = 4,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 56,
                            Amount = "2",
                            Name = "Beef Fillet",
                            RecipeId = 5,
                            Unit = "lbs"
                        },
                        new
                        {
                            Id = 57,
                            Amount = "1",
                            Name = "Mushrooms",
                            RecipeId = 5,
                            Unit = "lb"
                        },
                        new
                        {
                            Id = 58,
                            Amount = "1",
                            Name = "Puff Pastry",
                            RecipeId = 5,
                            Unit = "sheet"
                        },
                        new
                        {
                            Id = 59,
                            Amount = "8",
                            Name = "Prosciutto",
                            RecipeId = 5,
                            Unit = "slices"
                        },
                        new
                        {
                            Id = 60,
                            Amount = "2",
                            Name = "Dijon Mustard",
                            RecipeId = 5,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 61,
                            Amount = "8",
                            Name = "Rice Noodles",
                            RecipeId = 6,
                            Unit = "oz"
                        },
                        new
                        {
                            Id = 62,
                            Amount = "14",
                            Name = "Tofu",
                            RecipeId = 6,
                            Unit = "oz"
                        },
                        new
                        {
                            Id = 63,
                            Amount = "2",
                            Name = "Bean Sprouts",
                            RecipeId = 6,
                            Unit = "cups"
                        },
                        new
                        {
                            Id = 64,
                            Amount = "1/2",
                            Name = "Peanuts",
                            RecipeId = 6,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 65,
                            Amount = "2",
                            Name = "Tamarind Paste",
                            RecipeId = 6,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 66,
                            Amount = "2",
                            Name = "Almond Flour",
                            RecipeId = 7,
                            Unit = "cups"
                        },
                        new
                        {
                            Id = 67,
                            Amount = "3/4",
                            Name = "Cocoa Powder",
                            RecipeId = 7,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 68,
                            Amount = "1",
                            Name = "Coconut Sugar",
                            RecipeId = 7,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 69,
                            Amount = "1/2",
                            Name = "Coconut Oil",
                            RecipeId = 7,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 70,
                            Amount = "1",
                            Name = "Almond Milk",
                            RecipeId = 7,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 71,
                            Amount = "4",
                            Name = "Salmon Fillets",
                            RecipeId = 8,
                            Unit = "pieces"
                        },
                        new
                        {
                            Id = 72,
                            Amount = "1",
                            Name = "Lemon",
                            RecipeId = 8,
                            Unit = "whole"
                        },
                        new
                        {
                            Id = 73,
                            Amount = "2",
                            Name = "Olive Oil",
                            RecipeId = 8,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 74,
                            Amount = "4",
                            Name = "Garlic",
                            RecipeId = 8,
                            Unit = "cloves"
                        },
                        new
                        {
                            Id = 75,
                            Amount = "1/4",
                            Name = "Herbs",
                            RecipeId = 8,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 76,
                            Amount = "1",
                            Name = "Quinoa",
                            RecipeId = 9,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 77,
                            Amount = "1",
                            Name = "Avocado",
                            RecipeId = 9,
                            Unit = "whole"
                        },
                        new
                        {
                            Id = 78,
                            Amount = "1",
                            Name = "Black Beans",
                            RecipeId = 9,
                            Unit = "can"
                        },
                        new
                        {
                            Id = 79,
                            Amount = "1",
                            Name = "Corn",
                            RecipeId = 9,
                            Unit = "cup"
                        },
                        new
                        {
                            Id = 80,
                            Amount = "1",
                            Name = "Lime",
                            RecipeId = 9,
                            Unit = "whole"
                        },
                        new
                        {
                            Id = 81,
                            Amount = "1",
                            Name = "Cauliflower",
                            RecipeId = 10,
                            Unit = "head"
                        },
                        new
                        {
                            Id = 82,
                            Amount = "1",
                            Name = "Onion",
                            RecipeId = 10,
                            Unit = "medium"
                        },
                        new
                        {
                            Id = 83,
                            Amount = "2",
                            Name = "Garlic",
                            RecipeId = 10,
                            Unit = "cloves"
                        },
                        new
                        {
                            Id = 84,
                            Amount = "2",
                            Name = "Olive Oil",
                            RecipeId = 10,
                            Unit = "tbsp"
                        },
                        new
                        {
                            Id = 85,
                            Amount = "1/4",
                            Name = "Herbs",
                            RecipeId = 10,
                            Unit = "cup"
                        });
                });

            modelBuilder.Entity("RecipeShare.Data.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DifficultyLevelId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrepTimeMinutes")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DifficultyLevelId");

                    b.HasIndex("Title");

                    b.HasIndex("PrepTimeMinutes", "CookTimeMinutes");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CookTimeMinutes = 15,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A simple and delicious traditional Italian pizza",
                            DifficultyLevelId = 1,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/x0lk931587671540.jpg",
                            Instructions = "1. Preheat oven to 450°F\n2. Roll out pizza dough\n3. Add sauce and toppings\n4. Bake for 12-15 minutes",
                            PrepTimeMinutes = 20,
                            Servings = 4,
                            Title = "Classic Margherita Pizza"
                        },
                        new
                        {
                            Id = 2,
                            CookTimeMinutes = 20,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Quick and healthy Asian-inspired dish",
                            DifficultyLevelId = 2,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/1520084413.jpg",
                            Instructions = "1. Cut chicken into strips\n2. Stir fry vegetables\n3. Add chicken and sauce\n4. Serve over rice",
                            PrepTimeMinutes = 15,
                            Servings = 4,
                            Title = "Chicken Stir Fry"
                        },
                        new
                        {
                            Id = 3,
                            CookTimeMinutes = 12,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Classic homemade cookies",
                            DifficultyLevelId = 1,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/wyrqqq1468233628.jpg",
                            Instructions = "1. Mix dry ingredients\n2. Cream butter and sugar\n3. Add chocolate chips\n4. Bake at 350°F for 10-12 minutes",
                            PrepTimeMinutes = 15,
                            Servings = 24,
                            Title = "Chocolate Chip Cookies"
                        },
                        new
                        {
                            Id = 4,
                            CookTimeMinutes = 30,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A nourishing bowl packed with vegetables and protein",
                            DifficultyLevelId = 1,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/1550441882.jpg",
                            Instructions = "1. Cook quinoa according to package instructions\n2. Roast vegetables with olive oil and seasonings\n3. Prepare tahini dressing\n4. Assemble bowl with all components",
                            PrepTimeMinutes = 20,
                            Servings = 2,
                            Title = "Vegetarian Buddha Bowl"
                        },
                        new
                        {
                            Id = 5,
                            CookTimeMinutes = 40,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Classic British dish with beef fillet wrapped in puff pastry",
                            DifficultyLevelId = 3,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/vvusxs1483907034.jpg",
                            Instructions = "1. Sear beef fillet\n2. Prepare mushroom duxelles\n3. Wrap in prosciutto and puff pastry\n4. Bake until golden brown",
                            PrepTimeMinutes = 45,
                            Servings = 4,
                            Title = "Beef Wellington"
                        },
                        new
                        {
                            Id = 6,
                            CookTimeMinutes = 15,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Plant-based version of the classic Thai noodle dish",
                            DifficultyLevelId = 2,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/vtxyxv1483567157.jpg",
                            Instructions = "1. Soak rice noodles\n2. Prepare tofu and vegetables\n3. Make pad thai sauce\n4. Stir fry all components together",
                            PrepTimeMinutes = 25,
                            Servings = 4,
                            Title = "Vegan Pad Thai"
                        },
                        new
                        {
                            Id = 11,
                            CookTimeMinutes = 35,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Rich and moist chocolate cake without gluten",
                            DifficultyLevelId = 2,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/1550441275.jpg",
                            Instructions = "1. Mix dry ingredients\n2. Combine wet ingredients\n3. Bake at 350°F\n4. Prepare chocolate ganache",
                            PrepTimeMinutes = 20,
                            Servings = 12,
                            Title = "Gluten-Free Chocolate Cake"
                        },
                        new
                        {
                            Id = 8,
                            CookTimeMinutes = 20,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Healthy salmon with Mediterranean flavors",
                            DifficultyLevelId = 2,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/1550441275.jpg",
                            Instructions = "1. Marinate salmon\n2. Prepare vegetable medley\n3. Grill salmon\n4. Serve with vegetables",
                            PrepTimeMinutes = 15,
                            Servings = 4,
                            Title = "Mediterranean Grilled Salmon"
                        },
                        new
                        {
                            Id = 9,
                            CookTimeMinutes = 25,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Protein-packed bowl with quinoa and vegetables",
                            DifficultyLevelId = 1,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/1550441882.jpg",
                            Instructions = "1. Cook quinoa\n2. Roast vegetables\n3. Prepare tahini dressing\n4. Assemble bowl",
                            PrepTimeMinutes = 20,
                            Servings = 2,
                            Title = "Quinoa Buddha Bowl"
                        },
                        new
                        {
                            Id = 10,
                            CookTimeMinutes = 15,
                            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Healthy alternative to traditional rice",
                            DifficultyLevelId = 1,
                            ImageUrl = "https://www.themealdb.com/images/media/meals/1550441882.jpg",
                            Instructions = "1. Process cauliflower\n2. Cook with seasonings\n3. Add vegetables\n4. Serve hot",
                            PrepTimeMinutes = 10,
                            Servings = 4,
                            Title = "Low-Carb Cauliflower Rice"
                        });
                });

            modelBuilder.Entity("RecipeShare.Data.RecipeDietaryTag", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("DietaryTagId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "DietaryTagId");

                    b.HasIndex("DietaryTagId");

                    b.ToTable("RecipeDietaryTags");

                    b.HasData(
                        new
                        {
                            RecipeId = 1,
                            DietaryTagId = 1
                        },
                        new
                        {
                            RecipeId = 1,
                            DietaryTagId = 4
                        },
                        new
                        {
                            RecipeId = 2,
                            DietaryTagId = 5
                        },
                        new
                        {
                            RecipeId = 3,
                            DietaryTagId = 1
                        },
                        new
                        {
                            RecipeId = 4,
                            DietaryTagId = 1
                        },
                        new
                        {
                            RecipeId = 4,
                            DietaryTagId = 2
                        },
                        new
                        {
                            RecipeId = 4,
                            DietaryTagId = 3
                        },
                        new
                        {
                            RecipeId = 5,
                            DietaryTagId = 5
                        },
                        new
                        {
                            RecipeId = 6,
                            DietaryTagId = 2
                        },
                        new
                        {
                            RecipeId = 6,
                            DietaryTagId = 3
                        },
                        new
                        {
                            RecipeId = 7,
                            DietaryTagId = 3
                        },
                        new
                        {
                            RecipeId = 7,
                            DietaryTagId = 4
                        },
                        new
                        {
                            RecipeId = 8,
                            DietaryTagId = 5
                        },
                        new
                        {
                            RecipeId = 8,
                            DietaryTagId = 6
                        },
                        new
                        {
                            RecipeId = 9,
                            DietaryTagId = 1
                        },
                        new
                        {
                            RecipeId = 9,
                            DietaryTagId = 2
                        },
                        new
                        {
                            RecipeId = 9,
                            DietaryTagId = 3
                        },
                        new
                        {
                            RecipeId = 10,
                            DietaryTagId = 1
                        },
                        new
                        {
                            RecipeId = 10,
                            DietaryTagId = 6
                        });
                });

            modelBuilder.Entity("RecipeShare.Data.Ingredient", b =>
                {
                    b.HasOne("RecipeShare.Data.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeShare.Data.Recipe", b =>
                {
                    b.HasOne("RecipeShare.Data.DifficultyLevel", "DifficultyLevel")
                        .WithMany()
                        .HasForeignKey("DifficultyLevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DifficultyLevel");
                });

            modelBuilder.Entity("RecipeShare.Data.RecipeDietaryTag", b =>
                {
                    b.HasOne("RecipeShare.Data.DietaryTag", "DietaryTag")
                        .WithMany()
                        .HasForeignKey("DietaryTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeShare.Data.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DietaryTag");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeShare.Data.Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
