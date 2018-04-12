using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HospitalForms;
using HospitalConnections;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalClasses
{
    public class Patient : User
    {
        //  Properties  //

        public List<Diagnosis> MyHistory { get; private set; }
        public string InsurenceCard { get; private set; }
        public string Address { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        // End Properties  //

        //Constructor//

        public Patient(string name, string surname, string passportID, string login,
                       string password, string address, string insuranceCard,
                       DateTime dateOfBirth, string phoneNumber) : base(name, surname, passportID, login, password)
        {
            Address = address;
            InsurenceCard = insuranceCard;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;

            Initialization();
            string SQlcmd = "dbo.insertPatient";
            var conn = HospitalConnection.CreateDbConnection();
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQlcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 20).Value = name;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 20).Value = surname;
                    cmd.Parameters.Add("@PassportID", SqlDbType.Char, 9).Value = passportID;
                    cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 20).Value = password;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.Char, 9).Value = phoneNumber;
                    cmd.Parameters.Add("@InsuranceCard", SqlDbType.Char, 9).Value = insuranceCard;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20).Value = address;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = dateOfBirth;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Patient(string name, string surname, string passportID, string address,
                       DateTime dateOfBirth) : base(name, surname, passportID)
        {
            Address = address;
            DateOfBirth = dateOfBirth;
            InsurenceCard = "";
            Initialization();
        }
        //End Constructor//

        // Methods //

        public static Patient SignIn(string login, string password)
        {
            Patient patient = null;
            string SQlcmd = "dbo.SignInPatient";
            var conn = HospitalConnection.CreateDbConnection();
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQlcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 20).Value = password;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.HasRows)
                        {
                            throw new Exception("Wrong password or login.");
                        }
                        else
                        {
                            string passportID = (string)reader["PassportID"];
                            string name = (string)reader["Name"];
                            string surname = (string)reader["Surname"];
                            string address = (string)reader["Address"];
                            byte[] picture = (reader["Picture"] == System.DBNull.Value ? null : (byte[])reader["Picture"]);
                            string insuranceCard = (string)reader["InsuranceCard"];
                            DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                            string phoneNumber = (string)reader["PhoneNumber"];
                            patient = new Patient(name, surname, passportID, address, dateOfBirth);
                            patient.Login = login;
                            patient.Password = password;
                            patient.InsurenceCard = insuranceCard;
                            patient.Picture = picture;
                            patient.PhoneNumber = phoneNumber;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return patient;
        }

        public void RequestForConsult(Doctor doctor, DateTime perfectTime)
        {
            doctor.newPatient(this, perfectTime);
        }

        private void Initialization()
        {
            List<Diagnosis> history = new List<Diagnosis>();

            var conn = HospitalConnection.CreateDbConnection();
            Diagnosis diagnose = null;
            string SQLcmd0 = "dbo.ReadMyHistory";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd0 = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd0, CommandType.StoredProcedure);
                    cmd0.Parameters.Add("@PassportID", SqlDbType.Char, 9).Value = PassportID;

                    using (var reader0 = (SqlDataReader)cmd0.ExecuteReader())
                    {
                        while (reader0.Read())
                        {
                            string SQLcmd1 = "dbo.Drugs";



                            var cmd1 = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd1, CommandType.StoredProcedure);
                            cmd1.Parameters.Add("@DiagnosisID", SqlDbType.Int).Value = reader0["DiagnosesID"];

                            using (var reader1 = (SqlDataReader)cmd1.ExecuteReader())
                            {
                                if (reader1.HasRows)
                                {
                                    bool hasMoreResults = true;

                                    List<Medicine> medicine = new List<Medicine>();

                                    //while (hasMoreResults)
                                    {
                                        while (reader1.Read())
                                        {
                                            string n = (string)reader1["Name"];
                                            string c = (string)reader1["Country"];
                                            decimal p = (decimal)reader1["Price"];
                                            DateTime e= (DateTime)reader1["ExpirationDate"];
                                            medicine.Add(new Medicine(n, c, p, e));
                                        }
                                        diagnose = new Diagnosis((string)reader0["Description"], (DateTime)reader0["DateOfDiagnosis"], medicine);
                                        history.Add(diagnose);

                                        hasMoreResults = reader1.NextResult();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            MyHistory = history;
        }

        public List<Diagnosis> ShowMyHistory()
        {
            return MyHistory;
        }

        public void ChangeBalance(decimal moneyToAdd)       // Can be negative
        {
            if (Balance <= -moneyToAdd)
                Balance = 0;
            else
                Balance += moneyToAdd;

            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.changeBalance";

            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@PassportID", SqlDbType.Char, 9).Value = PassportID;
                    cmd.Parameters.Add("@Balance", SqlDbType.SmallMoney).Value = Balance;

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public List<Doctor> SearchBySpeciality(string Speciality)
        {
            List<Doctor> doctors = new List<Doctor>();
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.FindDoctorBySpeciality";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@Speciality", SqlDbType.VarChar, 20).Value = Speciality;

                    using (var reader = (SqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor((string)reader["Name"], (string)reader["Surname"], (string)reader["PassportID"],
                                                   (string)reader["Speciality"], (DateTime)reader["DateOfApproval"], (decimal)reader["ConsultationCost"]));      //incompatibility between databases and classes
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return doctors;
        }

        public void changeAddress(string newAddress)
        {
            Address = newAddress;

            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.ChangePatientAddress";

            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@PassportID", SqlDbType.Char, 9).Value = PassportID;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20).Value = Address;


                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void AddPicture(byte[] pic)
        {

            string sSQL = "update Patient\r\n" +
                            "set Picture = @pic" +
                         " where passportID = @passportID";
            Picture = pic;

            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.Text);

                cmd.Parameters.Add("@passportID", SqlDbType.Char, 9).Value = PassportID;
                cmd.Parameters.Add("@pic", SqlDbType.VarBinary, (1 << 20)).Value = Picture;

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void changePassword(string newPassword)
        {
            Password = newPassword;

            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.ChangePatientPassword";

            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@PassportID", SqlDbType.Char, 9).Value = PassportID;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 20).Value = Password;


                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //End Methods //

    }
}