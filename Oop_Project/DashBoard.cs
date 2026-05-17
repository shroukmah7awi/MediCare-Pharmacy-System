using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oop_Project
{
    public partial class DashBoard : Form
    {
        Panel menuPanel;

        public DashBoard()
        {
            InitializeComponent();
            ApplyModernDashboardDesign();
        }

        private void ApplyModernDashboardDesign()
        {
            
            this.Text = "MediCare System - Dashboard";
            this.Size = new Size(1050, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

           
            try
            {
                this.BackgroundImage = Image.FromFile("a1.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                this.BackColor = Color.White;
            }

            
            menuPanel = new Panel();
            menuPanel.Size = new Size(500, 450); 
            menuPanel.Location = new Point((this.ClientSize.Width - menuPanel.Width) / 2, (this.ClientSize.Height - menuPanel.Height) / 2);
            menuPanel.BackColor = Color.FromArgb(230, Color.AliceBlue);
            this.Controls.Add(menuPanel);
            menuPanel.BringToFront();

           
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label && ctrl.Text.Contains("Pharmacy"))
                    ctrl.Visible = false;
            }

          
            Label lblTitle = new Label
            {
                Text = "Main Dashboard",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(460, 60),
                Location = new Point(20, 30),
                BackColor = Color.Transparent
            };

           
            btnMed.Visible = true;
            btnMed.Parent = menuPanel;
            btnMed.Text = "📦 Manage Medicines";
            btnMed.Location = new Point(50, 120);
            btnMed.Size = new Size(400, 55);
            btnMed.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnMed.FlatStyle = FlatStyle.Flat;
            btnMed.FlatAppearance.BorderSize = 0;
            btnMed.BackColor = Color.FromArgb(41, 128, 185);
            btnMed.ForeColor = Color.White;
            btnMed.Cursor = Cursors.Hand;

            btnMed.MouseEnter += (s, e) => btnMed.BackColor = Color.FromArgb(31, 97, 141);
            btnMed.MouseLeave += (s, e) => btnMed.BackColor = Color.FromArgb(41, 128, 185);
            btnMed.Click += new EventHandler(btnMed_Click);

            
            Button btnSales = new Button
            {
                Visible = true,
                Parent = menuPanel,
                Text = "💳 Sales & Billing System",
                Location = new Point(50, 200), 
                Size = new Size(400, 55),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(39, 174, 96), 
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSales.FlatAppearance.BorderSize = 0;

            btnSales.MouseEnter += (s, e) => btnSales.BackColor = Color.FromArgb(30, 132, 73);
            btnSales.MouseLeave += (s, e) => btnSales.BackColor = Color.FromArgb(39, 174, 96);
           
            btnSales.Click += (s, e) =>
            {
                Form4 sForm = new Form4();
                sForm.Show();
                this.Hide();
            };
          
            btnLogout.Visible = true;
            btnLogout.Parent = menuPanel;
            btnLogout.Text = "🚪 Logout";
            btnLogout.Location = new Point(50, 280); 
            btnLogout.Size = new Size(400, 55);
            btnLogout.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.ForeColor = Color.White;
            btnLogout.Cursor = Cursors.Hand;

            btnLogout.MouseEnter += (s, e) => btnLogout.BackColor = Color.FromArgb(150, 40, 27);
            btnLogout.MouseLeave += (s, e) => btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.Click += new EventHandler(btnLogout_Click);

           
            menuPanel.Controls.Add(lblTitle);
            menuPanel.Controls.Add(btnSales);
        }

        private void btnMed_Click(object sender, EventArgs e)
        {
            MedicineForm m = new MedicineForm();
            m.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            
            Form openForm = Application.OpenForms["Form1"];
            if (openForm != null)
            {
                openForm.Show();
            }
            else
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
            }

            this.Dispose(); 
        }
    }
}