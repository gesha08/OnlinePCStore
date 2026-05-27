using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Data.Enums;
using Controllers;
using System.Threading.Tasks;

namespace TestProject1
{
    public class UserControllerTests
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
        public async Task RegisterAsync_ShouldCreateUser_WhenUsernameIsUnique()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                var user = await controller.RegisterAsync("testuser", "password");

                Assert.IsNotNull(user);
                Assert.AreEqual("testuser", user.Username);
                Assert.AreEqual(Role.User, user.Role);
            }

            using (var context = new StoreContext(_options))
            {
                var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
                Assert.IsNotNull(userInDb);
            }
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnNull_WhenUsernameExists()
        {
            using (var context = new StoreContext(_options))
            {
                var existingUser = new User { Username = "testuser", Password = "anotherpassword" };
                context.Users.Add(existingUser);
                await context.SaveChangesAsync();
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                var user = await controller.RegisterAsync("testuser", "password");

                Assert.IsNull(user);
            }
        }

        [Test]
        public async Task LoginAsync_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                await controller.RegisterAsync("testuser", "password");
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                var user = await controller.LoginAsync("testuser", "password");

                Assert.IsNotNull(user);
                Assert.AreEqual("testuser", user.Username);
            }
        }

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                await controller.RegisterAsync("testuser", "password");
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                var user = await controller.LoginAsync("testuser", "wrongpassword");

                Assert.IsNull(user);
            }
        }

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new UserController(context);
                var user = await controller.LoginAsync("nonexistentuser", "password");
                Assert.IsNull(user);
            }
        }
    }
}
