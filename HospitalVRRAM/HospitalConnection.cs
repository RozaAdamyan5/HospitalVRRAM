using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace HospitaConnection
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return command;
        }
    }
}
