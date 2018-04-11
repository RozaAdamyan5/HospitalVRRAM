using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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

        public PatientProfileWindow(Patient pat)
        {
            InitializeComponent();
            patient = pat;
        }

        private void PatientProfileWindow_Load(object sender, EventArgs e)
        {
            nameLabel.Text = patient.Name;
            surnameLabel.Text = patient.Surname;
            balanceLabel.Text = patient.Balance.ToString();
            phoneNumberLabel.Text = patient.PhoneNumber;
            birthdateLabel.Text = patient.DateOfBirth.ToShortDateString();
            if (patient.Picture != null)
                profilePicBox.Image = (Bitmap)((new ImageConverter()).ConvertFrom(patient.Picture));
        }

        private void historyButton_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();

            DataGridView historyView = new DataGridView() { ReadOnly = true, BackgroundColor = Color.White, Width = 500, Height = 200 };
            DataTable table = new DataTable("History");
            historyView.DataSource = table;

            DataColumn disease = new DataColumn()   { ColumnName = "Disease", DataType =typeof(string) };
            DataColumn medicine = new DataColumn()  { ColumnName = "Medicine", DataType = typeof(string),  };
            DataColumn date = new DataColumn()      { ColumnName = "Date", DataType = typeof(DateTime) };

            table.Columns.Add(disease);
            table.Columns.Add(medicine);
            table.Columns.Add(date);

            //List<Diagnosis> diagnoses = patient.ShowMyHistory();
            List<Diagnosis> diagnoses = new List<Diagnosis>();
            diagnoses.Add(new Diagnosis("blssssssh", new DateTime(2018, 04, 12), new List<Medicine> { new Medicine("aaaaaa", "aa", 153, new DateTime()), new Medicine("wwwwwwwwwwww", "aa", 15, new DateTime()) }));
            diagnoses.Add(new Diagnosis("aaa", new DateTime(2018, 04, 13), new List<Medicine>()));


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
                            diagnose["Medicine"] += "   |   ";

                        diagnose["Medicine"] += currentMedicine.Name;
       
                    }
                }
                else
                    diagnose["Medicine"] = "None";

                diagnose["Date"] = current.DiagnoseDate;
                table.Rows.Add(diagnose);
            }

            /*for(int i = 0; i < 40; i++)
            {
                DataRow diagnose = table.NewRow();
                diagnose["Disease"] = "aaa";
                diagnose["Medicine"] = "aaaaaa";
                diagnose["Date"] = new DateTime();
                table.Rows.Add(diagnose);
            }*/

            universalPanel.Controls.Add(historyView);

            foreach(DataGridViewColumn col in historyView.Columns)
            {
                col.Width = 152;
            }

            if (historyView.Rows.Count > 6)
                historyView.Height = (500 < 25 * (historyView.Rows.Count + 1) ? 500 : 25 * (historyView.Rows.Count + 1));
        }

        private void registerConsultation_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            // Show registration for consultation form in universalPanel

            List<Doctor> allDoctors = new List<Doctor>() {  new Doctor("Doc1", "ads", 156, "aa", new DateTime(), Convert.ToDecimal(15)),
                                                            new Doctor("Doc2", "abs", 156, "aa", new DateTime(), Convert.ToDecimal(15)),
                                                            new Doctor("Doc4", "acs", 156, "aaa", new DateTime(), Convert.ToDecimal(15))};

            Label consultation = new Label() { Text = "Consultation", Font = new Font("Segoe Print", 13F), Left = 10, Top = 10, Width = 300 };
            ComboBox doctorSelect = new ComboBox() { Font = new Font("Consolas", 11F), Left = 10, Top = 80, Width = 200 };
            foreach(var doc in allDoctors)
            {
                doctorSelect.Items.Add(doc.Name + " " + doc.Surname);
            }
            DateTimePicker dtPick = new DateTimePicker() { Font = new Font("Consolas", 11F), Left = 220, Top = 80, Width = 200, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MMM/yyyy HH:mm" };
            Button requestConsultation = new Button() { Text = "Send Request", Left = 300, Top = 130, Width = 150, Height = 25, Font = new Font("Microsoft Sans Serif", 9.5F) };

            doctorSelect.SelectedIndexChanged += (senderr, ee) => DoctorSelect_SelectedIndexChanged(senderr, ee, requestConsultation, dtPick);
            requestConsultation.Click += (senderr, ee) => RequestConsultation_Click(senderr, ee, allDoctors[doctorSelect.SelectedIndex], doctorSelect, dtPick);

            dtPick.Enabled = requestConsultation.Enabled = false;
            universalPanel.Controls.AddRange(new Control[] { consultation, doctorSelect, dtPick, requestConsultation });
        }

        private void RequestConsultation_Click(object sender, EventArgs e, Doctor doc, ComboBox docComb, DateTimePicker dtPk)
        {
            try
            {
                DateTime perfectTime = doc.FreeTime(dtPk.Value);

                DialogResult res = MessageBox.Show(
                    "Doctor have free time at " + perfectTime.ToString("hh:mm") + @"
Press OK to register for that time, or Cancel and try other time", "Consultation", MessageBoxButtons.OKCancel);

                if(res == DialogResult.OK)
                {
                    // Register Consultation
                    docComb.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    @"This doctor is busy whole day
Please choose other doctor or other day");
            }
        }

        private void DoctorSelect_SelectedIndexChanged(object sender, EventArgs e, Button send, DateTimePicker dtPk)
        {
            if (((ComboBox)sender).SelectedIndex != -1)
            {
                send.Enabled = true;
                dtPk.Enabled = true;
            }
            else
            {
                send.Enabled = false;
                dtPk.Enabled = false;
            }
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();

            var tmpFont = new Font("Microsoft Sans Serif", 10F);

            Label oldPasswordLabel = new Label() { Text = "Old Password", Width = 140, Left = 20, Top = 22, Font = tmpFont };
            Label newPasswordLabel = new Label() { Text = "New Password", Width = 140, Left = 20, Top = 52, Font = tmpFont };
            Label confirmPasswordLabel = new Label() { Text = "Confirm Password", Width = 140, Left = 20, Top = 82, Font = tmpFont };
            TextBox oldPassword = new TextBox() { Width = 180, Height = 23, Left = 160, Top = 20, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password, Font = tmpFont };
            TextBox newPassword = new TextBox() { Width = 180, Height = 23, Left = 160, Top = 50, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password, Font = tmpFont };
            TextBox confirmPassword = new TextBox() { Width = 180, Height = 23, Left = 160, Top = 80, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password, Font = tmpFont };
            Button saveButton = new Button() { Text = "Save", Left = 260, Top = 110, Font = tmpFont, Height = 30 };

            universalPanel.Controls.AddRange(
                new Control[] { oldPasswordLabel, oldPassword, newPasswordLabel, newPassword, confirmPasswordLabel, confirmPassword, saveButton });
        }

        private void addPicture_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog selectPicture = new OpenFileDialog() { InitialDirectory = "C:\\", Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG" };
            if (selectPicture.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = selectPicture.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            profilePicBox.Image = new Bitmap(myStream);
                            char[] chArr = (Convert.ToString(myStream)).ToCharArray();
                            // doctor.AddPicture(Array.ConvertAll(chArr, Convert.ToByte));
                            profilePicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            addPicture.Text = "Change Picture"; addPicture.Left = 45;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void PatientProfileWindow_Paint(object sender, PaintEventArgs e)
        {
            if (this.ClientRectangle.IsEmpty)
                return;
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                               Color.GhostWhite,
                                                               Color.SteelBlue,
                                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void PatientProfileWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
