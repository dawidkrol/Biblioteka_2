﻿using System;
using System.Data.SqlClient;

namespace Biblioteka_2
{
    public class SqlConnectionLogin
    {
        public SqlProfile Login(string ip, string databaseName, string userName, string password)
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
                    return profile;
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }
    }
}
