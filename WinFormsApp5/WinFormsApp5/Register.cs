using Controllers;
using Data;
using System;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Register : Form
    {
        private readonly UserController userController;
        private readonly StoreContext storeContext;

        public Register()
        {
            InitializeComponent();
            storeContext = new StoreContext();
            userController = new UserController(storeContext);
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            var user = await userController.RegisterAsync(usernameTextBox.Text, passwordTextBox.Text);
            if (user != null)
            {
                MessageBox.Show("Registration successful!");
                var loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Registration failed.");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void quitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
