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

        public void AddPicture(byte[] pic)
        {

            string sSQL = "select name,Picture.PathName() as PathName, Picture\r\n"
                       + "from Medicine\r\n"
                       + " where name =@name";

            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.Text);

                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 20).Value = "Nurofen";

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


    }

    //End Methods //

}
