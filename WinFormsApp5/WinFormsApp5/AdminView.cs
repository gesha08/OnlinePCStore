using Controllers;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace WinFormsApp5
{
    public partial class AdminView : Form
    {
        private readonly ProductController productController;
        private readonly CategoryController categoryController;
        private readonly OrderController orderController;
        private readonly StoreContext storeContext;
        private byte[]? selectedProductPicture;
        private bool isBusy = false;

        public AdminView()
        {
            InitializeComponent();
            storeContext = new StoreContext();
            productController = new ProductController(storeContext);
            categoryController = new CategoryController(storeContext);
            orderController = new OrderController(storeContext);

            productsDataGridView.SelectionChanged += new System.EventHandler(productsDataGridView_SelectionChanged);
        }

        private void productsDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (productsDataGridView.CurrentRow == null) return;

            var productId = (int)productsDataGridView.CurrentRow.Cells["Id"].Value;
            var product = loadedProducts.First(p => p.Id == productId);

            nameTextBox.Text = product.Name;
            descriptionTextBox.Text = product.Description;
            priceTextBox.Text = product.Price.ToString();
            stockTextBox.Text = product.Stock.ToString();
            categoryComboBox.SelectedValue = product.CategoryId;
            selectedProductPicture = product.Picture;
            selectPictureButton.Text = product.Picture == null ? "Choose..." : "Change";
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

        private async void AdminView_Load(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                await LoadProducts();
                await LoadCategories();
                await LoadOrders();
            }
            finally
            {
                isBusy = false;
            }
        }

        private List<Product> loadedProducts = null!;
        private List<Order> loadedOrders = null!;

        private async Task LoadProducts()
        {
            loadedProducts = await productController.GetAllProductsAsync();
            productsDataGridView.DataSource = loadedProducts.Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.Stock
            }).ToList();
        }

        private async Task LoadCategories()
        {
            var categories = await categoryController.GetAllCategoriesAsync();
            categoriesDataGridView.DataSource = categories;
            categoryComboBox.DataSource = categories;
            categoryComboBox.DisplayMember = "Name";
            categoryComboBox.ValueMember = "Id";
        }

        private async Task LoadOrders()
        {
            loadedOrders = await orderController.GetAllOrdersAsync();
            ordersDataGridView.DataSource = loadedOrders.Select(o => new
            {
                o.Id,
                UserName = o.User.Username,
                o.OrderDate,
                o.TotalAmount,
                o.Address,
                o.PaymentMethod,
                o.Status
            }).ToList();
        }

        private async void finishOrderAdminButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                if (ordersDataGridView.CurrentRow == null) return;

                var orderId = (int)ordersDataGridView.CurrentRow.Cells["Id"].Value;
                await orderController.FinishOrderAsync(orderId);
                await LoadOrders();
            }
            finally
            {
                isBusy = false;
            }
        }

        private async void addProductButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                if (categoryComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }

                if (!decimal.TryParse(priceTextBox.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                if (!int.TryParse(stockTextBox.Text, out int stock))
                {
                    MessageBox.Show("Please enter a valid stock amount.");
                    return;
                }

                var existingProduct = await productController.GetProductByDetailsAsync(nameTextBox.Text, descriptionTextBox.Text, price);
                if (existingProduct != null)
                {
                    existingProduct.Stock += stock;
                    await productController.UpdateProductAsync(existingProduct);
                }
                else
                {
                    var product = new Product
                    {
                        Name = nameTextBox.Text,
                        Description = descriptionTextBox.Text,
                        Price = price,
                        Stock = stock,
                        Picture = selectedProductPicture,
                        CategoryId = (int)categoryComboBox.SelectedValue
                    };
                    await productController.AddProductAsync(product);
                }

                selectedProductPicture = null;
                selectPictureButton.Text = "Choose...";
                await LoadProducts();
            }
            finally
            {
                isBusy = false;
            }
        }

        private async void updateProductButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                if (productsDataGridView.CurrentRow == null) return;

                if (categoryComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }

                if (!decimal.TryParse(priceTextBox.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                if (!int.TryParse(stockTextBox.Text, out int stock))
                {
                    MessageBox.Show("Please enter a valid stock amount.");
                    return;
                }

                var productId = (int)productsDataGridView.CurrentRow.Cells["Id"].Value;
                var product = loadedProducts.First(p => p.Id == productId);

                product.Name = nameTextBox.Text;
                product.Description = descriptionTextBox.Text;
                product.Price = price;
                product.Stock = stock;
                product.Picture = selectedProductPicture;
                product.CategoryId = (int)categoryComboBox.SelectedValue;

                await productController.UpdateProductAsync(product);
                await LoadProducts();
            }
            finally
            {
                isBusy = false;
            }
        }

        private void selectPictureButton_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Select product picture",
                Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            selectedProductPicture = File.ReadAllBytes(openFileDialog.FileName);
            selectPictureButton.Text = "Selected";
        }

        private async void deleteProductButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                if (productsDataGridView.CurrentRow == null) return;

                var productId = (int)productsDataGridView.CurrentRow.Cells["Id"].Value;
                await productController.DeleteProductAsync(productId);
                await LoadProducts();
            }
            finally
            {
                isBusy = false;
            }
        }

        private async void addCategoryButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                var category = new Category
                {
                    Name = categoryNameTextBox.Text
                };
                await categoryController.AddCategoryAsync(category);
                await LoadCategories();
            }
            finally
            {
                isBusy = false;
            }
        }

        private async void updateCategoryButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                if (categoriesDataGridView.CurrentRow == null) return;

                var category = (Category)categoriesDataGridView.CurrentRow.DataBoundItem;
                category.Name = categoryNameTextBox.Text;
                await categoryController.UpdateCategoryAsync(category);
                await LoadCategories();
            }
            finally
            {
                isBusy = false;
            }
        }

        private async void deleteCategoryButton_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            isBusy = true;
            try
            {
                if (categoriesDataGridView.CurrentRow == null) return;

                var category = (Category)categoriesDataGridView.CurrentRow.DataBoundItem;
                await categoryController.DeleteCategoryAsync(category.Id);
                await LoadCategories();
            }
            finally
            {
                isBusy = false;
            }
        }
    }
}