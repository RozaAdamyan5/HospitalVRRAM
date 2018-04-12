using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Resources;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalClasses;
using System.Drawing.Drawing2D;

namespace HospitalForms
{
    // Very low quality code
    public partial class DiagnosisWindow : Form
    {
        static Bitmap removeIcon;
        Doctor doctor;
        Patient patient;
        List< Tuple<string, int> > prescribed = new List<Tuple<string, int> >();

        public DiagnosisWindow(string patientsName)
        {
            InitializeComponent();
            fullName.Text = patientsName;
            removeIcon = new Bitmap((global::HospitalVRRAM.Properties.Resources.fileclose), new Size(20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Here must be loading of medicine from DB but hence we yet haven't DB here is something

            medicineName.Items.Add("Medicine 1");
            medicineName.Items.Add("Medicine 2");
            medicineName.Items.Add("Medicine 3");
            medicineName.Items.Add("Medicine 4");
            medicineName.Items.Add("Medicine 5");

            loadTable();

            checkDisableEnable(0, EventArgs.Empty);

        }

        private void loadTable()
        {
            medicineListTable.Controls.Add(new Label() { Text = "No",  });
            medicineListTable.Controls.Add(new Label() { Text = "Medicine" });
            medicineListTable.Controls.Add(new Label() { Text = "Count" });
            medicineListTable.Controls.Add(new Label() { Text = " " });
        }

        private void checkDisableEnable(object sender, EventArgs e)
        {
            if (medicineName.SelectedIndex != -1)
            {
                medicineCount.Enabled = true;
                if (Convert.ToInt32(medicineCount.Value) == 0)
                    addMedicine.Enabled = false;
                else
                    addMedicine.Enabled = true;
            }
            else
            {
                addMedicine.Enabled = medicineCount.Enabled = false;
            }
            
        }

        private void AddMedicine_Click(object sender, EventArgs e)
        {
            prescribed.Add(new Tuple<string, int>(medicineName.Text, Convert.ToInt32(medicineCount.Value)));

            medicineName.Items.RemoveAt(medicineName.SelectedIndex);
            updateTable();
            medicineName.SelectedIndex = -1;
            medicineCount.Value = 0;
            medicineListTable.RowCount++;

            checkDisableEnable(0, EventArgs.Empty);
        }

        private void updateTable()
        {
            medicineListTable.Controls.Clear();
            loadTable();

            foreach(var row in prescribed)
            {
                medicineListTable.Controls.Add(new Label() { Text = (prescribed.IndexOf(row) + 1).ToString()});
                medicineListTable.Controls.Add(new Label() { Text = row.Item1 });
                medicineListTable.Controls.Add(new Label() { Text = row.Item2.ToString() });
                Label removeLabel = new Label() { Image = removeIcon };
                removeLabel.Click += (sender, e) => deleteClicked(sender, e, prescribed.IndexOf(row));
                medicineListTable.Controls.Add(removeLabel);
            }
        }

        private void deleteClicked(object sender, EventArgs e, int index)
        {
            prescribed.RemoveAt(index);

            updateTable();
        }

        private void DiagnosisWindow_Paint(object sender, PaintEventArgs e)
        {
            if (this.ClientRectangle.IsEmpty)
                return;
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                               Color.GhostWhite,
                                                               Color.LightGreen,
                                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void DiagnosisWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
