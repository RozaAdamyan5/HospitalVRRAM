using HospitalConnections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalClasses
{
    public class Medicine
    {

        //  Properties  //
        public string Name { get; }
        public string Country { get; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; }
        public byte[] Picture { get; set; }

        // End Properties  //

        //Constructor//

        public Medicine(string name, string country, decimal price, DateTime expiryDate)
        {
            Name = name;
            Country = country;
            Price = price;
            ExpiryDate = expiryDate;
        }

        //End Constructor//

        // Methods //

        public void AddPicture(byte[] pic)
        {
            string sSQL = "dbo.MedicineAddPicture";
            this.Picture = pic;

            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.StoredProcedure);

                cmd.Parameters.Add("@Name", SqlDbType.Char, 9).Value = this.Name;
                cmd.Parameters.Add("@Pic", SqlDbType.VarBinary, (1 << 20)).Value = this.Picture;

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Medicine GetMedicine(string name)
        {
            Medicine med = null;
            var connection = HospitalConnection.CreateDbConnection();
            string sSQL = "dbo.LoadMedicineByName";
            try
            {
                using (connection)
                {
                    connection.Open();
                    var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(connection, sSQL, CommandType.StoredProcedure);


                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name;

                    using (var reader = (SqlDataReader)cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.HasRows)
                        {
                            throw new Exception("Medicine not found!");
                        }
                        else
                        {
                            string Name = (string)reader["Name"];
                            string Country = (string)reader["Country"];
                            DateTime ExpiryDate = (DateTime)reader["ExpirationDate"];
                            decimal Price = (decimal)reader["Price"];
                            byte[] Picture = (reader["Picture"] == System.DBNull.Value ? null : (byte[])reader["Picture"]);
                            
                            med = new Medicine(Name, Country, Price, ExpiryDate);
                            med.Picture = Picture;
                        }

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return med;
        }
    }

    //End Methods //

}
