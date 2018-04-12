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
        public decimal Price { get; private set; }
        public DateTime ExpiryDate { get; }
        public byte[] Picture { get; private set; }

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

        public Image GetPicture()
        {
            Image photo = null; //should be the default picture
            string SQLcmd = "dbo.MedicineGetPicture";
            
            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);

                cmd.Parameters.Add("Name", SqlDbType.Char, 9).Value = this.Name;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the pointer for file
                        var path = reader.GetString(reader.GetOrdinal("PathName"));
                        var imbytes = reader.GetSqlBytes(reader.GetOrdinal("Picture")).Buffer;

                        var ms = new MemoryStream(Picture);
                        photo = Image.FromStream(ms);

                        this.Picture = imbytes;
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
            return photo;
        }

        public void AddPicture(byte[] pic)
        {
            string SQLcmd = "dbo.MedicineAddPicture";
            this.Picture = pic;

            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, SQLcmd, CommandType.StoredProcedure);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 20).Value = this.Name;
                cmd.Parameters.Add("@Pic", SqlDbType.VarBinary, (1 << 20)).Value = this.Picture;

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    //End Methods //

}
