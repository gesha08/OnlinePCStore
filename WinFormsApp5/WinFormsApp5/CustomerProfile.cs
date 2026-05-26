using Controllers;
using Data;
using Data.Enums;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinFormsApp5
{
    public partial class CustomerProfile : Form
    {
        private readonly OrderController orderController;
        private readonly User currentUser;
        private readonly DbContextOptions<StoreContext> dbContextOptions;

        public CustomerProfile(User user, DbContextOptions<StoreContext> options)
        {
            InitializeComponent();
            currentUser = user;
            dbContextOptions = options;
            orderController = new OrderController(options);
            this.Load += CustomerProfile_Load;
        }

        private async void CustomerProfile_Load(object? sender, EventArgs e)
        {
            usernameLabel.Text = $"Welcome, {currentUser.Username}!";
            await LoadOrders();
        }

        private async Task LoadOrders()
        {
            var userOrders = await orderController.GetOrdersByUserIdAsync(currentUser.Id);
            ordersDataGridView.DataSource = userOrders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                o.TotalAmount,
                o.Address,
                o.PaymentMethod,
                o.Status
            }).ToList();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void ordersDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            var cancelColumn = ordersDataGridView.Columns["cancelOrderColumn"];
            if (cancelColumn == null) return; // Add null check for the column

            if (e.ColumnIndex == cancelColumn.Index && e.RowIndex >= 0)
            {
                var selectedRow = ordersDataGridView.Rows[e.RowIndex];
                var orderId = (int)selectedRow.Cells["Id"]!.Value; // Use null-forgiving operator
                
                var statusCell = ordersDataGridView.Rows[e.RowIndex].Cells["Status"]; 
                if (statusCell?.Value is not OrderStatus orderStatus) 
                {
                    orderStatus = OrderStatus.Pending; // Default if status is null or not OrderStatus
                }

                if (orderStatus == OrderStatus.Pending)
                {
                    var confirmResult = MessageBox.Show("Are you sure you want to cancel this order?", "Confirm Cancel", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        var success = await orderController.CancelOrderAsync(orderId, currentUser.Id);
                        if (success)
                        {
                            MessageBox.Show("Order cancelled successfully!");
                            await LoadOrders(); // Reload orders to update UI
                        }
                        else
                        {
                            MessageBox.Show("Failed to cancel order. Only pending orders can be cancelled, or you don't have permission.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Only pending orders can be cancelled.");
                }
            }
        }

        private void ordersDataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if it's the "cancelOrderColumn" and a valid row
            var cancelColumn = ordersDataGridView.Columns["cancelOrderColumn"];
            if (cancelColumn == null) return; // Add null check for the column

            if (e.ColumnIndex == cancelColumn.Index && e.RowIndex >= 0)
            {
                var statusCell = ordersDataGridView.Rows[e.RowIndex].Cells["Status"];
                if (statusCell?.Value is not OrderStatus orderStatus) 
                {
                    orderStatus = OrderStatus.Pending; // Default if status is null or not OrderStatus
                }
                
                if (orderStatus != OrderStatus.Pending)
                {
                    // Disable the button visually
                    e.Value = "N/A"; // Change button text
                    e.CellStyle.ForeColor = System.Drawing.Color.Gray; // Gray out text
                    e.CellStyle.SelectionForeColor = System.Drawing.Color.Gray;
                    var cell = ordersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell != null)
                    {
                        cell.Style.ForeColor = System.Drawing.Color.Gray;
                        cell.ReadOnly = true;
                    }
                }
                else
                {
                    // Ensure the button is enabled and has original style for pending orders
                    e.Value = "Cancel";
                    e.CellStyle.ForeColor = ordersDataGridView.DefaultCellStyle.ForeColor; // Use default
                    e.CellStyle.SelectionForeColor = ordersDataGridView.DefaultCellStyle.SelectionForeColor; // Use default
                    var cell = ordersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell != null)
                    {
                        cell.ReadOnly = false;
                    }
                }
            }
        }
    }
}
