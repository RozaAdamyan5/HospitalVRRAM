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
        public event EventHandler backToDoctorProfile;

        static Bitmap removeIcon;
        Doctor doctor;
        Patient patient;
        List< Tuple<string, int> > prescribed = new List<Tuple<string, int> >();

        public DiagnosisWindow(Doctor doc, Patient pat)
        {
            InitializeComponent();
            doctor = doc;
            patient = pat;
            removeIcon = new Bitmap((global::HospitalVRRAM.Properties.Resources.fileclose), new Size(20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Here must be loading of medicine from DB but hence we yet haven't DB here is something
            fullName.Text = patient.Name + " " + patient.Surname;

            List<string> medNames = doctor.GetMedicines();
            foreach (var name in medNames) {
                medicineName.Items.Add(name);
            }

            checkDisableEnable(0, EventArgs.Empty);
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
            medicineName.Items.Add(prescribed[index].Item1);
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

        private void patientHistory_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();

            DataGridView historyView = new DataGridView() { ReadOnly = true, BackgroundColor = Color.White, Width = 500, Height = 200 };
            DataTable table = new DataTable("History");
            historyView.DataSource = table;

            DataColumn disease = new DataColumn() { ColumnName = "Disease", DataType = typeof(string) };
            DataColumn medicine = new DataColumn() { ColumnName = "Medicine", DataType = typeof(string), };
            DataColumn date = new DataColumn() { ColumnName = "Date", DataType = typeof(DateTime) };

            table.Columns.Add(disease);
            table.Columns.Add(medicine);
            table.Columns.Add(date);

            List<Diagnosis> diagnoses = patient.ShowMyHistory();

            if (diagnoses != null)
            {
                foreach (var current in diagnoses)
                {
                    DataRow diagnose = table.NewRow();
                    diagnose["Disease"] = current.Disease;
                    if (current.PrescribedMedicines.Count != 0)
                    {
                        bool start = true;
                        foreach (var currentMedicine in current.PrescribedMedicines)
                        {
                            if (start)
                                diagnose["Medicine"] = "";
                            else
                                diagnose["Medicine"] += "   |   ";

                            diagnose["Medicine"] += currentMedicine.Key.Name + "(" + currentMedicine.Value.ToString() + ")";
                            start = false;
                        }
                    }
                    else
                        diagnose["Medicine"] = "None";

                    diagnose["Date"] = current.DiagnoseDate;
                    table.Rows.Add(diagnose);
                }
            }
            

            universalPanel.Controls.Add(historyView);

            foreach (DataGridViewColumn col in historyView.Columns)
            {
                col.Width = 152;
            }

            if (historyView.Rows.Count > 6)
                historyView.Height = (350 < 25 * (historyView.Rows.Count + 1) ? 500 : 25 * (historyView.Rows.Count + 1));
        }

        private void finish_Click(object sender, EventArgs e)
        {
            Dictionary<Medicine, int> medicines = new Dictionary<Medicine, int>();

            foreach(var item in prescribed)
            {
                medicines[Medicine.GetMedicine(item.Item1)] = item.Item2;
            }

            Diagnosis diagnosis = new Diagnosis(diseaseBox.Text, DateTime.Now, medicines);

            backToDoctorProfile(this, EventArgs.Empty);
        }
    }
}
