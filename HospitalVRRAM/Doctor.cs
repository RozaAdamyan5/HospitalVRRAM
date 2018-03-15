using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWithDB
{
    class Doctor : User
    {
        //  Properties  //

        public string Speciality { get; set; }
        public DateTime GetEmployed { get; set; }
        public decimal ConsultationCost { get; set; }
        public Dictionary<Patient, DateTime> Patients { get; set; }

        //  End Properties  //


        //Constructor//

        public Doctor(string name, string surename, int passportID, string login, string password,
            string speciality, DateTime getEmployed, decimal consultationCost) : base(name, surename, passportID, login, password)
        {
            Speciality = speciality;
            ConsultationCost = consultationCost;
            GetEmployed = getEmployed;
        }
        //End Constructor//


        // Methods //

        public void WriteDiagnosis(Patient patient)
        {
            //todo
        }
        public void ServePatient(Patient patient, Diagnosis diagnose)
        {

        }
        public Patient[] ShowPatient()
        {
            return null;
        }
        public Dictionary<Patient,DateTime> Calendar()
        {
            return null;
        }

        public Diagnosis PatientDiagnosis(Patient patient)
        {
            return null;
        }

        public DateTime newPatient (Patient patient)
        {
            //add in Dictionary of patietnts
        
        }
        //End Methods //
    }
}
