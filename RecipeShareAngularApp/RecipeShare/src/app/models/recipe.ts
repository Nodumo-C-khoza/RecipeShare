export interface Ingredient {
    id: number;
    name: string;
    amount: string;
    unit: string;
}

export interface Recipe {
    id: number;
    title: string;
    description: string;
    instructions: string;
    prepTimeMinutes: number;
    cookTimeMinutes: number;
    servings: number;
    imageUrl: string;
    createdAt: Date;
    updatedAt: Date;
    dietaryTags: string[];
    ingredients: Ingredient[];
    difficultyLevel: string;
}

export interface CreateRecipe {
    title: string;
    description: string;
    instructions: string;
    prepTimeMinutes: number;
    cookTimeMinutes: number;
    servings: number;
    imageUrl: string;
    dietaryTags: string[];
    ingredients: CreateIngredient[];
    difficultyLevel: string;
}

export interface CreateIngredient {
    name: string;
    amount: string;
    unit: string;
}

export interface PaginatedResult<T> {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
}
