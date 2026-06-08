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
        private readonly CategoryController categoryController;
        private readonly StoreContext storeContext;
        private readonly User currentUser;
        private List<OrderItem> cartItems = new List<OrderItem>();
        private List<Product> loadedProducts = null!;
        private bool isBusy = false;

        public Products(User user)
        {
            InitializeComponent();
            currentUser = user;
            storeContext = new StoreContext();
            productController = new ProductController(storeContext);
            categoryController = new CategoryController(storeContext);
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
            if (isBusy) return;
            isBusy = true;
            try
            {
                var categories = await categoryController.GetAllCategoriesAsync();
                var allCategories = new Category { Id = 0, Name = "All Categories" };
                categories.Insert(0, allCategories);
                categoryComboBox.DataSource = categories;
                categoryComboBox.DisplayMember = "Name";
                categoryComboBox.ValueMember = "Id";

                loadedProducts = await productController.GetAllProductsAsync();
                UpdateDataSource(loadedProducts);
            }
            finally
            {
                isBusy = false;
            }
        }

        private void UpdateDataSource(List<Product> products)
        {
            productsDataGridView.DataSource = products.Select(p => new
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

            if (productsDataGridView.Columns["addToCartButtonColumn"] == null)
            {
                var addToCartButtonColumn = new DataGridViewButtonColumn();
                addToCartButtonColumn.Name = "addToCartButtonColumn";
                addToCartButtonColumn.HeaderText = "Add to Cart";
                addToCartButtonColumn.Text = "Add to Cart";
                addToCartButtonColumn.UseColumnTextForButtonValue = true;
                productsDataGridView.Columns.Add(addToCartButtonColumn);
            }
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
            var cartForm = new Cart(currentUser, cartItems, storeContext);
            cartForm.Show();
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            var customerProfileForm = new CustomerProfile(currentUser, storeContext);
            customerProfileForm.ShowDialog();
        }

        private void productsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == productsDataGridView.Columns["addToCartButtonColumn"].Index && e.RowIndex >= 0)
            {
                var productId = (int)productsDataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                var product = loadedProducts.First(p => p.Id == productId);

                var existingCartItem = cartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = 1,
                        UnitPrice = product.Price
                    };
                    cartItems.Add(orderItem);
                }

                MessageBox.Show($"{product.Name} added to cart.");
            }
        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            if(isBusy) return;
            isBusy = true;
            try
            {
                var searchTerm = searchTextBox.Text;
                var categoryId = (int)categoryComboBox.SelectedValue;

                loadedProducts = await productController.SearchProductsAsync(searchTerm, categoryId == 0 ? null : categoryId);
                UpdateDataSource(loadedProducts);
            }
            finally
            {
                isBusy = false;
            }
        }

        private async void resetButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                searchTextBox.Clear();
                categoryComboBox.SelectedValue = 0;
                loadedProducts = await productController.GetAllProductsAsync();
                UpdateDataSource(loadedProducts);
            }
            finally
            {
                isBusy = false;
            }
        }
    }
}