using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        string picturePath = "";

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
        }

        private void DoctorProfileWindow_Load(object sender, EventArgs e) {
            // Maybe loading data about doctor's patients from DB
        }

        private void calendarButton_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;
            // TODO
            // Show calendar in universalPanel
        }

        private void myPatients_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;
            // TODO
            // Show patients table in universalPanel
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
            TextBox oldPassword =       new TextBox() { Width = 150, Height = 20, Left = 140, Top = 20, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };   
            TextBox newPassword =       new TextBox() { Width = 150, Height = 20, Left = 140, Top = 50, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
            TextBox confirmPassword =   new TextBox() { Width = 150, Height = 20, Left = 140, Top = 80, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
            Button saveButton = new Button() { Text = "Save", Left = 215, Top = 110 };

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
                            addPicture.Text = "Change Picture";         addPicture.Left = 45;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
