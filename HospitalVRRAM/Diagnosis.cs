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
        public List<Medicine> PrescribedMedicines { get; }
        public DateTime DiagnoseDate { get; }
        // End Properties  //

        //Constructor//

        public Diagnosis(string disease, DateTime consultationDate, List<Medicine> prescribedMedicines)
        {
            Disease = disease;
            DiagnoseDate = consultationDate;
            PrescribedMedicines = prescribedMedicines;
        }


        //End Constructor//
    }
}