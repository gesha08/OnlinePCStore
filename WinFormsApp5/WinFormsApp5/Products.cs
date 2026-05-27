using Controllers;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace WinFormsApp5
{
    public partial class Products : Form
    {
        private readonly ProductController productController;
        private readonly StoreContext storeContext;
        private readonly User currentUser;
        private List<OrderItem> cartItems = new List<OrderItem>();
        private List<Product> loadedProducts = null!;

        public Products(User user)
        {
            InitializeComponent();
            currentUser = user;
            storeContext = new StoreContext();
            productController = new ProductController(storeContext);
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

        private async void Products_Load(object sender, EventArgs e)
        {
            loadedProducts = await productController.GetAllProductsAsync();
            productsDataGridView.DataSource = loadedProducts.Select(p => new
            {
                p.Id,
                Picture = CreateImageFromBytes(p.Picture),
                p.Name,
                p.Description,
                p.Price,
                p.Stock,
                Category = p.Category.Name
            }).ToList();

            productsDataGridView.RowTemplate.Height = 70;
            foreach (DataGridViewRow row in productsDataGridView.Rows)
            {
                row.Height = 70;
            }

            if (productsDataGridView.Columns["Picture"] is DataGridViewImageColumn pictureColumn)
            {
                pictureColumn.HeaderText = "Picture";
                pictureColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                pictureColumn.DefaultCellStyle.NullValue = null;
            }

            var addToCartButtonColumn = new DataGridViewButtonColumn();
            addToCartButtonColumn.Name = "addToCartButtonColumn";
            addToCartButtonColumn.HeaderText = "Add to Cart";
            addToCartButtonColumn.Text = "Add to Cart";
            addToCartButtonColumn.UseColumnTextForButtonValue = true;
            productsDataGridView.Columns.Add(addToCartButtonColumn);
        }

        private static Image? CreateImageFromBytes(byte[]? picture)
        {
            if (picture == null || picture.Length == 0)
            {
                return null;
            }

            using var stream = new MemoryStream(picture);
            using var image = Image.FromStream(stream);
            return new Bitmap(image);
        }

        private void viewCartButton_Click(object sender, EventArgs e)
        {
            var cartForm = new Cart(currentUser, cartItems);
            cartForm.Show();
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            var customerProfileForm = new CustomerProfile(currentUser);
            customerProfileForm.ShowDialog();
        }

        private void productsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == productsDataGridView.Columns["addToCartButtonColumn"].Index && e.RowIndex >= 0)
            {
                var productId = (int)productsDataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                var product = loadedProducts.First(p => p.Id == productId);
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = 1,
                    UnitPrice = product.Price
                };
                cartItems.Add(orderItem);
                MessageBox.Show($"{product.Name} added to cart.");
            }
        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}