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
    public partial class RegisterWindow : Form
    {
        public RegisterWindow()
        {
            InitializeComponent();
            password.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            confirmPassword.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            
        }

        private void RegisterWindow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(100, 0, 0, 0)), 25, 330, 415, 330);
        }
    }
}
