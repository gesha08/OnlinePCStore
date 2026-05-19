using System;
using System.Windows.Forms;
using Data.Entities;
using System.Collections.Generic;

namespace WinFormsApp5
{
    public partial class FinishOrder : Form
    {
        public string Address { get; private set; }
        public string PaymentMethod { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpiryDate { get; private set; }
        public string Cvv { get; private set; }

        public FinishOrder()
        {
            InitializeComponent();
        }

        private void finishOrderButton_Click(object sender, EventArgs e)
        {
            Address = addressTextBox.Text;
            if (cashRadioButton.Checked)
            {
                PaymentMethod = "Cash";
            }
            else if (cardRadioButton.Checked)
            {
                PaymentMethod = "Card";
                CardNumber = cardNumberTextBox.Text;
                ExpiryDate = expiryDateTextBox.Text;
                Cvv = cvvTextBox.Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            cardDetailsPanel.Visible = cardRadioButton.Checked;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void quitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}