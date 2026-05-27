using Controllers;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Cart : Form
    {
        private readonly OrderController orderController;
        private readonly List<OrderItem> cartItems;
        private readonly User currentUser;


        public Cart(User user, List<OrderItem> cartItems)
        {
            InitializeComponent();
            this.cartItems = cartItems;
            currentUser = user;
            orderController = new OrderController();
        }

        private void backButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void logoutButton_Click(object? sender, EventArgs e)
        {
            var loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void quitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Cart_Load(object sender, EventArgs e)
        {
            cartDataGridView.DataSource = cartItems.Select(ci => new 
            {
                Product = ci.Product?.Name,
                Quantity = ci.Quantity,
                Price = ci.UnitPrice
            }).ToList();
        }

        private async void finishOrderButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You must be logged in to place an order.");
                return;
            }

            if (!cartItems.Any())
            {
                MessageBox.Show("Your cart is empty.");
                return;
            }

            var finishOrderForm = new FinishOrder();
            if (finishOrderForm.ShowDialog() == DialogResult.OK)
            {
                var order = await orderController.CreateOrderAsync(currentUser, cartItems, finishOrderForm.Address, finishOrderForm.PaymentMethod, finishOrderForm.CardNumber, finishOrderForm.ExpiryDate, finishOrderForm.Cvv);

                if (order != null)
                {
                    MessageBox.Show("Order placed successfully!");
                    cartItems.Clear();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to place order.");
                }
            }
        }
    }
}