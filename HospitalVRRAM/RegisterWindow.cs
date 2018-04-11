using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalClasses;
using HospitalForms;

namespace HospitalForms
{
    public partial class RegisterWindow : Form
    {
        public event EventHandler registrationCompleted;
        public event EventHandler goBackClicked;

        public RegisterWindow()
        {
            InitializeComponent();
            password.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            confirmPassword.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            
        }

        private void RegisterWindow_Paint(object sender, PaintEventArgs e)
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

        private void RegisterWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void checkIfValid()
        {
            if (name.Text.Length == 0 || surname.Text.Length == 0 || passportID.Text.Length == 0 ||
                phoneNumber.Text.Length == 0 || login.Text.Length == 0 || confirmPassword.Text.Length == 0 || password.Text.Length == 0 || passMatch.Visible)
                throw new Exception("Some fields are required!");

            if (birthDate.Value > DateTime.Now)
            {
                throw new Exception("Date of birth must be past! Not future.");
            }
            if (!(new Regex("[0-9]+").IsMatch(passportID.Text)))
            {
                throw new Exception("Passport ID must be an integer!");
            }

            /*try
            {
                User.PassportIdIsUnique(int.Parse(passportID.Text));
            }
            catch(Exception ex)
            {
                throw ex;
            }*/

            try
            {
                User.LoginIsValid(login.Text + "@pat.hosp");
            }
            catch(Exception ex)
            {
                throw ex;
            }

            try
            {
                User.PasswordIsValid(password.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            try
            {
                checkIfValid();
                new Patient(name.Text, surname.Text, passportID.Text, login.Text, password.Text, address.Text, insuranceCard.Text, birthDate.Value, phoneNumber.Text);
                registrationCompleted(this, EventArgs.Empty);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning"); 
            }
        }

        private void confirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (!confirmPassword.Text.Equals(password.Text))
            {
                passMatch.Visible = true;
            }
            else
                passMatch.Visible = false;
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            if (confirmPassword.Text.Length != 0) confirmPassword_TextChanged(sender, e);
        }

        private void back_Click(object sender, EventArgs e)
        {
            goBackClicked(this, EventArgs.Empty);
        }

        public void clearFields()
        {
            name.Text = surname.Text = passportID.Text = password.Text = confirmPassword.Text =
            login.Text = address.Text = phoneNumber.Text = insuranceCard.Text = "";
            birthDate.Value = DateTime.Now;
            passMatch.Visible = false;
        }
    }
}
