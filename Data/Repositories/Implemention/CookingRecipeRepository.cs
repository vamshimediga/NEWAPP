﻿using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class CookingRecipeRepository : BaseRepository, ICookingRecipe
    {
        public CookingRecipeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<CookingRecipe> GetcookingRecipebyid(int id)
        {
            CookingRecipe recipe = await QueryFirstOrDefaultAsync<CookingRecipe>("dbo.GetRecipeById", new { @RecipeId = id });// Retrieves the first result (or null if not found)
            return recipe;
        }

        public async Task<List<CookingRecipe>> GetcookingRecipesAsync()
        {
            List<CookingRecipe> cookingRecipes = await QueryAsync<CookingRecipe>("GetAllCookingRecipes");
            return cookingRecipes;
        }

        public async Task<List<CookingRecipe>> GetSearchCookingRecipes(string name)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RecipeName", name, DbType.String);

            // Executing the stored procedure using Dapper
            List<CookingRecipe> cookingRecipes = await QueryAsync<CookingRecipe>("SearchRecipesByName", parameters);
            return cookingRecipes;
        }
    }
}
