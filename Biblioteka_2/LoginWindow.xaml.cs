using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Module module = new Module();
            module.login(LoginName.Text, passw.Password, this);
            if (Module.user != null)
            {
                main.Text = "Zalogowano";
                this.Close();
            }
            else
            {
                main.Text = "!!!BŁĄD!!!\nPodaj dane do połączenia z serwerem sql";
            }
        }
    }
}
