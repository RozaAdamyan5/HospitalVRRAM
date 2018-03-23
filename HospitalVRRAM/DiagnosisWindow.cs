using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalForms
{
    // Very low quality code
    public partial class DiagnosisWindow : Form
    {
        private Dictionary<string, int> reserved = new Dictionary<string, int>();
        //private TableLayoutPanel table = new TableLayoutPanel();

        public DiagnosisWindow(string patientsName)
        {
            InitializeComponent();
            nameEdit.Text += patientsName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Here must be loading of medicine from DB but hence we yet haven't DB here is something
            medicineName.Items.Add("Medicine 1");      reserved["Medicine 1"] = 10;
            medicineName.Items.Add("Medicine 2");      reserved["Medicine 2"] = 5;
            medicineName.Items.Add("Medicine 3");      reserved["Medicine 3"] = 40;
            medicineName.Items.Add("Medicine 4");      reserved["Medicine 4"] = 20;
            medicineName.Items.Add("Medicine 5");      reserved["Medicine 5"] = 4;

            medicineListTable.Controls.Add(new Label() { Text = "No" });
            medicineListTable.Controls.Add(new Label() { Text = "Medicine" });
            medicineListTable.Controls.Add(new Label() { Text = "Count" });

            medicineCount.Enabled = false;
            addMedicine.Enabled = false;

        }

        private void checkDisableEnable(object sender, EventArgs e)
        {
            if (medicineName.SelectedIndex == -1)
            {
                addMedicine.Enabled = false;
                medicineCount.Enabled = false;
            }
            else
            {
                medicineCount.Enabled = true;
                if (Convert.ToInt32(medicineCount.Value) == 0)
                    addMedicine.Enabled = false;
                else
                    addMedicine.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (medicineName.SelectedIndex != -1)
                medicineCount.Maximum = reserved[medicineName.Text];
            checkDisableEnable(sender, e);
        }

        private void AddMedicine_Click(object sender, EventArgs e)
        {
            medicineListTable.Controls.Add(new Label() { Text = medicineListTable.RowCount.ToString() });
            medicineListTable.Controls.Add(new Label() { Text = medicineName.Text });
            medicineListTable.Controls.Add(new Label() { Text = medicineCount.Text });
            medicineName.SelectedIndex = -1;
            medicineCount.Value = 0;
            medicineListTable.RowCount++;
            checkDisableEnable(sender, e);
        }
    }
}
