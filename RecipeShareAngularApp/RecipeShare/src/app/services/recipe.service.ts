import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recipe, CreateRecipe, PaginatedResult } from '../models/recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private apiUrl = 'api/recipes';

  constructor(private http: HttpClient) { }

  getRecipes(pageNumber: number = 1, pageSize: number = 20): Observable<PaginatedResult<Recipe>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<PaginatedResult<Recipe>>(`${this.apiUrl}/GetRecipes`, { params });
  }

  getRecipe(id: number): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.apiUrl}/GetRecipe/${id}`);
  }

  getRecipesByTag(tag: string): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.apiUrl}/GetRecipesByTag/${tag}`);
  }

  getRecipesByTime(maxMinutes: number): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.apiUrl}/GetRecipesByTime/${maxMinutes}`);
  }

  getRecipesByDifficulty(level: string): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.apiUrl}/GetRecipesByDifficulty/${level}`);
  }

  getQuickRecipes(maxMinutes: number = 30): Observable<Recipe[]> {
    const params = new HttpParams().set('maxMinutes', maxMinutes.toString());
    return this.http.get<Recipe[]>(`${this.apiUrl}/GetQuickRecipes`, { params });
  }

  getRecipesByIngredients(ingredients: string[], matchAll: boolean = false): Observable<Recipe[]> {
    const params = new HttpParams()
      .set('ingredients', ingredients.join(','))
      .set('matchAll', matchAll.toString());
    return this.http.get<Recipe[]>(`${this.apiUrl}/GetRecipesByIngredients`, { params });
  }

  createRecipe(recipe: CreateRecipe): Observable<Recipe> {
    return this.http.post<Recipe>(`${this.apiUrl}/CreateRecipe`, recipe);
  }

  updateRecipe(id: number, recipe: CreateRecipe): Observable<Recipe> {
    return this.http.put<Recipe>(`${this.apiUrl}/UpdateRecipe/${id}`, recipe);
  }

  deleteRecipe(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteRecipe/${id}`);
  }
}
