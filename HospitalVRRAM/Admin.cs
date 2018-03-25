using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalClasses
{
    public class Admin : User
    {
        //  Properties  //
        // End Properties  //

        //Constructor//

        public Admin(string name, string surename, int passportID, string login, string password)
            : base(name, surename, passportID, login, password){}

        //End Constructor//

        public void AddMedicine(Medicine medicine) { }
        public void ChangePrice(Medicine medicine , decimal price) { }
        public void AddDoctor(Doctor doctor) { }
        public void DeleteDoctor(Doctor doctor) { }
        public Doctor[] ShowDoctors()    
        {
            return null;
        }
    }
}
