using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalForms;
using HospitalClasses;

namespace HospitalVRRAM
{
    public partial class ApplicationHospital : Form
    {

        private LoginWindow login;
        private RegisterWindow registration;
        private PatientProfileWindow patientProfile;
        private DoctorProfileWindow doctorProfile;
        private AdminProfileWindow adminProfile;
        private DiagnosisWindow diagnosis;

        public ApplicationHospital()
        {
            InitializeComponent();
            this.Hide();

            InitializeLogin();
            InitializeRegister();

            login.Show();
        }

        public void InitializeLogin()
        {
            login = new LoginWindow();
            //login.signInClicked += (sender, e) => { login.Hide(); registration.Show(); };
            login.signUpClicked += (sender, e) => { login.Hide(); registration.Show(); };
            login.FormClosed += (seder, e) => { this.Close(); };
        }

        public void InitializeRegister()
        {
            registration = new RegisterWindow();
            registration.registrationCompleted += (sender, e) => { login.Show(); registration.clearFields(); registration.Hide(); };
            registration.goBackClicked += (sender, e) => { login.Show(); registration.Hide(); };
            registration.FormClosed += (sender, e) => { this.Close(); };
        }
    }
}
