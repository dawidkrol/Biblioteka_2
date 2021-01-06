using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace Biblioteka_2
{
    public class Module : IModule
    {
        ILogin _loginClass;
        IFileConfig _fileConfig;
        IConnectionLogin _sqlLogin;
        MainWindow _mainWindow;
        public event PropertyChangedEventHandler LoginProperty;
        public IUserProfile user { get; private set; }
        public ISqlProfile SqlProfile { get; private set; }
        public bool _login { get; set; } = false;
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

        public Module(IConnectionLogin sqlLogin,IFileConfig fileConfig,ILogin login)
        {
            _loginClass = login;
            _fileConfig = fileConfig;
            _sqlLogin = sqlLogin;
            _mainWindow = new MainWindow(this);
            if (_fileConfig.fileEx("_config.txt"))
            {
                SqlProfile = _fileConfig.GetInfoSQL();
                openLogin();
            }
            else
            {
                openReconnect();
            }
        }

        public void login<T>(string name, string password, object sender) where T: IUserProfile, new()
        {
            try
            {
                List<T> users = _loginClass.getUsers<T>(this);
                foreach (var item in users)
                {
                    if (name == item._login && password == item._password)
                    {
                        user = new T() { _FirstName = item._FirstName, _LastName = item._LastName, _login = item._login, _password = item._password };
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

        public void logout()
        {
            IsLogged = false;
            user = null;
            openLogin();
        }

        public bool UpdateSqlProfile(string ip, string databaseName, string userName, string password)
        {
            try
            {
                SqlProfile = _sqlLogin.Login(ip, databaseName, userName, password);
                _fileConfig.ConfigWrite(SqlProfile, "_config.txt");
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