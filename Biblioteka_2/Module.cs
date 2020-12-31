using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;

namespace Biblioteka_2
{
    public class Module
    {
        internal event PropertyChangedEventHandler LoginProperty;
        internal IUserProfile user { get; private set; }
        internal SqlProfile SqlProfile { get; private set; }
        private bool _login { get; set; } = false;
        public bool IsLogged
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                LoginProperty.Invoke(this, new PropertyChangedEventArgs(_login.ToString()));
            }
        }

        public Module()
        {
            if (FileConfig.fileEx())
            {
                FileConfig info = new FileConfig();
                SqlProfile = info.GetInfo();
                openLogin();
            }
            else
            {
                openReconnect();
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
                Window se = (Window)sender;
                se.Visibility = Visibility.Hidden;
                se.Close();
                openReconnect();
            }
        }

        private void openLogin()
        {
            LoginWindow login = new LoginWindow(this);
            login.Activate();
            login.Visibility = Visibility.Visible;
        }

        private void openReconnect()
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

        public bool UpdateSqlProfile(string ip, string databaseName, string userName, string password)
        {
            SqlConnectionLogin sqlLogin = new SqlConnectionLogin();
            FileConfig fileConfig = new FileConfig();
            try
            {
                SqlProfile = sqlLogin.Login(ip, databaseName, userName, password);
                fileConfig.ConfigWrite(SqlProfile);
                openLogin();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}