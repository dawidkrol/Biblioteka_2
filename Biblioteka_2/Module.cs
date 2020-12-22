using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;

namespace Biblioteka_2
{
    public class Module
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string connectionstring { get; private set; }
        public UserProfile user { get; private set; }
        public SqlProfile SqlProfile { get; private set; }
        private bool _login { get; set; }
        public bool IsLogged
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(_login.ToString()));
            }
        }

        public Module()
        {
            if (FileConfig.fileEx())
            {
                FileConfig info = new FileConfig();
                SqlProfile = info.GetInfo();
                connectionstring = SqlProfile.connectionString;
                openLogin();
            }
            else
            {
                Reconnect();
            }
        }

        internal void login(string name, string password, object sender)
        {
            try
            {
                List<UserProfile> users = Login.getUsers(this);
                foreach (var item in users)
                {
                    if (name == item._login && password == item._password)
                    {
                        user = new UserProfile(item._FirstName, item._LastName, item._password, item._login);
                        IsLogged = true;
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

        private void openLogin()
        {
            LoginWindow login = new LoginWindow(this);
            login.Visibility = Visibility.Visible;
        }

        private void Reconnect()
        {
            ReconnectSql reconnect = new ReconnectSql(this);
            reconnect.Activate();
            reconnect.Visibility = Visibility.Visible;
        }

        internal void logout()
        {
            IsLogged = false;
            user = null;
            openLogin();
        }

        public bool UpdateSqlProfile(string ip, string databaseName, string userName, string password, object sender)
        {
            SqlConnectionLogin sqlLogin = new SqlConnectionLogin();
            try
            {
                SqlProfile profile;
                sqlLogin.Login(ip, databaseName, userName, password, out profile);
                connectionstring = profile.connectionString;
                SqlProfile = profile;
                FileConfig file = new FileConfig();
                file.ConfigWrite(SqlProfile);
                var se = (Window)sender;
                se.Close();
                openLogin();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}