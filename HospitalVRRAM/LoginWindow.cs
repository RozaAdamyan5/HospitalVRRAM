using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalClasses;
using HospitalForms;


namespace HospitalForms
{
    public partial class LoginWindow : Form
    {
        public event EventHandler<PatientPassEventArgs> patientSignedIn;
        public event EventHandler<DoctorPassEventArgs> doctorSignedIn;
        public event EventHandler<AdminPassEventArgs> adminIsHere;
        public event EventHandler signUpClicked;

        public LoginWindow()
        {
            InitializeComponent();
            passwordBox.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (showPassword.Checked)
                passwordBox.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
            else
                passwordBox.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;

        }

        private void LoginWindow_Paint(object sender, PaintEventArgs e)
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

        private void LoginWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            signUpClicked(this, EventArgs.Empty);
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            if (loginSuffix.SelectedIndex == -1)
            {
                MessageBox.Show("Please specify your account type by login suffix");
                return;
            }
            if (loginSuffix.SelectedIndex == 0)             // Patient
            {
                try
                {
                    Patient patient = Patient.SignIn(loginBox.Text, passwordBox.Text);
                    if (patient != null)
                    {
                        patientSignedIn(this, new PatientPassEventArgs(patient));
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            if (loginSuffix.SelectedIndex == 1)             // Doctor
            {
                try
                {
                    Doctor doctor = Doctor.SignIn(loginBox.Text, passwordBox.Text);
                    if (doctor != null)
                    {
                        doctorSignedIn(this, new DoctorPassEventArgs(doctor));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            if (loginSuffix.SelectedIndex == 2)
            {
                try
                {
                    Admin admin = null;
                    admin = Admin.signIn(loginBox.Text, passwordBox.Text);
                    if (admin != null)
                    {
                        adminIsHere(this, new AdminPassEventArgs(admin));
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        public void clearFields()
        {
            loginBox.Text = passwordBox.Text = "";
            loginSuffix.SelectedIndex = -1;
        }
    }

    public class PatientPassEventArgs : EventArgs
    {
        public readonly Patient patient;
        public PatientPassEventArgs(Patient pat)
        {
            patient = pat;
        }
    }

    public class DoctorPassEventArgs : EventArgs
    {
        public readonly Doctor doctor;
        public DoctorPassEventArgs(Doctor doc)
        {
            doctor = doc;
        }
    }

    public class AdminPassEventArgs : EventArgs
    {
        public readonly Admin admin;
        public AdminPassEventArgs(Admin adm)
        {
            admin = adm;
        }
    }
}
