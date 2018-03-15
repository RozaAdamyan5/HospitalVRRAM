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
        public string Name { get; protected set; }
        public string SureName { get; protected set; }
        public int PassportID { get; protected set; } // Պետք է լինի ստուգում , որ չկրկնվեն
        public string Login { get { return Login; } set { /*Validation*/} } //կասկածելի է 
        public string Password { get { return Password; } set { /*Validation*/} }
        public byte[] Picture { get; protected set; }
        public string PhoneNumber { get; set; }
        public int Balance { get; set; }

        // End Propertieselds  //

        //Constructor//

        public User(string name, string surename, int passportID, string login,string password)
        {
            Name = name;
            SureName = surename;
            PassportID = passportID;
            Login = login;
            this.ChangePassword(password);
        }

        //End Constructor//

        // Methods //

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        public void AddPicture(byte[] pic)
        {
            Picture = pic;
        }

        public int showBalance()
        {
            return Balance;
        }

    


        //End Methods //

    }
}
