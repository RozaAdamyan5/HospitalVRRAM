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
        Doctor doctor;

        public DoctorProfileWindow(Doctor doc)
        {
            InitializeComponent();
            doctor = doc;
            nameLabel.Text = doctor.Name;
            surnameLabel.Text = doctor.Surname;
            balanceLabel.Text = doctor.Balance.ToString();
            phoneNumberLabel.Text = doctor.PhoneNumber;
            birthdateLabel.Text = doctor.GetEmployed.ToShortDateString();
            if (doctor.Picture != null)
                profilePicBox.Image = (Bitmap)((new ImageConverter()).ConvertFrom(doctor.Picture));

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

            for(int i = 0; i < monthStartDay; i++)  calendar.Controls.Add(new Label() { Text = "", Width = 30, Height = 30, TextAlign = ContentAlignment.MiddleRight });

            for (int i = 0; i < countOfDays; i++)
            {
                var lbl = new Label() { Text = (i + 1).ToString(), Width = (i > 8 ? 41 : 37), Height = 30, TextAlign = ContentAlignment.MiddleRight, Font = calendarFont };
                calendar.Controls.Add(lbl);
                lbl.Click += (sender, e) => showPatientsOfSpecifiedDay(sender, e, new DateTime(currentMonth.Year, currentMonth.Month, int.Parse(lbl.Text)));
            }
            if (monthStartDay + countOfDays <= 35) calendar.Height -= 25;
        }

        private void DoctorProfileWindow_Load(object sender, EventArgs e) {
            // Maybe loading data about doctor's patients from DB
        }

        private void showPatientsOfSpecifiedDay(object sender, EventArgs e, DateTime date)
        {
            patientsOfDay.Controls.Clear();
            patientsOfDay.RowCount = 1;
            //var calendarInfo = doctor.Calendar();
            Dictionary<DateTime, Patient> calendarInfo = new Dictionary<DateTime, Patient>();
            Patient tmp = new Patient("Name", "Surname", 6514651, "Ggg", DateTime.Today);

            calendarInfo[new DateTime(2018, 4, 1, 15, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 1, 16, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 1, 18, 20, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 1, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 2, 0, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 1, 20, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 2, 10, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 3, 10, 0)] = tmp;
            calendarInfo[new DateTime(2018, 4, 10, 4, 05, 0)] = tmp;

            foreach (var info in calendarInfo)
            {
                if (info.Key.Date.Equals(date.Date))
                {
                    patientsOfDay.Controls.Add(new Label() { Text = patientsOfDay.RowCount.ToString() });
                    patientsOfDay.Controls.Add(new Label() { Text = (info.Value.Name + " " + info.Value.Surname), Width = 220 });
                    patientsOfDay.Controls.Add(new Label() { Text = (info.Key.ToShortTimeString()) });
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
                            char[] chArr = (Convert.ToString(myStream)).ToCharArray();
                            // doctor.AddPicture(Array.ConvertAll(chArr, Convert.ToByte));
                            profilePicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            addPicture.Text = "Change Picture";         addPicture.Left -= 7;
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
    }
}
