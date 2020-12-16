using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Biblioteka_2
{
    public class SqlLogin
    {
        public bool Login(string ip,string databaseName,string userName,string password,out UserProfile user)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ip;
            builder.Password = password;
            builder.InitialCatalog = databaseName;
            builder.UserID = userName;

            using (SqlConnection cnn = new SqlConnection(builder.ToString()))
            //using (SqlCommand commandDatabase = new SqlCommand(sql, cnn))
            {
                try
                {
                    cnn.Open();
                    UserProfile profile = new UserProfile(userName, password, databaseName, ip);
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
