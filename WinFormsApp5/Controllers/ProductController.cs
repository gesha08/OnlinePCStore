using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    public class ProductController
    {
        private readonly StoreContext context;

        public ProductController(StoreContext context)
        {
            this.context = context;
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductByDetailsAsync(string name, string description, decimal price)
        {
            return await context.Products.FirstOrDefaultAsync(p => p.Name == name && p.Description == description && p.Price == price);
        }

        public async Task AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }
    }
}
