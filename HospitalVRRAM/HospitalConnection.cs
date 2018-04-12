using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace HospitalConnections
{
    class HospitalConnection
    {
        public static IDbConnection CreateDbConnection()
        {
            IDbConnection connection = null;
            try
            {
                var sConnectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                var sProviderName = ConfigurationManager.ConnectionStrings["MainConnection"].ProviderName;

                var factory =
                    DbProviderFactories.GetFactory(sProviderName);

                connection = factory.CreateConnection();
                connection.ConnectionString = sConnectionString;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return connection;
        }
        public static IDbCommand CreateDbCommand(IDbConnection conn, string sSQL, CommandType type)
        {
            IDbCommand command = null;

            try
            {
                command = conn.CreateCommand();
                command.CommandType = type;
                command.CommandText = sSQL;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return command;
        }
    }
}
