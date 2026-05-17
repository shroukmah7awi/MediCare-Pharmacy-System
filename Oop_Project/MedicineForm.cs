using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Oop_Project
{
    public partial class MedicineForm : Form
    {
        List<Medicine> medicines = new List<Medicine>();
        int selectedIndex = -1;

        private readonly string filePath = Path.Combine(Application.StartupPath, "medicines.json");

        Panel inputPanel;
        Label lblFormTitle;
        Button btnBack;

        public MedicineForm()
        {
            InitializeComponent();
            ApplyModernMedicineDesign();
            LoadData();
        }

        private void ApplyModernMedicineDesign()
        {
            this.Text = "MediCare System - Medicine Management";
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

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label &&
                    (ctrl.Text.Contains("Name") ||
                     ctrl.Text.Contains("Price") ||
                     ctrl.Text.Contains("Qty")))
                {
                    ctrl.Visible = false;
                }
            }

            inputPanel = new Panel();
            inputPanel.Size = new Size(400, 550);
            inputPanel.Location = new Point(30, 30);
            inputPanel.BackColor = Color.FromArgb(240, Color.AliceBlue);

            this.Controls.Add(inputPanel);
            inputPanel.BringToFront();

            // إضافة زر الرجوع الصغير أعلى اليسار
            btnBack = new Button
            {
                Text = "⬅",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(15, 20),
                Size = new Size(40, 40),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, Color.LightCyan);
            btnBack.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, Color.AliceBlue);

            btnBack.Click += btnBack_Click;
            inputPanel.Controls.Add(btnBack);

            lblFormTitle = new Label
            {
                Text = "Medicine Details",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(65, 20),
                Size = new Size(315, 40),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };

            inputPanel.Controls.Add(lblFormTitle);

            Label l1 = new Label
            {
                Text = "Medicine Name:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Location = new Point(25, 80),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            txtName.Visible = true;
            txtName.Parent = inputPanel;
            txtName.Location = new Point(25, 110);
            txtName.Size = new Size(340, 30);
            txtName.Font = new Font("Segoe UI", 12);

            Label l2 = new Label
            {
                Text = "Price (EGP):",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Location = new Point(25, 160),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            txtPrice.Visible = true;
            txtPrice.Parent = inputPanel;
            txtPrice.Location = new Point(25, 190);
            txtPrice.Size = new Size(340, 30);
            txtPrice.Font = new Font("Segoe UI", 12);

            Label l3 = new Label
            {
                Text = "Quantity:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Location = new Point(25, 240),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            txtQty.Visible = true;
            txtQty.Parent = inputPanel;
            txtQty.Location = new Point(25, 270);
            txtQty.Size = new Size(340, 30);
            txtQty.Font = new Font("Segoe UI", 12);

            inputPanel.Controls.Add(l1);
            inputPanel.Controls.Add(l2);
            inputPanel.Controls.Add(l3);

            txtName.KeyDown += MoveNextWithEnter;
            txtPrice.KeyDown += MoveNextWithEnter;
            txtQty.KeyDown += MoveNextWithEnter;

            // BUTTONS
            btnAdd.Visible = true;
            btnAdd.Parent = inputPanel;
            btnAdd.Text = "➕ Add Medicine";
            btnAdd.Location = new Point(25, 335);
            btnAdd.Size = new Size(340, 45);
            btnAdd.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.BackColor = Color.FromArgb(41, 128, 185);
            btnAdd.ForeColor = Color.White;

            btnUpdate.Visible = true;
            btnUpdate.Parent = inputPanel;
            btnUpdate.Text = "🔄 Update Selected";
            btnUpdate.Location = new Point(25, 395);
            btnUpdate.Size = new Size(340, 45);
            btnUpdate.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.BackColor = Color.FromArgb(230, 126, 34);
            btnUpdate.ForeColor = Color.White;

            btnDelete.Visible = true;
            btnDelete.Parent = inputPanel;
            btnDelete.Text = "🗑️ Delete Medicine";
            btnDelete.Location = new Point(25, 455);
            btnDelete.Size = new Size(340, 45);
            btnDelete.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.BackColor = Color.FromArgb(192, 57, 43);
            btnDelete.ForeColor = Color.White;

            // GRID
            grid.Visible = true;
            grid.Location = new Point(460, 30);
            grid.Size = new Size(540, 550);

            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 248);
            grid.Font = new Font("Segoe UI", 11);
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            grid.ColumnHeadersHeight = 40;

            grid.CellClick += (s, e) =>
            {
                HandleGridSelection(e.RowIndex);
            };
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
           

            DashBoard dashboard = new DashBoard(); dashboard.Show();

    
            this.Hide();
            this.Close();
        }

        private void MoveNextWithEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        void RefreshGrid()
        {
            grid.DataSource = null;
            grid.DataSource = medicines;
            ClearInputs();
            SaveData();
        }

        void ClearInputs()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            selectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int nextId = medicines.Count > 0 ? medicines[medicines.Count - 1].Id + 1 : 1;

                Medicine m = new Medicine()
                {
                    Id = nextId,
                    Name = txtName.Text,
                    Price = double.Parse(txtPrice.Text),
                    Quantity = int.Parse(txtQty.Text)
                };

                medicines.Add(m);
                RefreshGrid();
                MessageBox.Show("Medicine Added Successfully");
            }
            catch
            {
                MessageBox.Show("Invalid Input");
            }
        }

        private void HandleGridSelection(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < medicines.Count)
            {
                selectedIndex = rowIndex;
                txtName.Text = medicines[rowIndex].Name;
                txtPrice.Text = medicines[rowIndex].Price.ToString();
                txtQty.Text = medicines[rowIndex].Quantity.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    medicines[selectedIndex].Name = txtName.Text;
                    medicines[selectedIndex].Price = double.Parse(txtPrice.Text);
                    medicines[selectedIndex].Quantity = int.Parse(txtQty.Text);

                    RefreshGrid();
                    MessageBox.Show("Updated Successfully");
                }
                catch
                {
                    MessageBox.Show("Invalid Input");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                medicines.RemoveAt(selectedIndex);
                RefreshGrid();
                MessageBox.Show("Deleted Successfully");
            }
        }

        private void SaveData()
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(medicines);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);

                    medicines = JsonConvert.DeserializeObject<List<Medicine>>(jsonString);

                    if (medicines == null)
                        medicines = new List<Medicine>();

                    grid.DataSource = null;
                    grid.DataSource = medicines;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
                medicines = new List<Medicine>();
            }
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}