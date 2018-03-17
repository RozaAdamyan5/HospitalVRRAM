using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWithDB
{
    class Admin : User
    {
        //  Properties  //
        // End Properties  //

        //Constructor//

        public Admin(string name, string surename, int passportID, string login, string password)
            : base(name, surename, passportID, login, password){}

        //End Constructor//

        public void AddMedicine(Medicine medicine) { }      // TODO
        public void ChangePrice(Medicine medicine , decimal price) { }  // TODO
        public void AddDoctor(Doctor doctor) { }            // TODO
        public void DeleteDoctor(Doctor doctor) { }         // TODO
        public Doctor[] ShowDoctors()                       // TODO
        {
            return null;
        }
    }
}
