using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using AutoMapper;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using DAL.Dtos;
namespace DAL.Data
{
    public class RecipeData :IRecipe   
    {
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public RecipeData(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<RecipeDto> GetRecipe(int id)
        {
            var recipe = await _context.AdminUsers.FindAsync(id);
            return _mapper.Map<RecipeDto>(recipe);
        }

        //public async Task<List<Recipe>> GetAllRecipes()
        //{
        //    return await _context.Recipes.ToListAsync();
        //}

        public async Task<bool> AddRecipe(RecipeDto recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRecipe(RecipeDto recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

