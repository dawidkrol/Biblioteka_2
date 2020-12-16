using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace Biblioteka_2
{
    public class Module
    {
        public static string connectionstring { get; private set; }
        public static UserProfile user { get; private set; }
        public static SqlProfile SqlProfile { get; private set; }
        static Module()
        {
            SqlProfile = new SqlProfile("sa", "zaq1@WSX", "biblioteka_2", "192.168.0.19");
            connectionstring = SqlProfile.connectionString;
        }


        public void login(string connectionString, string name, string password,object sender)
        {
            List<UserProfile> users = new List<UserProfile>();
            string sql = "select * from Users";
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                using (SqlCommand cmm = new SqlCommand(sql, cnn))
                {
                    SqlDataReader reader = null;
                    cnn.Open();
                    reader = cmm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string login = reader.GetString(1);
                            string pass = reader.GetString(2);
                            string fn = reader.GetString(3);
                            string ln = reader.GetString(4);
                            users.Add(new UserProfile(fn, ln, pass, login));
                        }
                    }
                }
                foreach (var item in users)
                {
                    if (name == item._login && password == item._password)
                    {
                        user = new UserProfile(item._FirstName, item._LastName, item._password, item._login);
                    }
                }
            }
            catch (SqlException e)
            {
                ReconnectSql reconnect = new ReconnectSql();
                reconnect.Activate();
                reconnect.Visibility = System.Windows.Visibility.Visible;
                var se = (Window)sender;
                se.Visibility = Visibility.Hidden;
                se.Close();
            }
        }


        public bool Updatesqlprofile(string ip, string databaseName, string userName, string password, object sender)
        {
            bool output = false;
            SqlConnectionLogin sqlLogin = new SqlConnectionLogin();
            SqlProfile profile = null;
            try
            {
                sqlLogin.Login(ip, databaseName, userName, password, out profile);
                connectionstring = profile.connectionString;
                SqlProfile = profile;
                output = true;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Visibility = Visibility.Visible;
                var se = (Window)sender;
                se.Visibility = Visibility.Hidden;
                se.Close();
                return output;
            }
            catch (Exception ex)
            {
                return output;
            }
        }
    }

}
