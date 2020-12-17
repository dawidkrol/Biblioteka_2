using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            //SqlProfile = new SqlProfile("sa", "zaq1@WSX", "biblioteka_2", "192.168.0.19");
            //connectionstring = SqlProfile.connectionString;
            if (FileConfig.fileEx())
            {
                    FileConfig info = new FileConfig();
                    SqlProfile = info.GetInfo();
                    connectionstring = SqlProfile.connectionString;
                    Module module = new Module();
                    module.openLogin();
            }
            else
            {
                FileConfig file = new FileConfig();
                file.CreateConfigFile();
                Reconnect();
            }
        }

        public void login(string name, string password,object sender)
        {
            try
            {
                List<UserProfile> users = Login.getUsers();
                foreach (var item in users)
                {
                    if (name == item._login && password == item._password)
                    {
                        user = new UserProfile(item._FirstName, item._LastName, item._password, item._login);
                    }
                }
            }
            catch (SqlException)
            {
                var se = (Window)sender;
                se.Visibility = Visibility.Hidden;
                se.Close();
                Reconnect();
            }
        }
        public void openLogin()
        {
            LoginWindow login = new LoginWindow();
            login.Visibility = Visibility.Visible;
        }

        public static void Reconnect()
        {
            ReconnectSql reconnect = new ReconnectSql();
            reconnect.Activate();
            reconnect.Visibility = Visibility.Visible;
        }

        public bool UpdateSqlProfile(string ip, string databaseName, string userName, string password, object sender)
        {
            SqlConnectionLogin sqlLogin = new SqlConnectionLogin();
            SqlProfile profile = null;
            try
            {
                sqlLogin.Login(ip, databaseName, userName, password, out profile);
                connectionstring = profile.connectionString;
                SqlProfile = profile;
                FileConfig file = new FileConfig();
                file.ConfigWrite(SqlProfile);
                var se = (Window)sender;
                se.Close();
                Module module = new Module();
                module.openLogin();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}