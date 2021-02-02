using Biblioteka_2.Data;
using Biblioteka_2.DemoData;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka_2.Controls
{
    /// <summary>
    /// Interaction logic for Wypozyczanie.xaml
    /// </summary>
    public partial class Wypozyczanie : UserControl
    {
        GetDeomoData _data;
        IModule _module;
        public Wypozyczanie(GetDeomoData data,IModule module)
        {
            _data = data;
            _module = module;
            InitializeComponent();
            UsersList();
            BooksList();
        }
        async void UsersList()
        {
            users.ItemsSource = await _data.GetReaders(_module.SqlProfile.connectionString.ToString());
        }
        async void BooksList()
        {
            books.ItemsSource = await _data.GetAvailableBooks(_module.SqlProfile.connectionString.ToString());
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(dataOddania.ToString()))
            {
                dataOddania.BorderBrush = Brushes.Red;
                return;
            }
            if(users.SelectedItem == null)
            {
                users.BorderBrush = Brushes.Red;
                return;
            }
            if (books.SelectedItem == null)
            {
                books.BorderBrush = Brushes.Red;
                return;
            }
            try
            {
                ReaderModel reader = (ReaderModel)users.SelectedItem;
                AvailableBooks book = (AvailableBooks)books.SelectedItem;
                DateTime time = (DateTime)dataOddania.SelectedDate;

                _data.NewRental(_module.SqlProfile.connectionString.ToString(), book, reader, time);

                books.BorderBrush = Brushes.Transparent;
                dataOddania.BorderBrush = Brushes.Transparent;
                dataOddania.BorderBrush = Brushes.Transparent;

                users.SelectedItem = null;
                books.SelectedItem = null;
                dataOddania.SelectedDate = null;
            }
            catch
            {
                books.BorderBrush = Brushes.Red;
                users.BorderBrush = Brushes.Red;
                dataOddania.BorderBrush = Brushes.Red;
            }
        }
    }
}
