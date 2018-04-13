using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalClasses
{
    public class Diagnosis
    {
        //  Properties  //

        public string Disease { get; }
        public Dictionary<Medicine, int> PrescribedMedicines { get; }
        public DateTime DiagnoseDate { get; }
        // End Properties  //

        //Constructor//

        public Diagnosis(string disease, DateTime consultationDate, Dictionary<Medicine,int> prescribedMedicines)
        {
            Disease = disease;
            DiagnoseDate = consultationDate;
            PrescribedMedicines = prescribedMedicines;
        }


        //End Constructor//
    }
}