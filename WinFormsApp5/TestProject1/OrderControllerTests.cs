using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Data.Enums;
using Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    public class OrderControllerTests
    {
        private DbContextOptions<StoreContext> _options;
        private User _user;
        private Category _category;
        private Product _product;

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

                _user = new User { Username = "customer", Password = "password", Role = Role.User };
                _category = new Category { Name = "RAM" };
                _product = new Product { Name = "DDR4 16GB", Description = "A stick of ram.", Price = 75.00m, Stock = 100, Category = _category };

                context.Users.Add(_user);
                context.Categories.Add(_category);
                context.Products.Add(_product);
                context.SaveChanges();
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
        public async Task CreateOrderAsync_ShouldCreateOrderAndDecreaseStock()
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = _product.Id, Quantity = 2, UnitPrice = _product.Price }
            };

            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                await controller.CreateOrderAsync(_user, orderItems, "123 Test St", "Credit Card", "1234", "12/26", "123");
            }

            using (var context = new StoreContext(_options))
            {
                var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync();
                Assert.IsNotNull(order);
                Assert.AreEqual(_user.Id, order.UserId);
                Assert.AreEqual(150.00m, order.TotalAmount);
                Assert.AreEqual(1, order.OrderItems.Count);
                Assert.AreEqual(OrderStatus.Pending, order.Status);

                var product = await context.Products.FindAsync(_product.Id);
                Assert.AreEqual(98, product.Stock);
            }
        }

        [Test]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                var orderItems = new List<OrderItem> { new OrderItem { ProductId = _product.Id, Quantity = 1, UnitPrice = _product.Price } };
                await controller.CreateOrderAsync(_user, orderItems, "123 Test St", "Credit Card", null, null, null);
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                var orders = await controller.GetAllOrdersAsync();
                Assert.AreEqual(1, orders.Count);
            }
        }

        [Test]
        public async Task GetOrdersByUserIdAsync_ShouldReturnUserOrders()
        {
            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                var orderItems = new List<OrderItem> { new OrderItem { ProductId = _product.Id, Quantity = 1, UnitPrice = _product.Price } };
                await controller.CreateOrderAsync(_user, orderItems, "123 Test St", "Credit Card", null, null, null);
            }

            using (var context = new StoreContext(_options))
            {
                var anotherUser = new User { Username = "another", Password = "password" };
                context.Users.Add(anotherUser);
                await context.SaveChangesAsync();

                var controller = new OrderController(context);
                var orders = await controller.GetOrdersByUserIdAsync(_user.Id);
                var noOrders = await controller.GetOrdersByUserIdAsync(anotherUser.Id);

                Assert.AreEqual(1, orders.Count);
                Assert.AreEqual(0, noOrders.Count);
            }
        }

        [Test]
        public async Task FinishOrderAsync_ShouldChangeOrderStatusToFinished()
        {
            int orderId;
            using (var context = new StoreContext(_options))
            {
                var order = new Order { UserId = _user.Id, Status = OrderStatus.Pending, Address = "Some Address", PaymentMethod = "Some Payement" };
                context.Orders.Add(order);
                await context.SaveChangesAsync();
                orderId = order.Id;
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                await controller.FinishOrderAsync(orderId);
            }

            using (var context = new StoreContext(_options))
            {
                var order = await context.Orders.FindAsync(orderId);
                Assert.AreEqual(OrderStatus.Finished, order.Status);
            }
        }

        [Test]
        public async Task CancelOrderAsync_ShouldChangeStatusAndRestoreStock()
        {
            int orderId;
            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                var product = await context.Products.FirstAsync();
                var orderItems = new List<OrderItem> { new OrderItem { ProductId = product.Id, Quantity = 5, UnitPrice = product.Price } };
                var order = await controller.CreateOrderAsync(_user, orderItems, "Some Address", "Some Payement", null, null, null);
                orderId = order.Id;
            }

            using (var context = new StoreContext(_options))
            {
                var controller = new OrderController(context);
                var result = await controller.CancelOrderAsync(orderId, _user.Id);
                Assert.IsTrue(result);
            }

            using (var context = new StoreContext(_options))
            {
                var order = await context.Orders.FindAsync(orderId);
                Assert.AreEqual(OrderStatus.Canceled, order.Status);
                var product = await context.Products.FindAsync(_product.Id);
                Assert.AreEqual(100, product.Stock);
            }
        }
    }
}
