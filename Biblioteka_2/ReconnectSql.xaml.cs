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
    /// Interaction logic for ReconnectSql.xaml
    /// </summary>
    public partial class ReconnectSql : Window
    {
        public ReconnectSql()
        {
            InitializeComponent();
            SqlIp.Text = Module.SqlProfile.ip;
            DBName.Text = Module.SqlProfile.databaseName;
            UserName.Text = Module.SqlProfile.name;
        }

        private void hideReconnect(object sender, string e)
        {
            this.Close();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Module module = new Module();
            if(module.Updatesqlprofile(SqlIp.Text, DBName.Text, UserName.Text, password.Password,this))
            {
                title_sql.Text = "Połączono";
            }
            else
            {
                title_sql.Text = "Błąd";
            }
        }
    }
}
