using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalClasses;

namespace HospitalForms
{
    public partial class PatientProfileWindow : Form
    {
        Patient patient;

        public PatientProfileWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
        }

        private void PatientProfileWindow_Load(object sender, EventArgs e)
        {
            nameLabel.Text = patient.Name;
            surnameLabel.Text = patient.Surname;
            balanceLabel.Text = patient.Balance.ToString();
            phoneNumberLabel.Text = patient.PhoneNumber;
            birthdateLabel.Text = patient.DateOfBirth.ToString();
            if (patient.Picture != null)
                profilePicBox.Image = (Bitmap)((new ImageConverter()).ConvertFrom(patient.Picture));
        }

        private void historyButton_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;

            DataGridView historyView = new DataGridView() { DefaultCellStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True },
                                                            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                                                            ReadOnly = true};
            DataSet set = new DataSet();
            DataTable table = new DataTable("History");
            set.Tables.Add(table);
            historyView.DataSource = set.Tables["History"];

            DataColumn disease = new DataColumn()   { ColumnName = "Disease", DataType =typeof(string) };
            DataColumn medicine = new DataColumn()  { ColumnName = "Medicine", DataType = typeof(string),  };
            DataColumn date = new DataColumn()      { ColumnName = "Date", DataType = typeof(DateTime) };

            table.Columns.Add(disease);
            table.Columns.Add(medicine);
            table.Columns.Add(date);

            //List<Diagnosis> diagnoses = patient.ShowMyHistory();
            List<Diagnosis> diagnoses = new List<Diagnosis>();
            diagnoses.Add(new Diagnosis("blssssssh", new DateTime(), new List<Medicine> { new Medicine("aaaaaa", "aa", 153, new DateTime()), new Medicine("wwwwwwwwwwww", "aa", 15, new DateTime()) }));
            diagnoses.Add(new Diagnosis("aaa", new DateTime(), new List<Medicine>()));


            foreach (var current in diagnoses)
            {
                DataRow diagnose = table.NewRow();
                diagnose["Disease"] = current.Disease;
                if (current.PrescribedMedicines.Count != 0)
                {
                    foreach (var currentMedicine in current.PrescribedMedicines)
                    {
                        if (currentMedicine == current.PrescribedMedicines.ElementAt(0))
                            diagnose["Medicine"] = "";
                        else
                            diagnose["Medicine"] += Environment.NewLine;

                        diagnose["Medicine"] += currentMedicine.Name;
       
                    }
                }
                else
                    diagnose["Medicine"] = "None";

                diagnose["Date"] = current.DiagnoseDate.ToShortDateString();
                table.Rows.Add(diagnose);
            }

            universalPanel.Controls.Add(historyView);
            historyView.Dock = DockStyle.Fill;
            historyView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        }

        private void registerConsultation_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;
            // TODO
            // Show registration for consultation form in universalPanel
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.None;
            universalPanel.BackColor = DefaultBackColor;

            Label oldPasswordLabel = new Label() { Text = "Old Password", Width = 100, Left = 20, Top = 22 };
            Label newPasswordLabel = new Label() { Text = "New Password", Width = 100, Left = 20, Top = 52 };
            Label confirmPasswordLabel = new Label() { Text = "Confirm Password", Width = 100, Left = 20, Top = 82 };
            TextBox oldPassword = new TextBox() { Width = 150, Height = 20, Left = 140, Top = 20, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
            TextBox newPassword = new TextBox() { Width = 150, Height = 20, Left = 140, Top = 50, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
            TextBox confirmPassword = new TextBox() { Width = 150, Height = 20, Left = 140, Top = 80, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
            Button saveButton = new Button() { Text = "Save", Left = 215, Top = 110 };

            universalPanel.Controls.AddRange(
                new Control[] { oldPasswordLabel, oldPassword, newPasswordLabel, newPassword, confirmPasswordLabel, confirmPassword, saveButton });
        }
    }
}
