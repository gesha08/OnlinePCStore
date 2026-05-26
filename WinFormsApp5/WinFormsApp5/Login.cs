using Controllers;
using Data;
using Data.Enums;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Login : Form
    {
        private readonly UserController userController;
        private readonly StoreContext storeContext;
        private readonly DbContextOptions<StoreContext> dbContextOptions;

        public Login(DbContextOptions<StoreContext> options)
        {
            InitializeComponent();
            dbContextOptions = options;
            storeContext = new StoreContext(options);
            userController = new UserController(storeContext);
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            var user = await userController.LoginAsync(usernameTextBox.Text, passwordTextBox.Text);
            if (user != null)
            {
                MessageBox.Show("Login successful!");
                if (user.Role == Role.Admin)
                {
                    var adminForm = new AdminView(dbContextOptions);
                    adminForm.Show();
                }
                else
                {
                    var productsForm = new Products(user, dbContextOptions);
                    productsForm.Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var registerForm = new Register(dbContextOptions);
            registerForm.Show();
            this.Hide();
        }

        private void quitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}