using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteka_2.ViewModels
{
    class ModuleViewModel : IModule
    {
        ILogin _loginClass;
        IFileConfig _fileConfig;
        IConnectionLogin _sqlLogin;
        MainWindow _mainWindow;
        public event PropertyChangedEventHandler LoginProperty;

        public string configFileName { get; } = "_config.txt";
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

        public ModuleViewModel(IConnectionLogin sqlLogin, IFileConfig fileConfig, ILogin login)
        {
            _loginClass = login;
            _fileConfig = fileConfig;
            _sqlLogin = sqlLogin;
            _mainWindow = new MainWindow(this);
            if (_fileConfig.fileEx(configFileName))
            {
                SqlProfile = _fileConfig.GetInfoSQL(configFileName);
                openLogin();
            }
            else
            {
                openReconnect();
            }
        }

        public async Task<bool> login<T>(string name, string password, object sender) where T : IUserProfile, new()
        {
            bool output = false;
            try
            {
                List<T> users = await _loginClass.getUsers<T>(this);
                foreach (var item in users)
                {
                    if (name == item.login && password == item.password)
                    {
                        user = new T() { firstName = item.firstName, lastName = item.lastName, login = item.login, password = item.password };
                        IsLogged = true;
                        output = true;
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
            return output;
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
                _fileConfig.ConfigWrite(SqlProfile, configFileName);
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
