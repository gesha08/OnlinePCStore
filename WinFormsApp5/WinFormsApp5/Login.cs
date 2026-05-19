using Controllers;
using Data;
using Data.Entities;
using System;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Login : Form
    {
        private readonly UserController userController;
        private readonly StoreContext storeContext;

        public Login()
        {
            InitializeComponent();
            storeContext = new StoreContext();
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
                    var adminForm = new AdminView();
                    adminForm.Show();
                }
                else
                {
                    var productsForm = new Products(user);
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
            var registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void quitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
