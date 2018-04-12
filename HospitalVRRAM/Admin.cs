using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HospitalForms;
using HospitalConnections;
using System.Windows.Forms;

namespace HospitalClasses
{
    public class Admin : User
    {
        //  Properties  //
        // End Properties  //

        //Constructor//

        public Admin(string name, string surname, string passportID, string login, string password)
            : base(name, surname, passportID, login, password)
        {

        }

        public static Admin signIn(string login, string password)
        {
            if (login == ConfigurationManager.AppSettings["Login"] && password == ConfigurationManager.AppSettings["Password"])
            {
                return new Admin(ConfigurationManager.AppSettings["Name"],
                                ConfigurationManager.AppSettings["Surname"],
                                ConfigurationManager.AppSettings["PassportID"],
                                ConfigurationManager.AppSettings["Login"],
                                ConfigurationManager.AppSettings["Password"]);
            }
            else
            {
                throw new Exception("Wrong password or login.");
            }
        }

        //End Constructor//

        public void AddMedicine(Medicine medicine)
        {
            var conn = HospitalConnection.CreateDbConnection();
            string sSQL = "sp_AddMedecine";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);


                    cmd.Parameters.Add("@Name", SqlDbType.Char, 20).Value = medicine.Name;
                    cmd.Parameters.Add("@Country", SqlDbType.Char, 20).Value = medicine.Country;
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = medicine.ExpiryDate;
                    cmd.Parameters.Add("@Price", SqlDbType.SmallMoney).Value = medicine.Price;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary, (1 << 20)).Value = medicine.Picture;

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteMedicine(Medicine medicine)
        {
            var conn = HospitalConnection.CreateDbConnection();
            string sSQL = "sp_DeleteMedicine";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);

                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = medicine.Name;

                    cmd.ExecuteNonQuery();
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ChangePrice(Medicine medicine, decimal price)
        {
            if (price < 0)
                throw new Exception("Price must be poisitive.");

            var conn = HospitalConnection.CreateDbConnection();
            string sSQL = "sp_UpdateMedicine";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);

                    cmd.Parameters.Add("@Name", SqlDbType.Char, 20).Value = medicine.Name;
                    cmd.Parameters.Add("@NewPrice", SqlDbType.SmallMoney).Value = price;

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                MessageBox.Show(e.Message);
            }
        }
        public List<Doctor> ShowDoctors()
        {
            List<Doctor> docs = new List<Doctor>();
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "sp_AllDoctors";
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
                            string passportID = (string)reader["PassportID"];
                            DateTime dateOfapproval = (DateTime)reader["DateOfApproval"];
                            decimal balance = (decimal)reader["Balance"];
                            decimal consCost = (decimal)reader["ConsultationCost"];
                            byte[] pic = (reader["Picture"] == System.DBNull.Value ? null : (byte[])reader["Picture"]);
                            string phoneNumber = (string)reader["PhoneNumber"];
                            string speciality = (string)reader["Speciality"];

                            Doctor pat = new Doctor(name,surname,passportID,speciality,dateOfapproval,consCost);
                            docs.Add(pat);
                        }
                    }
                }
                return docs;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return docs;
        }

        public List<Medicine> ShowMedicine()
        {
            List<Medicine> meds = new List<Medicine>();
            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "sp_AllMedicine";
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
                            string country = (string)reader["Country"];
                            DateTime expirationDate = (DateTime)reader["ExpirationDate"];
                            decimal price = (decimal)reader["Price"];
                            byte[] pic = (byte[])reader["Picture"];


                            Medicine med = new Medicine(name, country, price, expirationDate);
                            med.Picture = pic;
                            meds.Add(med);
                        }
                    }
                }
                return meds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return meds;
        }
    }
}
