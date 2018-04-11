using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HospitalConnections;
using System.Data;
using System.Data.SqlClient;

namespace HospitalClasses
{
    public class User
    {

        //  Properties  //
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public int PassportID { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public byte[] Picture { get; protected set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }


        // End Propertieselds  //

        //Constructor//

        protected User(string name, string surname, int passportID, string login, string password)
        {
            Name = name;
            Surname = surname;
            if (PassportIdIsUnique(passportID))
                PassportID = passportID;

            if (LoginIsValid(login))
                Login = login;

            if (!PasswordIsValid(password))
                Password = password;

        }

        protected User(string name, string surname, int passportID)
        {
            Name = name;
            Surname = surname;
            PassportID = passportID;
            Login = "";
            Password = "";
        }
        //End Constructor//


        // Methods //
       public  bool PassportIdIsUnique(int passportID)
        {
            int existInDB = 0;
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.FindPatientPasspordID";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@passportID", SqlDbType.NVarChar).Value = passportID;
                cmd.ExecuteNonQuery();

                existInDB = (int)cmd.ExecuteScalar();
            }

            if (existInDB == 1)
                throw new Exception("Invallid Passport ID\n");

            //////////////////////////

            conn = HospitalConnection.CreateDbConnection();

            SQLcmd = "dbo.FindDoctorPasspordID";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@passportID", SqlDbType.NVarChar).Value = passportID;
                cmd.ExecuteNonQuery();

                existInDB = (int)cmd.ExecuteScalar();
            }

            if (existInDB == 1)
                throw new Exception("Invallid Passport ID\n");

            return true;
        }

        public bool LoginIdIsUnique(string login)
        {
            int existInDB = 0;
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.FindPatientLogin";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = login;
                cmd.ExecuteNonQuery();

                existInDB = (int)cmd.ExecuteScalar();
            }

            if (existInDB == 1)
                return false;

            /////////

            conn = HospitalConnection.CreateDbConnection();

            SQLcmd = "dbo.FindDoctortLogin";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = login;
                cmd.ExecuteNonQuery();

                existInDB = (int)cmd.ExecuteScalar();
            }

            if (existInDB == 1)
                return false;

            return true;
        }

        public bool LoginIsValid(string login)
        {
            //validation
            if(login.Length < 8)
            {
                throw new Exception("Login must be at least 8 characters.");
            }
            
            //else if (!Regex.Replace(login, @"^[a-z0-9](\.?[a-z0-9]){5,}@pat\.hosp$", "").Equals(""))
            //{
            //    throw new Exception("login must have SOMETHING@pat.hosp form");
            //}
            else if(!LoginIdIsUnique(login))
            {
                throw new Exception("This login already exists");
            }

            return true;
        }

        public bool PasswordIsValid(string password)
        {
            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                throw new Exception("Password should contain At least one lower case letter");
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                throw new Exception("Password should contain At least one upper case letter");
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                throw new Exception("Password should not be less than or greater than 12 characters");
            }
            else if (!hasNumber.IsMatch(input))
            {
                throw new Exception("Password should contain At least one numeric value");
            }
            else if (!hasSymbols.IsMatch(input))
            {
                throw new Exception("Password should contain At least one special case characters");
            }

            return true;
        }

        public virtual void AddPicture(byte[] pic)
        {
           //must be implemented by derived classes
            
        } 

        public virtual decimal ShowBalance()
        {
            return Balance;
        } 

        public virtual void ChageBalance(int amountForChange)
        {
            Balance += amountForChange;
        } 


        //End Methods //

    }
}