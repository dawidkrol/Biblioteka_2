using Biblioteka_2.Data;
using Biblioteka_2.DemoData;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

                fUser.Text = null;
                fBook.Text = null;

                users.SelectedItem = null;
                books.SelectedItem = null;
                dataOddania.SelectedDate = null;

                BooksList();
                UsersList();
            }
            catch
            {
                books.BorderBrush = Brushes.Red;
                users.BorderBrush = Brushes.Red;
                dataOddania.BorderBrush = Brushes.Red;
            }
        }

        private async void fUser_KeyUp(object sender, KeyEventArgs e)
        {
            var filter = await _data.GetReaders(_module.SqlProfile.connectionString.ToString());
            var filtered = filter.Where(x => (x.Imię.ToLowerInvariant().StartsWith(fUser.Text.ToLowerInvariant()) || x.Nazwisko.ToLowerInvariant().StartsWith(fUser.Text.ToLowerInvariant())));
            users.ItemsSource = filtered;
        }

        private async void fBook_KeyUp(object sender, KeyEventArgs e)
        {
            var filter = await _data.GetAvailableBooks(_module.SqlProfile.connectionString.ToString());
            var filtered = filter.Where(x => (x.Tytuł.ToLowerInvariant().StartsWith(fBook.Text.ToLowerInvariant()) || x.Wydawca.ToLowerInvariant().StartsWith(fBook.Text.ToLowerInvariant())));
            books.ItemsSource = filtered;
        }
    }
}
