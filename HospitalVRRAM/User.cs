using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWithDB
{
    class User
    {

        //  Properties  //
        public string Name { get; }
        public string SureName { get;}
        public int PassportID { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public byte[] Picture { get; protected set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }

        // End Propertieselds  //

        //Constructor//

        public User(string name, string surename, int passportID, string login, string password)
        {
            Name = name;
            SureName = surename;
            if (!PassportIdIsUnique(passportID)) throw new Exception("Invallid Passport ID\n");
            PassportID = passportID;
            if (!LoginIsValid(login)) throw new Exception("Invalid login\n");
            Login = login;
            if (!PasswordIsValid(password)) throw new Exception("Invalid password\n");
            Password = password;
        }

        //End Constructor//

        // Methods //
        // TODO
        bool PassportIdIsUnique(int passport)
        {
            bool existInDB = true;
            if (existInDB)
                return false;
            else
                return true;
        }
        // TODO
        public bool LoginIsValid(string login)
        {
            //validation
            return false;
        }
        // TODO
        public bool PasswordIsValid(string password)
        {
            bool isValid = true;
            if (isValid)
                return true;
            else
                return false;
        }

        public void AddPicture(byte[] pic)
        {
            Picture = pic;
        }

        public int showBalance()
        {
            return Balance;
        }

        public void ChageBalance(int amountForChange)
        {
            Balance += amountForChange;
        }


        //End Methods //

    }
}
