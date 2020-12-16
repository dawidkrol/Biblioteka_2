using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlBase
{
    public class SqlLogin
    {
        public bool Login(string ip,string databaseName,string userName,string password)
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
                    return true;
                    cnn.Close();
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }
    }
}
