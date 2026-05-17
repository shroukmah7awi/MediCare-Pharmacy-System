using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Oop_Project
{
    public partial class Form1 : Form
    {
        Panel loginPanel;
        
        Dictionary<string, string> users = new Dictionary<string, string>
        {
            {"admin", "123"}, {"dr_sara", "2026"}, {"pharmacist", "med123"},
            {"ahmed", "it_dept"}, {"Mostafa", "1122"}, {"user2", "p2"},
            {"shrouk", "123"}, {"user4", "p4"}, {"user5", "p5"}, {"staff", "mansoura"}
        };

        public Form1()
        {
            InitializeComponent();
            CleanOldLabels();
            ApplyModernPharmacyDesign();
        }

        private void CleanOldLabels()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label || ctrl is TextBox || ctrl is Button)
                    ctrl.Visible = false;
            }
        }

        private void ApplyModernPharmacyDesign()
        {
            
            this.Text = "Guide to Medicine - Login";
            this.Size = new Size(1050, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            try
            {
                this.BackgroundImage = Image.FromFile("form1.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                this.BackColor = Color.White;
            }

           
            loginPanel = new Panel();
            loginPanel.Size = new Size(600, 450);
            loginPanel.Location = new Point(160, (this.ClientSize.Height - loginPanel.Height) / 2);
            loginPanel.BackColor = Color.FromArgb(230, Color.AliceBlue);
            this.Controls.Add(loginPanel);
            loginPanel.BringToFront();

          
            Label lblWelcome = new Label
            {
                Text = "Welcome To MediCare System",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(570, 80),
                Location = new Point(15, 30),
                BackColor = Color.Transparent
            };

           
            Label lblUser = new Label { Text = "Username:", Font = new Font("Segoe UI", 13, FontStyle.Bold), Location = new Point(60, 140), AutoSize = true, BackColor = Color.Transparent };
            txtUser.Visible = true;
            txtUser.Parent = loginPanel;
            txtUser.Location = new Point(60, 165);
            txtUser.Size = new Size(480, 55);
            txtUser.Font = new Font("Segoe UI", 12);

            Label lblPass = new Label { Text = "Password:", Font = new Font("Segoe UI", 13, FontStyle.Bold), Location = new Point(60, 215), AutoSize = true, BackColor = Color.Transparent };
            txtPass.Visible = true;
            txtPass.Parent = loginPanel;
            txtPass.Location = new Point(60, 240);
            txtPass.Size = new Size(480, 55);
            txtPass.Font = new Font("Segoe UI", 12);
            txtPass.UseSystemPasswordChar = true;

            
            login.Visible = true;
            login.Parent = loginPanel;
            login.Text = "LOGIN";
            login.Location = new Point(215, 310);
            login.Size = new Size(170, 100);
            login.FlatStyle = FlatStyle.Flat;
            login.FlatAppearance.BorderSize = 0;
            login.BackColor = Color.FromArgb(41, 128, 185); 
            login.ForeColor = Color.White;
            login.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            login.Cursor = Cursors.Hand;

          
            login.GotFocus += (s, ev) => {
                login.BackColor = Color.FromArgb(21, 67, 96); 
            };

            login.LostFocus += (s, ev) => {
                login.BackColor = Color.FromArgb(41, 128, 185); 
            };

            loginPanel.Controls.Add(lblWelcome);
            loginPanel.Controls.Add(lblUser);
            loginPanel.Controls.Add(lblPass);
        }

        private void login_Click(object sender, EventArgs e)
        {
            string u = txtUser.Text.Trim();
            string p = txtPass.Text.Trim();

            if (users.ContainsKey(u) && users[u] == p)
            {
                DashBoard d = new DashBoard();
                d.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Guide to Medicine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
                txtUser.Focus();
            }
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPass.Focus();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                login.Focus(); 
            }
        }
    }
}