using Biblioteka_2.Controls;
using Biblioteka_2.Data;
using System;
using System.Windows;
using System.Windows.Media;

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
        public NewAutor(GetDeomoData data, IModule module, Books books)
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
                if (!(string.IsNullOrWhiteSpace(imie.Text) && string.IsNullOrWhiteSpace(nazwisko.Text)))
                {
                    _data.NewAutor(_module.SqlProfile.connectionString.ToString(), imie.Text, nazwisko.Text);
                    _book.listAuthors();
                    this.Close();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                buttOn.Background = Brushes.DarkRed;
            }
        }
    }
}
