using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    public class CategoryController
    {
        private readonly StoreContext context;

        public CategoryController(StoreContext context)
        {
            this.context = context;
        }
        public CategoryController()
        {
            context = new StoreContext();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
    }
}
