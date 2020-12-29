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
                Window se = (Window)sender;
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
                SqlProfile = sqlLogin.Login(ip, databaseName, userName, password);
                FileConfig fileConfig = new FileConfig();
                fileConfig.ConfigWrite(SqlProfile);

                Window se = (Window)sender;
                se.Close();

                openLogin();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}