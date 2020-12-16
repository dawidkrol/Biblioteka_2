using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserProfile user = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            SqlLogin sqlLogin = new SqlLogin();
            try
            {
                if(sqlLogin.Login(SqlIp.Text, DBName.Text, UserName.Text, password.Password,out user));
                {
                    main.Text = "Połączono";
                }
                main.FontSize = 10;
                main.Text = user.name + "\n" + user.password + "\n" + user.ip + "\n" + user.databaseName;
            }
            catch(Exception ex)
            {
                main.Text = ex.Message;
            }
        }
    }
}
