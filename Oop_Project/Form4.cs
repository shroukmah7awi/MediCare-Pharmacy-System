using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Oop_Project

{
    public partial class Form4 : Form

    {

        List<Medicine> medicines = new List<Medicine>();

        private readonly string filePath = Path.Combine(Application.StartupPath, "medicines.json");



        Panel salesPanel;

        ComboBox cmbMedicines;

        TextBox txtQuantity;

        Label lblTotalPrice;

        Label lblStockStatus;

        Button btnBill;



        public Form4()

        {

            InitializeComponent();

            ApplyModernSalesDesign();

            LoadMedicines();

        }



        private void ApplyModernSalesDesign()

        {

            this.Text = "MediCare System - Sales & Billing";

            this.Size = new Size(1050, 650);

            this.StartPosition = FormStartPosition.CenterScreen;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;



            try

            {

                this.BackgroundImage = Image.FromFile("a2.png");

                this.BackgroundImageLayout = ImageLayout.Stretch;

            }

            catch

            {

                this.BackColor = Color.White;

            }



            salesPanel = new Panel

            {

                Size = new Size(550, 480),

                Location = new Point((this.ClientSize.Width - 550) / 2, (this.ClientSize.Height - 480) / 2),

                BackColor = Color.FromArgb(230, Color.AliceBlue)

            };

            this.Controls.Add(salesPanel);



            Label lblTitle = new Label

            {

                Text = "💳 Quick Billing System",

                Font = new Font("Segoe UI", 20, FontStyle.Bold),

                ForeColor = Color.FromArgb(41, 128, 185),

                Location = new Point(30, 20),

                Size = new Size(490, 40),

                TextAlign = ContentAlignment.MiddleCenter

            };



            Label lblSelect = new Label { Text = "Select Medicine:", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(50, 90), AutoSize = true };

            cmbMedicines = new ComboBox { Location = new Point(50, 120), Size = new Size(450, 35), Font = new Font("Segoe UI", 12), DropDownStyle = ComboBoxStyle.DropDownList };

            cmbMedicines.SelectedIndexChanged += CmbMedicines_SelectedIndexChanged;



            lblStockStatus = new Label { Text = "Available Stock: 0", Font = new Font("Segoe UI", 10, FontStyle.Italic), ForeColor = Color.DarkOrange, Location = new Point(50, 160), AutoSize = true };



            Label lblQty = new Label { Text = "Enter Quantity:", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(50, 200), AutoSize = true };

            txtQuantity = new TextBox { Location = new Point(50, 230), Size = new Size(450, 35), Font = new Font("Segoe UI", 12) };

            txtQuantity.TextChanged += TxtQuantity_TextChanged;



            lblTotalPrice = new Label { Text = "Total Price: 0.00 EGP", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(39, 174, 96), Location = new Point(50, 290), Size = new Size(450, 40), TextAlign = ContentAlignment.MiddleCenter };



            btnBill = new Button

            {

                Text = "🛒 Confirm & Print Invoice",

                Location = new Point(50, 350),

                Size = new Size(450, 50),

                Font = new Font("Segoe UI", 12, FontStyle.Bold),

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.FromArgb(39, 174, 96),

                ForeColor = Color.White,

                Cursor = Cursors.Hand

            };

            btnBill.FlatAppearance.BorderSize = 0;

            btnBill.Click += BtnBill_Click;



            Button btnBack = new Button

            {

                Text = "⬅️ Back to Dashboard",

                Location = new Point(50, 415),

                Size = new Size(450, 40),

                Font = new Font("Segoe UI", 10, FontStyle.Bold),

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.Gray,

                ForeColor = Color.White,

                Cursor = Cursors.Hand

            };

            btnBack.FlatAppearance.BorderSize = 0;

            btnBack.Click += (s, e) => {

                DashBoard d = new DashBoard();

                d.Show();

                this.Close();

            };



            salesPanel.Controls.AddRange(new Control[] { lblTitle, lblSelect, cmbMedicines, lblStockStatus, lblQty, txtQuantity, lblTotalPrice, btnBill, btnBack });

        }



        private void LoadMedicines()

        {

            try

            {

                if (File.Exists(filePath))

                {

                    string jsonString = File.ReadAllText(filePath);

                    medicines = JsonConvert.DeserializeObject<List<Medicine>>(jsonString);



                    if (medicines == null)

                        medicines = new List<Medicine>();



                    cmbMedicines.DataSource = null;

                    cmbMedicines.DataSource = medicines;

                    cmbMedicines.DisplayMember = "Name";

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show("Error loading medicines: " + ex.Message);

            }

        }



        private void CmbMedicines_SelectedIndexChanged(object sender, EventArgs e)

        {

            UpdatePriceAndStock();

        }



        private void TxtQuantity_TextChanged(object sender, EventArgs e)

        {

            UpdatePriceAndStock();

        }



        private void UpdatePriceAndStock()

        {

            if (cmbMedicines.SelectedItem is Medicine selectedMed)

            {

                lblStockStatus.Text = $"Available Stock: {selectedMed.Quantity}";



                if (int.TryParse(txtQuantity.Text, out int qty) && qty > 0)

                {

                    double total = selectedMed.Price * qty;

                    lblTotalPrice.Text = $"Total Price: {total:F2} EGP";

                }

                else

                {

                    lblTotalPrice.Text = "Total Price: 0.00 EGP";

                }

            }

        }



        private void BtnBill_Click(object sender, EventArgs e)

        {

            if (cmbMedicines.SelectedItem is Medicine selectedMed)

            {

                if (int.TryParse(txtQuantity.Text, out int qty) && qty > 0)

                {

                    if (qty <= selectedMed.Quantity)

                    {

                        selectedMed.Quantity -= qty;



                        try

                        {

                            string jsonString = JsonConvert.SerializeObject(medicines);

                            File.WriteAllText(filePath, jsonString);



                            MessageBox.Show($"Invoice Created Successfully!\n\n" +

                                            $"🔹 Medicine: {selectedMed.Name}\n" +

                                            $"🔹 Quantity Sold: {qty}\n" +

                                            $"🔹 Total Paid: {selectedMed.Price * qty:F2} EGP",

                                            "MediCare Billing System",

                                            MessageBoxButtons.OK,

                                            MessageBoxIcon.Information);



                            txtQuantity.Clear();

                            UpdatePriceAndStock();

                        }

                        catch (Exception ex)

                        {

                            MessageBox.Show("Error saving update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }

                    else

                    {

                        MessageBox.Show($"Sorry, Not enough stock available!\nMaximum you can buy is {selectedMed.Quantity}", "Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }

                else

                {

                    MessageBox.Show("Please enter a valid numeric quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }

        }

    }

}