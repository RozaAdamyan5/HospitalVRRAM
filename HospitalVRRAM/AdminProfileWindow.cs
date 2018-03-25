using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalVRRAM
{
    public partial class AdminProfileWindow : Form
    {
        public AdminProfileWindow()
        {
            InitializeComponent();
        }

        private void allDoctors_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;
            // TODO
            // Show all doctors in universalPanel
        }

        private void allPatients_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;
            // TODO
            // Show all patients in universalPanel
        }

        private void allMedicine_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            this.Width = 750;
            universalPanel.BorderStyle = BorderStyle.Fixed3D;
            universalPanel.BackColor = Color.White;
            // TODO
            // Show all medicine in universalPanel
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
