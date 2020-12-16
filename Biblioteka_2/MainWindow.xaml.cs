using System;
using System.Windows;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void hidemain(object sender, object a)
        {
            this.Visibility = Visibility.Hidden;
            this.Close();
        }
        private void showmain(object sender, string e)
        {
            this.Visibility = Visibility.Visible;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Module module = new Module();
            module.login(Module.connectionstring, LoginName.Text, passw.Password,this);
            if(Module.user != null)
            {
                main.Text = Module.user._FirstName +"\n"+Module.user._LastName+"\n";
            }
            else
            {
                main.Text = "!!!BŁĄD!!!\nPodaj dane do połączenia z serwerem sql";
            }
        }
    }
}
