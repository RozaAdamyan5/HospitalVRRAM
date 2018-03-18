using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalWithDB//
{
    // Very low quality code
    public partial class DiagnosisWindow : Form
    {
        private Dictionary<string, int> reserved = new Dictionary<string, int>();
        private TableLayoutPanel table = new TableLayoutPanel();

        public DiagnosisWindow(string patientsName)
        {
            InitializeComponent();
            label1.Text += ": " + patientsName;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // Here must be loading of medicine from DB but hence we yet haven't DB here is something
            comboBox1.Items.Add("Medicine 1");      reserved["Medicine 1"] = 10;
            comboBox1.Items.Add("Medicine 2");      reserved["Medicine 2"] = 5;
            comboBox1.Items.Add("Medicine 3");      reserved["Medicine 3"] = 40;
            comboBox1.Items.Add("Medicine 4");      reserved["Medicine 4"] = 20;
            comboBox1.Items.Add("Medicine 5");      reserved["Medicine 5"] = 4;

            panel1.Controls.Add(table);

            table.Font = new Font("Consolas", 9.5F);
            table.ColumnCount = 3;
            table.RowCount = 1;

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, AddNewMedicine.Width / 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 7 * AddNewMedicine.Width / 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, AddNewMedicine.Width / 4));

            table.Controls.Add(new Label() { Text = "No" }, 0, 0);
            table.Controls.Add(new Label() { Text = "Medicine" }, 1, 0);
            table.Controls.Add(new Label() { Text = "Count" }, 2, 0);
            table.Width = AddNewMedicine.Width;
            table.AutoSize = true;

            numericUpDown1.Enabled = false;
            AddMedicine.Enabled = false;
        }

        private void checkDisableEnable(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                AddMedicine.Enabled = false;
                numericUpDown1.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
                if (Convert.ToInt32(numericUpDown1.Value) == 0)
                {
                    AddMedicine.Enabled = false;
                }
                else
                    AddMedicine.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = reserved[comboBox1.Text];
            checkDisableEnable(sender, e);
        }

        private void AddMedicine_Click(object sender, EventArgs e)
        {
            table.Controls.Add(new Label() { Text = table.RowCount.ToString() }, 0, table.RowCount);
            table.Controls.Add(new Label() { Text = comboBox1.Text }, 1, table.RowCount);
            table.Controls.Add(new Label() { Text = numericUpDown1.Text }, 2, table.RowCount);
            table.RowCount++;
        }
    }
}
