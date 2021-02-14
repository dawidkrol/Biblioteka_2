using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Biblioteka_2
{
    public class SqlConnectionLogin : IConnectionLogin
    {
        public ISqlProfile Login(string ip, string databaseName, string userName, string password)
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
                    string[] curr = { "KsiążkiAutorzy","Users", "Autorzy", "Czytelnicy", "Wypożyczenia", "GetCzytelnicy","GetCategories", "Kategorie", "Książki"};
                    DataTable schema = cnn.GetSchema("Tables");
                    StringBuilder s = new StringBuilder();
                    foreach (DataRow row in schema.Rows)
                    {
                        s.Append(row[2].ToString());
                        s.Append(",");
                    }
                    //Console.WriteLine(s);

                    foreach(var a in curr)
                    {
                        if(!(s.ToString().Contains(a)))
                        {
                            throw new Exception();
                        }
                    }
                    ISqlProfile profile = new SqlProfile(userName, password, databaseName, ip);
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
