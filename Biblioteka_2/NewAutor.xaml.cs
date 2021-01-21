using Biblioteka_2.Controls;
using Biblioteka_2.Data;
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
    /// Interaction logic for NewAutor.xaml
    /// </summary>
    public partial class NewAutor : Window
    {
        GetDeomoData _data;
        IModule _module;
        Books _book;
        public NewAutor(GetDeomoData data,IModule module,Books books)
        {
            _data = data;
            _module = module;
            _book = books;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _data.NewAutor(_module.SqlProfile.connectionString.ToString(), imie.Text, nazwisko.Text);
            }
            catch
            {
                buttOn.Background = Brushes.DarkRed;
            }
            _book.listAuthors();
            this.Close();
        }
    }
}
