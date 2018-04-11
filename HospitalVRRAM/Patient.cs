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

        public Patient(string name, string surname, int passportID, string login,
                       string password, string address, string insuranceCard,
                       DateTime dateOfBirth) : base(name, surname, passportID, login, password)
        {
            Address = address;
            InsurenceCard = insuranceCard;
            DateOfBirth = dateOfBirth;

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
                    cmd.Parameters.Add("@Login", SqlDbType.VarChar, 8).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 8).Value = password;

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

        public Patient(string name, string surname, int passportID, string address,
                       DateTime dateOfBirth) : base(name, surname, passportID)
        {
            Address = address;
            DateOfBirth = dateOfBirth;
            InsurenceCard = "";
        }
        //End Constructor//

        // Methods //

        public static Patient SignUp(string login,string password)
        {
            Patient patient = null;
            string SQlcmd = "dbo.SignUpPatient";
            var conn = HospitalConnection.CreateDbConnection();
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQlcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@Login", SqlDbType.VarChar, 8).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 8).Value = password;
                    using (var reader = (SqlDataReader)cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            throw new Exception("Wrong password or login.");
                        }
                        else
                        {
                            string name = (string)reader["Name"];
                            string surename = (string)reader["Surename"];
                            int passportID = (int)reader["PassportID"];
                            string address = (string)reader["Address"];
                            string insuranceCard = (string)reader["InsuranceCard"];
                            DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                            patient = new Patient(name, surename, passportID, login, password, address, insuranceCard, dateOfBirth);
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

        public List<Diagnosis> ShowMyHistory()
        {
            List<Diagnosis> history = new List<Diagnosis>();

            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd0 = "dbo.ReadMyHistory"; //"select Discription, DateOfDiagnosis, DiagnosesID \r\n" +
                                                  //"from Diagnoses \r\n" +
                                                  //"where PatientID='" + this.PassportID + "'";
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
                            //"select Name, Country, Price, ExpirationDaAtet \r\n" +
                            //"from Medicine \r\n" +
                            //"join AssingnedTo on MedicineID = ID" +
                            //"where DiagnoseID = " + reader0["DiagnosesID"];


                            var cmd1 = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd1, CommandType.Text);
                            cmd1.Parameters.Add("@DiagnosisID", SqlDbType.Int).Value = reader0["DiagnosesID"];

                            using (var reader1 = (SqlDataReader)cmd1.ExecuteReader())
                            {
                                List<Medicine> medicine = new List<Medicine>();
                                while (reader1.Read())
                                {
                                    medicine.Add(new Medicine((string)reader1["Name"], (string)reader1["Country"],
                                                               (int)reader1["Price"], (DateTime)reader1["ExpirationDate"]));
                                }
                                Diagnosis diagnose = new Diagnosis((string)reader0["Discription"], (DateTime)reader0["DateOfDiagnosis"], medicine);
                                history.Add(diagnose);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            MyHistory = history;
            return history;
        }

        public void ChangeBalance(decimal moneyToAdd /*can be negative*/)
        {
            if (Balance <= -moneyToAdd)
                Balance = 0;
            else
                Balance += moneyToAdd;

            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.changeBalance";
            //"update Patient \r\n" +
            //"set Balance = " + Balance + "\r\n" +
            //"where PassportID = '" + PassportID + "'";
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.Text);
                    cmd.Parameters.Add("@PassportID", SqlDbType.SmallMoney).Value = PassportID;
                    cmd.Parameters.Add("@Balance", SqlDbType.Char, 9).Value = Balance;

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
            //"select *, Count(*) as count \r\n" +
            //"from Doctor \r\n" +
            //"where Speciality = " + Speciality;
            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);
                    cmd.Parameters.Add("@Speciality", SqlDbType.TinyInt).Value = Speciality;

                    using (var reader = (SqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor((string)reader["Name"], (string)reader["Surname"], (int)reader["PasportID"],
                                                   (string)reader["Speciality"], (DateTime)reader["DateOfApproval"], (decimal)reader["ConsultationCost"]));      //incompatibility between databases and classes
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            return doctors;
        }

        public void changeAddress(string newAddress)
        {
            Address = newAddress;

            var conn = HospitalConnection.CreateDbConnection();

            string SQLcmd = "dbo.ChangePatientAddress";
            //"update Patient \r\n" +
            //                "set Address = '" + Address + "'\r\n" +
            //                "where PassportID = '" + PassportID + "'";
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

        public void AddPicture(byte[] pic)
        {

            string sSQL = "select passportID,Picture.PathName() as PathName, Picture\r\n"
                       + "from Patients\r\n"
                       + " where passportID =@passportID";

            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.Text);

                cmd.Parameters.Add("@passportID", SqlDbType.Char,9 ).Value = this.PassportID;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the pointer for file
                        var path = reader.GetString(reader.GetOrdinal("PathName"));
                        var imbytes = reader.GetSqlBytes(reader.GetOrdinal("Picture")).Buffer;

                        var ms = new MemoryStream(imbytes);

                        Image photo = Image.FromStream(ms);
                        //must be done using our form
                        //  label1.Text = reader.GetString(reader.GetOrdinal("SName"));
                        //  pictureBox1.Image = photo;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //End Methods //

    }
}