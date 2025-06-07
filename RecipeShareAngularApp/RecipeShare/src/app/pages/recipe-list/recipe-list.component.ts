import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { RecipeService } from '../../services/recipe.service';
import { Recipe, PaginatedResult } from '../../models/recipe';

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
    MatPaginatorModule
  ],
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {
  recipes: Recipe[] = [];
  displayedColumns: string[] = ['title', 'prepTimeMinutes', 'cookTimeMinutes', 'difficultyLevel', 'actions'];
  totalItems = 0;
  pageSize = 20;
  pageIndex = 0;

  constructor(private recipeService: RecipeService) {}

  ngOnInit(): void {
    this.loadRecipes();
  }

  loadRecipes(): void {
    this.recipeService.getRecipes(this.pageIndex + 1, this.pageSize).subscribe({
      next: (result: PaginatedResult<Recipe>) => {
        this.recipes = result.items;
        this.totalItems = result.totalCount;
      },
      error: (error) => console.error('Error loading recipes:', error)
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadRecipes();
  }

  deleteRecipe(id: number): void {
    if (confirm('Are you sure you want to delete this recipe?')) {
      this.recipeService.deleteRecipe(id).subscribe({
        next: () => this.loadRecipes(),
        error: (error) => console.error('Error deleting recipe:', error)
      });
    }
  }
}
