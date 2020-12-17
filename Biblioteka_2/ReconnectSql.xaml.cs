using System.Windows;

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
            //SqlIp.Text = Module.SqlProfile.ip ?? " ";
            //DBName.Text = Module.SqlProfile.databaseName ?? " ";
            //UserName.Text = Module.SqlProfile.name ?? " ";
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Module module = new Module();
            if (module.UpdateSqlProfile(SqlIp.Text, DBName.Text, UserName.Text, password.Password, this))
            {
                title_sql.Text = "Połączono";
                this.Close();
                LoginWindow loginWindow = new LoginWindow();
            }
            else
            {
                title_sql.Text = "Błąd";
            }
        }
    }
}
