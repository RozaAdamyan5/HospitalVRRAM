using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HospitalForms;
using HospitalConnections;

namespace HospitalClasses
{
    public class Admin : User
    {
        //  Properties  //
        // End Properties  //

        //Constructor//

        public Admin(string name, string surname, int passportID, string login, string password)
            : base(name, surname, passportID, login, password)
        {
            string SQlcmd = "dbo.insertAdmin";
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
                    cmd.Parameters.Add("@Login", SqlDbType.VarChar, 8).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 8).Value = password;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        //End Constructor//

        public void AddMedicine(Medicine medicine)
        {
            var conn = HospitalConnection.CreateDbConnection();
            string sSQL = "sp_AddMedicine";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);


                    cmd.Parameters.Add("@Name", SqlDbType.Char, 20).Value = medicine.Name;////name must be unique
                    cmd.Parameters.Add("@Country", SqlDbType.Char, 20).Value = medicine.Country;
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = medicine.ExpiryDate;
                    cmd.Parameters.Add("@Price", SqlDbType.SmallMoney).Value = medicine.Price;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = medicine.Picture;

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ChangePrice(Medicine medicine, decimal price)
        {
            var conn = HospitalConnection.CreateDbConnection();
            string sSQL = "sp_ChangeMedcinePrice";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);

                    cmd.Parameters.Add("@Name", SqlDbType.Char, 20).Value = medicine.Name;////name must be unique
                    cmd.Parameters.Add("@Price", SqlDbType.SmallMoney).Value = price;

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteDoctor(Doctor doctor)
        {
            var conn = HospitalConnection.CreateDbConnection();
            string sSQL = "sp_DeleteDoctor";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);

                    cmd.Parameters.Add("@passportID", SqlDbType.Char, 9).Value = doctor.PassportID;

                    cmd.ExecuteNonQuery();
                }


            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AddDoctor(Doctor doctor)
        {
            Doctor a = new Doctor(doctor.Name, doctor.Surname, doctor.PassportID, doctor.Login,
                doctor.Password, doctor.Speciality, doctor.GetEmployed, doctor.ConsultationCost);//constructor will add to db

        }
        public List<Doctor> ShowDoctors()
        {
            List<Doctor> docs = new List<Doctor>();
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.AllDoctors";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            string surname = (string)reader["surname"];
                            int passportID = (int)reader["PassportID"];
                            string login = (string)reader["Login"];
                            string password = (string)reader["Password"];
                            DateTime dateOfapproval = (DateTime)reader["DateOfApproval"];
                            decimal balance = (decimal)reader["Balance"];
                            decimal consCost = (decimal)reader["ConsultationCost"];//must be added in db
                            byte[] pic = reader["Picture"].ToString;//cant find normal cast
                            string phoneNumber = (string)reader["PhoneNumber"];
                            int speciality = (int)reader["Speciality"];

                            Doctor pat = new Doctor(name,surname,passportID,login
                                ,password,speciality,dateOfapproval,consCost);
                            docs.Add(pat);
                        }

                    }
                }
                return docs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return docs;
        }
    }
}
