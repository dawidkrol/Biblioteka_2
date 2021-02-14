using System;
using System.Windows;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IModule _module = null;
        public LoginWindow(IModule module)
        {
            InitializeComponent();
            _module = module;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            bool logged = await _module.login<UserProfile>(LoginName.Text, passw.Password, this);
            if (logged == true)
            {
                main.Text = "Zalogowano";
                Close();
            }
            else
            {
                main.Text = "Niepoprawne dane, wprowadź ponownie";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_module.IsLogged == false)
            {
                App.Current.Shutdown();
            }
        }
    }
}
