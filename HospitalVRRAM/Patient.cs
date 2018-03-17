using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWithDB
{
    class Patient : User
    {
        //  Properties  //

        public List<Diagnosis> MyHistory { get; private set; }
        public string InsurenceCard { get; private set; }
        public string Address { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        // End Properties  //

        //Constructor//

        public Patient(string name, string surename, int passportID, string login, string password,
          string insurenceCard, string address, DateTime dateOfBirth) : base(name, surename, passportID, login, password)
        {
            InsurenceCard = insurenceCard;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
        //End Constructor//

        // Methods //
        // TODO
        public DateTime RequestForConsult(Doctor doctor)
        {
            return new DateTime();
        }
        // TODO
        public List<Diagnosis> ShowMyHistory()
        {
            return null;
        }

        public void ChangeBalance(decimal moneyToAdd)
        {
            //can be negative
        }

        // TODO
        public Doctor[] SearchBySpeciality(string Speciality)
        {
            return null;
        }

        // TODO
        public void changeAddress(string newAddress)
        {

        }
        //End Methods //

    }
}
