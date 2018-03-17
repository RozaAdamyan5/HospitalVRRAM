using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWithDB
{
    class Diagnosis
    {
        //  Properties  //

        public string Disease { get; }
        public Medicine[] PrescribedMedicines { get; }
        // Can Doctor add medicines after writing diagnosis ? 
        public DateTime DiagnoseDate { get;  }
        // End Properties  //

        //Constructor//

        public Diagnosis(string disease, DateTime consultationDate, Medicine[] prescribedMedicines)
        {
            Disease = disease;
            DiagnoseDate = consultationDate;
            PrescribedMedicines = prescribedMedicines;
        }


        //End Constructor//
    }
}
