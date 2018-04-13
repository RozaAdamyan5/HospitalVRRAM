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
        public byte[] Picture { get;  set; }

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
            string sSQL = "update Medicine\r\n" +
                            "set Picture = @pic" +
                         " where Name = @name";
            this.Picture = pic;

            try
            {
                var conn = HospitalConnection.CreateDbConnection();
                conn.Open();

                var cmd = (SqlCommand)HospitalConnection.CreateDbCommand(conn, sSQL, CommandType.Text);

                cmd.Parameters.Add("@name", SqlDbType.Char, 9).Value = this.Name;
                cmd.Parameters.Add("@pic", SqlDbType.VarBinary, (1 << 20)).Value = this.Picture;

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
