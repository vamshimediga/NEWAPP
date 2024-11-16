using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICookingRecipe
    {
        Task<List<CookingRecipe>> GetcookingRecipesAsync();

        Task<CookingRecipe> GetcookingRecipebyid(int id);

        Task<List<CookingRecipe>> GetSearchCookingRecipes(string name);

        Task<List<Image>> GetImages();

        Task<int> insertimagebase64(Image image);

        Task<int> insert(CookingRecipe cookingRecipe);  
    }
}
