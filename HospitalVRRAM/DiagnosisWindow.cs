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
        List<string> prescribed = new List<string>();

        public DiagnosisWindow(string patientsName)
        {
            InitializeComponent();
            fullName.Text = patientsName;
            removeIcon = new Bitmap((Bitmap)(global::HospitalVRRAM.Properties.Resources.fileclose), new Size(20, 20));
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

            checkDisableEnable(sender, e);

        }

        private void loadTable()
        {
            medicineListTable.Controls.Add(new Label() { Text = "No",  });
            medicineListTable.Controls.Add(new Label() { Text = "Medicine" });
            medicineListTable.Controls.Add(new Label() { Text = " " });
        }

        private void checkDisableEnable(object sender, EventArgs e)
        {
            addMedicine.Enabled = medicineName.SelectedIndex != -1;
        }

        private void AddMedicine_Click(object sender, EventArgs e)
        {
            prescribed.Add(medicineName.Text);
            medicineName.Items.RemoveAt(medicineName.SelectedIndex);
            updateTable();
            medicineName.SelectedIndex = -1;
            medicineListTable.RowCount++;
            checkDisableEnable(sender, e);
        }

        private void updateTable()
        {
            medicineListTable.Controls.Clear();
            loadTable();

            foreach(var row in prescribed)
            {
                medicineListTable.Controls.Add(new Label() { Text = (prescribed.IndexOf(row) + 1).ToString()});
                medicineListTable.Controls.Add(new Label() { Text = row });
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
