using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HospitalConnections;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace HospitalClasses
{
    public class User
    {

        //  Properties  //
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string PassportID { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public byte[] Picture { get; protected set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }


        // End Propertieselds  //

        //Constructor//

        protected User(string name, string surname, string passportID, string login, string password)
        {
            Name = name;
            Surname = surname;
            //if (PassportIdIsUnique(passportID))
                PassportID = passportID;

            //if (LoginIsValid(login))
                Login = login;

            //if (!PasswordIsValid(password))
                Password = password;

        }

        protected User(string name, string surname, string passportID)
        {
            Name = name;
            Surname = surname;
            PassportID = passportID;
            Login = "";
            Password = "";
        }
        //End Constructor//


        // Methods //
        public static bool CheckPassportID(string passportID)
        {
            int existInDB = 0;
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.FindPatientPasspordID";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@passpordID", SqlDbType.Char, 9).Value = passportID;
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                existInDB = (int)returnParameter.Value;
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
                cmd.Parameters.Add("@passpordID", SqlDbType.Char, 9).Value = passportID;
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                existInDB = (int)returnParameter.Value;
            }

            if (existInDB == 1)
                throw new Exception("Invallid Passport ID\n");

            return true;
        }

        public static bool CheckPatientLogin(string login)
        {
            int existInDB = 0;
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.FindPatientLogin";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 20).Value = login;
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                existInDB = (int)returnParameter.Value;
            }

            return existInDB != 1;
        }

        public static bool CheckDoctorLogin(string login)
        {
            int existInDB = 0;
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.FindDoctorLogin";

            using (conn)
            {
                conn.Open();
                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                cmd.Parameters.Add("@login", SqlDbType.VarChar, 20).Value = login;
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                existInDB = (int)returnParameter.Value;
            }

            return existInDB != 1;
        }

        public static void LoginIsValid(string login, Type userType)
        {
            if (login.Length < 8)
            {
                throw new Exception("Login must be at least 8 characters.");
            }
            else if(!CheckDoctorLogin(login) && userType == typeof(Doctor) || 
                    !CheckPatientLogin(login) && userType == typeof(Patient))
            {
                throw new Exception("This login already exists");
            }
        }

        public static bool PasswordIsValid(string password)
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
                throw new Exception("Password should not be less than 8 or greater than 12 characters");
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

        public virtual void ChangeBalance(decimal amountForChange)
        {
            Balance += amountForChange;
        }

        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }


        //End Methods //

    }
}