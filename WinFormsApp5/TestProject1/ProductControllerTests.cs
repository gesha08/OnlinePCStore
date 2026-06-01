using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Controllers;
using System.Threading.Tasks;
using System.Linq;

namespace TestProject1
{
    public class ProductControllerTests
    {
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + System.Guid.NewGuid().ToString())
                .Options;

            using (var context = new StoreContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new StoreContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public async Task AddProductAsync_ShouldAddProductToDatabase()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new ProductController(context);
                var category = new Category { Name = "Graphics Cards" };
                context.Categories.Add(category);
                await context.SaveChangesAsync();

                var product = new Product
                {
                    Name = "GeForce RTX 4090",
                    Description = "Top-tier GPU",
                    Price = 1599.99m,
                    Stock = 10,
                    CategoryId = category.Id
                };

                await controller.AddProductAsync(product);
            }

            using (var context = new StoreContext(_options))
            {
                Assert.AreEqual(1, await context.Products.CountAsync());
                var savedProduct = await context.Products.Include(p => p.Category).FirstAsync();
                Assert.AreEqual("GeForce RTX 4090", savedProduct.Name);
                Assert.AreEqual("Graphics Cards", savedProduct.Category.Name);
            }
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            using (var context = new StoreContext(_options))
            {
                var category = new Category { Name = "Storage" };
                context.Categories.Add(category);
                context.Products.Add(new Product { Name = "1TB NVMe SSD", Description = "A very fast ssd.", Price = 100m, Stock = 50, Category = category });
                context.Products.Add(new Product { Name = "4TB HDD", Description = "A very slow hdd.", Price = 80m, Stock = 30, Category = category });
                await context.SaveChangesAsync();
            }

            System.Collections.Generic.List<Product> products;
            using (var context = new StoreContext(_options))
            {
                var controller = new ProductController(context);
                products = await controller.GetAllProductsAsync();
            }

            Assert.IsNotNull(products);
            Assert.AreEqual(2, products.Count);
        }

        [Test]
        public async Task UpdateProductAsync_ShouldUpdateProductDetails()
        {
            var category = new Category { Name = "Memory" };
            var product = new Product { Name = "16GB DDR4 RAM", Description = "Some ram.", Price = 60m, Stock = 20, Category = category };
            using (var context = new StoreContext(_options))
            {
                context.Categories.Add(category);
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }

            product.Price = 55.50m;
            product.Stock = 15;
            using (var context = new StoreContext(_options))
            {
                var controller = new ProductController(context);
                await controller.UpdateProductAsync(product);
            }

            using (var context = new StoreContext(_options))
            {
                var updatedProduct = await context.Products.FindAsync(product.Id);
                Assert.IsNotNull(updatedProduct);
                Assert.AreEqual(55.50m, updatedProduct.Price);
                Assert.AreEqual(15, updatedProduct.Stock);
            }
        }

        [Test]
        public async Task DeleteProductAsync_ShouldRemoveProductFromDatabase()
        {
            var category = new Category { Name = "Cases" };
            var product = new Product { Name = "Mid-Tower Case", Description = "A case.", Price = 90m, Stock = 5, Category = category };
            using (var context = new StoreContext(_options))
            {
                context.Categories.Add(category);
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new ProductController(context);
                await controller.DeleteProductAsync(product.Id);
            }

            using (var context = new StoreContext(_options))
            {
                Assert.AreEqual(0, await context.Products.CountAsync());
            }
        }
    }
}
