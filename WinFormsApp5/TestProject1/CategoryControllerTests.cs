using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Controllers;
using System.Threading.Tasks;
using System.Linq;

namespace TestProject1
{
    public class CategoryControllerTests
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
        public async Task AddCategoryAsync_ShouldAddCategory()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new CategoryController(context);
                var category = new Category { Name = "CPUs" };

                await controller.AddCategoryAsync(category);
            }

            using (var context = new StoreContext(_options))
            {
                Assert.AreEqual(1, await context.Categories.CountAsync());
                var cat = await context.Categories.FirstAsync();
                Assert.AreEqual("CPUs", cat.Name);
            }
        }

        [Test]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            using (var context = new StoreContext(_options))
            {
                context.Categories.Add(new Category { Name = "CPUs" });
                context.Categories.Add(new Category { Name = "Motherboards" });
                await context.SaveChangesAsync();
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new CategoryController(context);
                var categories = await controller.GetAllCategoriesAsync();

                Assert.IsNotNull(categories);
                Assert.AreEqual(2, categories.Count);
            }
        }

        [Test]
        public async Task UpdateCategoryAsync_ShouldUpdateCategory()
        {
            var category = new Category { Name = "CPUs" };
            using (var context = new StoreContext(_options))
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }

            category.Name = "Processors";
            using (var context = new StoreContext(_options))
            {
                var controller = new CategoryController(context);
                await controller.UpdateCategoryAsync(category);
            }

            using (var context = new StoreContext(_options))
            {
                var updatedCategory = await context.Categories.FindAsync(category.Id);
                Assert.IsNotNull(updatedCategory);
                Assert.AreEqual("Processors", updatedCategory.Name);
            }
        }

        [Test]
        public async Task DeleteCategoryAsync_ShouldDeleteCategory()
        {
            var category = new Category { Name = "CPUs" };
            using (var context = new StoreContext(_options))
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new CategoryController(context);
                await controller.DeleteCategoryAsync(category.Id);
            }

            using (var context = new StoreContext(_options))
            {
                Assert.AreEqual(0, await context.Categories.CountAsync());
            }
        }
    }
}
