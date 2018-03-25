using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalForms
{
    public partial class LoginWindow : Form
    {
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

    }
}
