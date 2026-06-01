using Data;
using Data.Enums;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class OrderController
    {
        private readonly StoreContext _context;

        public OrderController(StoreContext context)
        {
            _context = context;
        }


        public async Task<Order> CreateOrderAsync(User user, List<OrderItem> orderItems, string address, string paymentMethod, string? cardNumber, string? expiryDate, string? cvv)
        {
            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = orderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
                OrderItems = orderItems,
                Address = address,
                PaymentMethod = paymentMethod,
                CardNumber = cardNumber,
                ExpiryDate = expiryDate,
                Cvv = cvv,
                Status = OrderStatus.Pending
            };

            foreach (var item in orderItems)
            {
                item.Product = null;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in orderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                }
            }
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task FinishOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = OrderStatus.Finished;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CancelOrderAsync(int orderId, int userId)
        {
            var order = await _context.Orders
                                    .Include(o => o.OrderItems)
                                    .ThenInclude(oi => oi.Product)
                                    .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return false;
            }

            if (order.Status != OrderStatus.Pending)
            {
                return false;
            }

            order.Status = OrderStatus.Canceled;

            foreach (var item in order.OrderItems)
            {
                if (item.Product != null)
                {
                    item.Product.Stock += item.Quantity;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
