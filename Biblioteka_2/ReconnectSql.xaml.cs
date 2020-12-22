using System.Windows;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for ReconnectSql.xaml
    /// </summary>
    public partial class ReconnectSql : Window
    {
        Module _module = null;
        public ReconnectSql(Module module)
        {
            InitializeComponent();
            _module = module;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (_module.UpdateSqlProfile(SqlIp.Text, DBName.Text, UserName.Text, password.Password, this))
            {
                title_sql.Text = "Połączono";
                this.Close();
            }
            else
            {
                title_sql.Text = "Błąd";
            }
        }
    }
}
