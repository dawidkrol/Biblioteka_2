using System;
using System.Data.SqlClient;

namespace Biblioteka_2
{
    public class SqlConnectionLogin
    {
        public bool Login(string ip, string databaseName, string userName, string password, out SqlProfile user)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ip;
            builder.Password = password;
            builder.InitialCatalog = databaseName;
            builder.UserID = userName;

            using (SqlConnection cnn = new SqlConnection(builder.ToString()))
            {
                try
                {
                    cnn.Open();
                    SqlProfile profile = new SqlProfile(userName, password, databaseName, ip);
                    user = profile;
                    return true;
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }
    }
}
