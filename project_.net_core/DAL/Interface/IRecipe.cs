using DAL.Dtos;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRecipe
    {
        Task<RecipeDto> GetRecipe(int id);
        //Task<List<Recipe>> GetAllRecipes();
        Task<bool> AddRecipe(RecipeDto recipe,int userId);
        Task<bool> UpdateRecipe(RecipeDto recipe,int userId);
        Task<bool> DeleteRecipe(int id);
    }
}
