namespace WinFormsApp5
{
    partial class Products
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            productsDataGridView = new DataGridView();
            panel1 = new Panel();
            quitButton = new Button();
            logoutButton = new Button();
            viewCartButton = new Button();
            profileButton = new Button();
            ((System.ComponentModel.ISupportInitialize)productsDataGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // productsDataGridView
            // 
            productsDataGridView.AllowUserToAddRows = false;
            productsDataGridView.AllowUserToDeleteRows = false;
            productsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productsDataGridView.BackgroundColor = Color.FromArgb(34, 36, 49);
            productsDataGridView.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(78, 184, 206);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            productsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            productsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(34, 36, 49);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(78, 184, 206);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            productsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            productsDataGridView.Dock = DockStyle.Fill;
            productsDataGridView.EnableHeadersVisualStyles = false;
            productsDataGridView.GridColor = Color.FromArgb(78, 184, 206);
            productsDataGridView.Location = new Point(0, 0);
            productsDataGridView.Margin = new Padding(3, 4, 3, 4);
            productsDataGridView.Name = "productsDataGridView";
            productsDataGridView.ReadOnly = true;
            productsDataGridView.RowHeadersVisible = false;
            productsDataGridView.RowHeadersWidth = 51;
            productsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productsDataGridView.Size = new Size(914, 600);
            productsDataGridView.TabIndex = 0;
            productsDataGridView.CellClick += productsDataGridView_CellClick;
            productsDataGridView.CellContentClick += productsDataGridView_CellContentClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(quitButton);
            panel1.Controls.Add(logoutButton);
            panel1.Controls.Add(viewCartButton);
            panel1.Controls.Add(profileButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 550);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 50);
            panel1.TabIndex = 1;
            // 
            // quitButton
            // 
            quitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            quitButton.BackColor = Color.FromArgb(78, 184, 206);
            quitButton.FlatAppearance.BorderSize = 0;
            quitButton.FlatStyle = FlatStyle.Flat;
            quitButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            quitButton.ForeColor = Color.White;
            quitButton.Location = new Point(801, 4);
            quitButton.Margin = new Padding(3, 4, 3, 4);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(101, 40);
            quitButton.TabIndex = 3;
            quitButton.Text = "Quit";
            quitButton.UseVisualStyleBackColor = false;
            quitButton.Click += quitButton_Click;
            // 
            // logoutButton
            // 
            logoutButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            logoutButton.BackColor = Color.FromArgb(78, 184, 206);
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.FlatStyle = FlatStyle.Flat;
            logoutButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            logoutButton.ForeColor = Color.White;
            logoutButton.Location = new Point(694, 4);
            logoutButton.Margin = new Padding(3, 4, 3, 4);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(101, 40);
            logoutButton.TabIndex = 2;
            logoutButton.Text = "Logout";
            logoutButton.UseVisualStyleBackColor = false;
            logoutButton.Click += logoutButton_Click;
            // 
            // viewCartButton
            // 
            viewCartButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            viewCartButton.BackColor = Color.FromArgb(78, 184, 206);
            viewCartButton.FlatAppearance.BorderSize = 0;
            viewCartButton.FlatStyle = FlatStyle.Flat;
            viewCartButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            viewCartButton.ForeColor = Color.White;
            viewCartButton.Location = new Point(587, 4);
            viewCartButton.Margin = new Padding(3, 4, 3, 4);
            viewCartButton.Name = "viewCartButton";
            viewCartButton.Size = new Size(101, 40);
            viewCartButton.TabIndex = 1;
            viewCartButton.Text = "View Cart";
            viewCartButton.UseVisualStyleBackColor = false;
            viewCartButton.Click += viewCartButton_Click;
            // 
            // profileButton
            // 
            profileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            profileButton.BackColor = Color.FromArgb(78, 184, 206);
            profileButton.FlatAppearance.BorderSize = 0;
            profileButton.FlatStyle = FlatStyle.Flat;
            profileButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            profileButton.ForeColor = Color.White;
            profileButton.Location = new Point(480, 4);
            profileButton.Margin = new Padding(3, 4, 3, 4);
            profileButton.Name = "profileButton";
            profileButton.Size = new Size(101, 40);
            profileButton.TabIndex = 0;
            profileButton.Text = "Profile";
            profileButton.UseVisualStyleBackColor = false;
            profileButton.Click += profileButton_Click;
            // 
            // Products
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 36, 49);
            ClientSize = new Size(914, 600);
            Controls.Add(panel1);
            Controls.Add(productsDataGridView);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Products";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Products";
            Load += Products_Load;
            ((System.ComponentModel.ISupportInitialize)productsDataGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView productsDataGridView;
        private Button viewCartButton;
        private Panel panel1;
        private Button quitButton;
        private Button logoutButton;
        private Button profileButton;
    }
}
