using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalClasses;

namespace HospitalForms
{
    public partial class DoctorProfileWindow : Form
    {
        public event EventHandler logOutClicked;
        
        public Doctor doctor;

        public DoctorProfileWindow(Doctor doc)
        {
            InitializeComponent();
            doctor = doc;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Bitmap byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Bitmap returnImage = new Bitmap(ms);
            return returnImage;
        }

        private void DoctorProfileWindow_Load(object sender, EventArgs e) {
            nameLabel.Text = doctor.Name;
            surnameLabel.Text = doctor.Surname;
            balanceLabel.Text = doctor.Balance.ToString();
            phoneNumberLabel.Text = doctor.PhoneNumber;
            birthdateLabel.Text = doctor.GetEmployed.ToShortDateString();
            if (doctor.Picture.Length > 4)
            {
                addPicture.Text = "Change Picture"; addPicture.Left = 60;
                profilePicBox.Image = byteArrayToImage(doctor.Picture);
            }

            calendarPanel.Hide();

            calendar.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            string[] daysOfWeek = new string[7] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            Font calendarFont = new Font("Consolas", 11F, FontStyle.Bold);
            Font calendarHearderFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);

            foreach (var arg in daysOfWeek)
                calendar.Controls.Add(new Label() { Text = arg, Width = 46, Height = 30, TextAlign = ContentAlignment.MiddleRight, Font = calendarHearderFont });

            var currentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            currentDate.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth.Month) + " " + currentMonth.Year.ToString();

            int monthStartDay = (int)currentMonth.DayOfWeek;
            int countOfDays = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);

            for (int i = 0; i < monthStartDay; i++) calendar.Controls.Add(new Label() { Text = "", Width = 30, Height = 30, TextAlign = ContentAlignment.MiddleRight });

            for (int i = 0; i < countOfDays; i++)
            {
                var lbl = new Label() { Text = (i + 1).ToString(), Width = (i > 8 ? 41 : 37), Height = 30, TextAlign = ContentAlignment.MiddleRight, Font = calendarFont };
                calendar.Controls.Add(lbl);
                lbl.Click += (senderr, ee) => showPatientsOfSpecifiedDay(senderr, ee, new DateTime(currentMonth.Year, currentMonth.Month, int.Parse(lbl.Text)));
            }
            if (monthStartDay + countOfDays <= 35) calendar.Height -= 25;
        }

        private void showPatientsOfSpecifiedDay(object sender, EventArgs e, DateTime date)
        {
            patientsOfDay.Controls.Clear();
            patientsOfDay.RowCount = 1;
            //var calendarInfo = doctor.Calendar();
            Dictionary<DateTime, Patient> calendarInfo = new Dictionary<DateTime, Patient>();
            Patient tmp = new Patient("Name", "Surname", "aaaaaaa", "Ggg", DateTime.Today);

            calendarInfo[new DateTime(2018, 4, 1, 15, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 1, 16, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 1, 18, 20, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 1, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 2, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 11, 0, 20, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 11, 0, 40, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 11, 1, 20, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 11, 4, 05, 0)] = tmp;

            foreach (var info in calendarInfo)
            {
                if (info.Key.Date.Equals(date.Date))
                {
                    patientsOfDay.Controls.Add(new Label() { Text = patientsOfDay.RowCount.ToString() });
                    patientsOfDay.Controls.Add(new Label() { Text = (info.Value.Name + " " + info.Value.Surname), Width = 190 });
                    patientsOfDay.Controls.Add(new Label() { Text = (info.Key.ToShortTimeString() + "-" + info.Key.AddMinutes(20).ToShortTimeString()), Width = 140 });
                    Label status = new Label() { Width = 80 };
                    if (DateTime.Now > info.Key.AddMinutes(20))
                    {
                        status.Text = "Served";
                        status.ForeColor = Color.Green;
                    }
                    else if (DateTime.Now > info.Key)
                    {
                        status.Text = "Waiting...";
                        status.ForeColor = Color.Gray;
                        //status.Click += Function that links to diagnose writing window
                    }
                    else
                    {
                        status.Text = "Not Yet";
                        status.ForeColor = Color.Black;
                    }
                    patientsOfDay.Controls.Add(status);
                    patientsOfDay.RowCount++;
                }
            }
        }

        private void calendarButton_Click(object sender, EventArgs e)
        {
            calendarPanel.Show();
            calendarPanel.BringToFront();
        }

        private void myPatients_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            // TODO
            // Show patients table in universalPanel

            universalPanel.Controls.Clear();
            
            DataGridView servedPatients = new DataGridView() { ReadOnly = true, BackgroundColor = Color.White, Width = 500, Height = 200 };
            DataTable table = new DataTable("Patients");
            servedPatients.DataSource = table;

            DataColumn name = new DataColumn() { ColumnName = "Name", DataType = typeof(string) };
            DataColumn surname = new DataColumn() { ColumnName = "Surname", DataType = typeof(string) };
            DataColumn passId = new DataColumn() { ColumnName = "Passport ID", DataType = typeof(string) };
            DataColumn address = new DataColumn() { ColumnName = "Address", DataType = typeof(string) };
            DataColumn dateOfBirth = new DataColumn() { ColumnName = "Date Of Birth", DataType = typeof(DateTime) };

            table.Columns.Add(name);
            table.Columns.Add(surname);
            table.Columns.Add(passId);
            table.Columns.Add(address);
            table.Columns.Add(dateOfBirth);

            //List<Patient> patients = doctor.ShowPatient();
            List<Patient> patients = new List<Patient>() {
                new Patient("Aa", "Aaa", "aaa", "Aaaa", DateTime.Now), new Patient("Bb", "BBs", "aaaa", "AD", DateTime.Now)};
            

            foreach (var current in patients)
            {
                DataRow patient = table.NewRow();
                patient["Name"] = current.Name;
                patient["Surname"] = current.Surname;
                patient["Passport ID"] = current.PassportID;
                patient["Address"] = current.Address;
                patient["Date Of Birth"] = current.DateOfBirth;

                table.Rows.Add(patient);
            }

            /*for(int i = 0; i < 40; i++)
            {
                DataRow diagnose = table.NewRow();
                diagnose["Disease"] = "aaa";
                diagnose["Medicine"] = "aaaaaa";
                diagnose["Date"] = new DateTime();
                table.Rows.Add(diagnose);
            }*/

            universalPanel.Controls.Add(servedPatients);

            foreach (DataGridViewColumn col in servedPatients.Columns)
            {
                col.Width = 152;
            }

            if (servedPatients.Rows.Count > 6)
                servedPatients.Height = (500 < 25 * (servedPatients.Rows.Count + 1) ? 500 : 25 * (servedPatients.Rows.Count + 1));
            
            universalPanel.BringToFront();
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            universalPanel.BringToFront();

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

                            doctor.AddPicture(imageToByteArray(new Bitmap(myStream)));
                            addPicture.Text = "Change Picture"; addPicture.Left = 60;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void DoctorProfileWindow_Paint(object sender, PaintEventArgs e)
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

        private void DoctorProfileWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            logOutClicked(this, EventArgs.Empty);
        }
    }
}
