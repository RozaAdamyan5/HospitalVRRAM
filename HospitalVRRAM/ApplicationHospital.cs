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
            login.FormClosing += (sender, e) => { if (MessageBox.Show("Are you sure you want to exit?", "Hospital", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true; };
            login.patientSignedIn += (sender, e) => { login.Hide(); InitializePatientWindow(e.patient); patientProfile.Show(); };
            login.doctorSignedIn += (sender, e) => { login.Hide(); InitializeDoctorWindow(e.doctor); doctorProfile.Show(); };
            login.adminIsHere += (sender, e) => { login.Hide(); InitializeAdminWindow(e.admin); adminProfile.Show(); };
            login.signUpClicked += (sender, e) => { login.Hide(); registration.Show(); };
            login.FormClosed += (sender, e) => { this.Close(); };
        }

        public void InitializeRegister()
        {
            registration = new RegisterWindow();
            registration.FormClosing += (sender, e) => { if (MessageBox.Show("Are you sure you want to exit?", "Hospital", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true; };
            registration.registrationCompleted += (sender, e) => { login.Show(); registration.clearFields(); registration.Hide(); };
            registration.goBackClicked += (sender, e) => { login.Show(); registration.Hide(); };
            registration.FormClosed += (sender, e) => { this.Close(); };
        }

        public void InitializePatientWindow(Patient patient)
        {
            patientProfile = new PatientProfileWindow(patient);
            patientProfile.FormClosing += (sender, e) => { if (MessageBox.Show("Are you sure you want to exit?", "Hospital", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true; };
            patientProfile.FormClosed += (sender, e) => { this.Close(); };
            patientProfile.logOutClicked += (sender, e) => { login.clearFields(); login.Show(); patientProfile.Dispose(); };
        }

        public void InitializeDoctorWindow(Doctor doctor)
        {
            doctorProfile = new DoctorProfileWindow(doctor);
            doctorProfile.FormClosing += (sender, e) => { if (MessageBox.Show("Are you sure you want to exit?", "Hospital", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true; };
            doctorProfile.FormClosed += (sender, e) => { this.Close(); };
            doctorProfile.logOutClicked += (sender, e) => { login.clearFields(); login.Show(); doctorProfile.Dispose(); };
            doctorProfile.serveClicked += (sender, e) => { doctorProfile.Hide(); InitializeDiagnosisWindow(e.doctor, e.patient); diagnosis.Show(); };
        }

        public void InitializeAdminWindow(Admin admin)
        {
            adminProfile = new AdminProfileWindow(admin);
            adminProfile.FormClosing += (sender, e) => { if (MessageBox.Show("Are you sure you want to exit?", "Hospital", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true; };
            adminProfile.FormClosed += (sender, e) => { this.Close(); };
            adminProfile.logOutClicked += (sender, e) => { login.clearFields(); login.Show(); adminProfile.Dispose(); };
        }

        public void InitializeDiagnosisWindow(Doctor doctor, Patient patient)
        {
            diagnosis = new DiagnosisWindow(doctor, patient);
            diagnosis.FormClosing += (sender, e) => { if (MessageBox.Show("Are you sure you want to exit?", "Hospital", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true; };
            diagnosis.FormClosed += (sender, e) => { this.Close(); };
            diagnosis.backToDoctorProfile += (sender, e) => { doctorProfile.Show(); diagnosis.Hide(); diagnosis.Dispose(); };
        }
    }
}
