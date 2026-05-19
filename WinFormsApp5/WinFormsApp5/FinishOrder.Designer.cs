namespace WinFormsApp5
{
    partial class FinishOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.paymentMethodLabel = new System.Windows.Forms.Label();
            this.cashRadioButton = new System.Windows.Forms.RadioButton();
            this.cardRadioButton = new System.Windows.Forms.RadioButton();
            this.cardDetailsPanel = new System.Windows.Forms.Panel();
            this.cvvLabel = new System.Windows.Forms.Label();
            this.cvvTextBox = new System.Windows.Forms.TextBox();
            this.expiryDateLabel = new System.Windows.Forms.Label();
            this.expiryDateTextBox = new System.Windows.Forms.TextBox();
            this.cardNumberLabel = new System.Windows.Forms.Label();
            this.cardNumberTextBox = new System.Windows.Forms.TextBox();
            this.finishOrderButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.quitButton = new System.Windows.Forms.Button();
            this.cardDetailsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addressLabel.Location = new System.Drawing.Point(50, 50);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(66, 21);
            this.addressLabel.TabIndex = 0;
            this.addressLabel.Text = "Address";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addressTextBox.Location = new System.Drawing.Point(54, 74);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(300, 29);
            this.addressTextBox.TabIndex = 1;
            // 
            // paymentMethodLabel
            // 
            this.paymentMethodLabel.AutoSize = true;
            this.paymentMethodLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.paymentMethodLabel.Location = new System.Drawing.Point(50, 120);
            this.paymentMethodLabel.Name = "paymentMethodLabel";
            this.paymentMethodLabel.Size = new System.Drawing.Size(127, 21);
            this.paymentMethodLabel.TabIndex = 2;
            this.paymentMethodLabel.Text = "Payment Method";
            // 
            // cashRadioButton
            // 
            this.cashRadioButton.AutoSize = true;
            this.cashRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cashRadioButton.Location = new System.Drawing.Point(54, 144);
            this.cashRadioButton.Name = "cashRadioButton";
            this.cashRadioButton.Size = new System.Drawing.Size(62, 25);
            this.cashRadioButton.TabIndex = 3;
            this.cashRadioButton.TabStop = true;
            this.cashRadioButton.Text = "Cash";
            this.cashRadioButton.UseVisualStyleBackColor = true;
            // 
            // cardRadioButton
            // 
            this.cardRadioButton.AutoSize = true;
            this.cardRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cardRadioButton.Location = new System.Drawing.Point(154, 144);
            this.cardRadioButton.Name = "cardRadioButton";
            this.cardRadioButton.Size = new System.Drawing.Size(61, 25);
            this.cardRadioButton.TabIndex = 4;
            this.cardRadioButton.TabStop = true;
            this.cardRadioButton.Text = "Card";
            this.cardRadioButton.UseVisualStyleBackColor = true;
            this.cardRadioButton.CheckedChanged += new System.EventHandler(this.cardRadioButton_CheckedChanged);
            // 
            // cardDetailsPanel
            // 
            this.cardDetailsPanel.Controls.Add(this.cvvLabel);
            this.cardDetailsPanel.Controls.Add(this.cvvTextBox);
            this.cardDetailsPanel.Controls.Add(this.expiryDateLabel);
            this.cardDetailsPanel.Controls.Add(this.expiryDateTextBox);
            this.cardDetailsPanel.Controls.Add(this.cardNumberLabel);
            this.cardDetailsPanel.Controls.Add(this.cardNumberTextBox);
            this.cardDetailsPanel.Location = new System.Drawing.Point(54, 175);
            this.cardDetailsPanel.Name = "cardDetailsPanel";
            this.cardDetailsPanel.Size = new System.Drawing.Size(300, 150);
            this.cardDetailsPanel.TabIndex = 5;
            this.cardDetailsPanel.Visible = false;
            // 
            // cvvLabel
            // 
            this.cvvLabel.AutoSize = true;
            this.cvvLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cvvLabel.Location = new System.Drawing.Point(0, 100);
            this.cvvLabel.Name = "cvvLabel";
            this.cvvLabel.Size = new System.Drawing.Size(40, 21);
            this.cvvLabel.TabIndex = 5;
            this.cvvLabel.Text = "CVV";
            // 
            // cvvTextBox
            // 
            this.cvvTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cvvTextBox.Location = new System.Drawing.Point(0, 124);
            this.cvvTextBox.Name = "cvvTextBox";
            this.cvvTextBox.Size = new System.Drawing.Size(100, 29);
            this.cvvTextBox.TabIndex = 4;
            // 
            // expiryDateLabel
            // 
            this.expiryDateLabel.AutoSize = true;
            this.expiryDateLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.expiryDateLabel.Location = new System.Drawing.Point(0, 50);
            this.expiryDateLabel.Name = "expiryDateLabel";
            this.expiryDateLabel.Size = new System.Drawing.Size(88, 21);
            this.expiryDateLabel.TabIndex = 3;
            this.expiryDateLabel.Text = "Expiry Date";
            // 
            // expiryDateTextBox
            // 
            this.expiryDateTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.expiryDateTextBox.Location = new System.Drawing.Point(0, 74);
            this.expiryDateTextBox.Name = "expiryDateTextBox";
            this.expiryDateTextBox.Size = new System.Drawing.Size(150, 29);
            this.expiryDateTextBox.TabIndex = 2;
            // 
            // cardNumberLabel
            // 
            this.cardNumberLabel.AutoSize = true;
            this.cardNumberLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cardNumberLabel.Location = new System.Drawing.Point(0, 0);
            this.cardNumberLabel.Name = "cardNumberLabel";
            this.cardNumberLabel.Size = new System.Drawing.Size(104, 21);
            this.cardNumberLabel.TabIndex = 1;
            this.cardNumberLabel.Text = "Card Number";
            // 
            // cardNumberTextBox
            // 
            this.cardNumberTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cardNumberTextBox.Location = new System.Drawing.Point(0, 24);
            this.cardNumberTextBox.Name = "cardNumberTextBox";
            this.cardNumberTextBox.Size = new System.Drawing.Size(250, 29);
            this.cardNumberTextBox.TabIndex = 0;
            // 
            // finishOrderButton
            // 
            this.finishOrderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.finishOrderButton.FlatAppearance.BorderSize = 0;
            this.finishOrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.finishOrderButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.finishOrderButton.ForeColor = System.Drawing.Color.White;
            this.finishOrderButton.Location = new System.Drawing.Point(54, 340);
            this.finishOrderButton.Name = "finishOrderButton";
            this.finishOrderButton.Size = new System.Drawing.Size(120, 40);
            this.finishOrderButton.TabIndex = 6;
            this.finishOrderButton.Text = "Finish Order";
            this.finishOrderButton.UseVisualStyleBackColor = false;
            this.finishOrderButton.Click += new System.EventHandler(this.finishOrderButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(200, 340);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 40);
            this.backButton.TabIndex = 7;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.quitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 50);
            this.panel1.TabIndex = 8;
            // 
            // quitButton
            // 
            this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.quitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.quitButton.FlatAppearance.BorderSize = 0;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.quitButton.ForeColor = System.Drawing.Color.White;
            this.quitButton.Location = new System.Drawing.Point(687, 6);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(101, 40);
            this.quitButton.TabIndex = 3;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // FinishOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.finishOrderButton);
            this.Controls.Add(this.cardDetailsPanel);
            this.Controls.Add(this.cardRadioButton);
            this.Controls.Add(this.cashRadioButton);
            this.Controls.Add(this.paymentMethodLabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FinishOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Finish Order";
            this.cardDetailsPanel.ResumeLayout(false);
            this.cardDetailsPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label paymentMethodLabel;
        private System.Windows.Forms.RadioButton cashRadioButton;
        private System.Windows.Forms.RadioButton cardRadioButton;
        private System.Windows.Forms.Panel cardDetailsPanel;
        private System.Windows.Forms.Label cvvLabel;
        private System.Windows.Forms.TextBox cvvTextBox;
        private System.Windows.Forms.Label expiryDateLabel;
        private System.Windows.Forms.TextBox expiryDateTextBox;
        private System.Windows.Forms.Label cardNumberLabel;
        private System.Windows.Forms.TextBox cardNumberTextBox;
        private System.Windows.Forms.Button finishOrderButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button quitButton;
    }
}