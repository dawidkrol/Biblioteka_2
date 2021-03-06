﻿using System.Data.SqlClient;

namespace Biblioteka_2
{
    public class SqlProfile : ISqlProfile
    {
        public string name { get; }
        public string password { get; }
        public string databaseName { get; }
        public string ip { get; }
        public SqlConnectionStringBuilder connectionString { get; }
        public SqlProfile(string UserName, string UserPassword, string DatabaseName, string Ip)
        {
            name = UserName;
            password = UserPassword;
            databaseName = DatabaseName;
            ip = Ip;
            connectionString = createConnectionString(UserName, UserPassword, DatabaseName, Ip);
        }
        private SqlConnectionStringBuilder createConnectionString(string UserName, string UserPassword, string DatabaseName, string Ip)
        {
            SqlConnectionStringBuilder output = new SqlConnectionStringBuilder();
            output.DataSource = Ip;
            output.Password = UserPassword;
            output.InitialCatalog = DatabaseName;
            output.UserID = UserName;
            return output;
        }
    }
}
