import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule, PageEvent, MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RecipeService } from '../../services/recipe.service';
import { Recipe } from '../../models/recipe';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import Swal from 'sweetalert2';
import { filter, Subscription } from 'rxjs';

interface DietaryTag {
  id: number;
  name: string;
  displayName: string;
  description: string;
  recipes: any[];
}

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    FormsModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatChipsModule
  ],
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit, OnDestroy {
  recipes: Recipe[] = [];
  filteredRecipes: Recipe[] = [];
  dataSource = new MatTableDataSource<Recipe>();
  displayedColumns: string[] = ['image', 'title', 'prepTimeMinutes', 'cookTimeMinutes', 'difficultyLevel', 'actions'];
  pageSize = 20;
  currentPage = 0;
  totalItems = 0;
  searchQuery = '';
  filterTag = '';
  filterMaxTime: number | null = null;
  filterDifficulty = '';
  filterQuickRecipes = false;
  loading = false;
  dietaryTags: DietaryTag[] = [];
  difficultyLevels: string[] = [];
  private routerSubscription: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private recipeService: RecipeService,
    private router: Router
  ) {
    this.routerSubscription = this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.loadRecipes();
    });
  }

  ngOnInit(): void {
    this.loadRecipes();
    this.loadDietaryTags();
    this.loadDifficultyLevels();
  }

  ngOnDestroy(): void {
    // Clean up subscription when component is destroyed
    if (this.routerSubscription) {
      this.routerSubscription.unsubscribe();
    }
  }

  loadDietaryTags(): void {
    this.recipeService.getAvailableDietaryTags().subscribe({
      next: (tags) => {
        this.dietaryTags = tags;
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'Failed to load dietary tags. Please try again.',
          confirmButtonColor: '#3085d6'
        });
        console.error('Error loading dietary tags:', error);
      }
    });
  }

  loadDifficultyLevels(): void {
    this.recipeService.getAvailableDifficultyLevels().subscribe({
      next: (levels) => {
        this.difficultyLevels = levels;
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'Failed to load difficulty levels. Please try again.',
          confirmButtonColor: '#3085d6'
        });
        console.error('Error loading difficulty levels:', error);
      }
    });
  }

  loadRecipes(): void {
    this.loading = true;
    this.recipeService.getRecipes(this.currentPage + 1, this.pageSize).subscribe({
      next: (result) => {
        this.recipes = result.items.map(recipe => ({
          ...recipe,
          dietaryTags: Array.isArray(recipe.dietaryTags)
            ? recipe.dietaryTags.map(tag => typeof tag === 'string' ? tag : tag.name)
            : []
        }));
        console.log("Processed recipes:", this.recipes);
        this.filteredRecipes = [...this.recipes];
        this.applyFilter();
        this.loading = false;
      },
      error: (error) => {
        this.loading = false;
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'Failed to load recipes. Please try again.',
          confirmButtonColor: '#3085d6'
        });
        console.error('Error loading recipes:', error);
      }
    });
  }

  applyFilter(): void {
    const query = this.searchQuery?.trim().toLowerCase() || '';
    const maxTime = this.filterMaxTime;
    const quickLimit = 30;

    this.filteredRecipes = this.recipes.filter(recipe => {
        const combinedFields = [
        recipe.title,
        recipe.description,
        recipe.difficultyLevel,
        recipe.id?.toString(),
        recipe.prepTimeMinutes?.toString(),
        recipe.cookTimeMinutes?.toString(),
        (recipe.dietaryTags || []).join(' ')
      ]
        .filter(Boolean)
        .join(' ')
        .toLowerCase();

      const totalTime = (recipe.prepTimeMinutes || 0) + (recipe.cookTimeMinutes || 0);

      const searchMatch = !query || combinedFields.includes(query);

      const tagMatch = !this.filterTag ||
        (recipe.dietaryTags && recipe.dietaryTags.some(tag =>
          typeof tag === 'string' ? tag.toLowerCase() === this.filterTag.toLowerCase() :
          tag.name.toLowerCase() === this.filterTag.toLowerCase()
        ));

      const difficultyMatch = !this.filterDifficulty ||
        (recipe.difficultyLevel && recipe.difficultyLevel.toLowerCase() === this.filterDifficulty.toLowerCase());

      const timeMatch = !maxTime || totalTime <= maxTime;

      const quickMatch = !this.filterQuickRecipes || totalTime <= quickLimit;

      return searchMatch && tagMatch && difficultyMatch && timeMatch && quickMatch;
    });

    this.dataSource.data = this.filteredRecipes;
    this.totalItems = this.filteredRecipes.length;

    if (this.paginator) {
      this.paginator.firstPage();
    }
  }

  resetFilter(): void {
    this.searchQuery = '';
    this.filterTag = '';
    this.filterMaxTime = null;
    this.filterDifficulty = '';
    this.filterQuickRecipes = false;
    this.applyFilter();
  }

  onPageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.loadRecipes();
  }

  navigateToNewRecipe(): void {
    this.router.navigate(['/recipes/new']);
  }

  deleteRecipe(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.recipeService.deleteRecipe(id).subscribe({
          next: () => {
            Swal.fire({
              icon: 'success',
              title: 'Deleted!',
              text: 'Recipe has been deleted.',
              confirmButtonColor: '#3085d6'
            });
            this.loadRecipes();
          },
          error: (error) => {
            Swal.fire({
              icon: 'error',
              title: 'Error!',
              text: 'Failed to delete recipe. Please try again.',
              confirmButtonColor: '#3085d6'
            });
            console.error('Error deleting recipe:', error);
          }
        });
      }
    });
  }

  getTagDisplayName(tag: string | { id: number; name: string; displayName: string; description: string; recipes: any[] }): string {
    if (typeof tag === 'string') return tag;
    return tag.displayName;
  }
}
