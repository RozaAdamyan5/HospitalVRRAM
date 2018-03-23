using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalForms;
using HospitalClasses;

namespace HospitalVRRAM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new RegisterWindow());
            //Application.Run(new LoginWindow());
            //Application.Run(new DiagnosisWindow("Name Surname"));
            Application.Run(new DoctorProfileWindow(new Doctor("Name", "Surname", 15, "blah", "aaa", "aaa", new DateTime(1991, 01, 10), 1000)));
            Application.Run(new PatientProfileWindow("Name", "Surname", "1500", "099999999", "01/10/1991"));
        }
    }
}
