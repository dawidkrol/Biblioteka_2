using Biblioteka_2.Data;
using Biblioteka_2.DemoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Biblioteka_2.Controls
{
    public partial class Books : UserControl
    {
        IModule _module;
        GetDeomoData _data;
        List<SimlpeAthor> autors;
        List<Categories> categories;
        public Books(IModule module)
        {
            _data = new GetDeomoData();
            _module = module;
            InitializeComponent();
            listAuthors();
            listCategories();
        }
        public void listCategories()
        {
            categories = _data.ListOfCategories(_module.SqlProfile.connectionString.ToString());
            string[] vs = new string[categories.Count];
            for (int i = 0; i < vs.Length; i++)
            {
                vs[i] = categories[i].nazwa_kategorii;
            }
            kategoria.ItemsSource = vs;
        }
        public void listAuthors()
        {
            autors = _data.ListOfAuthors(_module.SqlProfile.connectionString.ToString());
            string[] vs = new string[autors.Count];
            for(int i = 0;i < vs.Length; i++)
            {
                vs[i] = autors[i].imie_nazwisko;
            }
            autor.ItemsSource = vs;
        }

        private void dodaj_książke(object sender, RoutedEventArgs e)
        {
            try
            {
                AvailableBooks book = new AvailableBooks();
                book.ISBN = isbn.Text;
                book.Rok_wydania = rok_wydania.Text;
                book.Wydawca = wydawca.Text;
                book.Tytuł = tytul.Text;

                SimlpeAthor author = new SimlpeAthor();

                author.imie_nazwisko = autor.SelectedItem?.ToString() ?? throw new Exception();
                autors.ForEach(x =>
                {
                    if (x.imie_nazwisko == author.imie_nazwisko)
                        author.Nr_autora = x.Nr_autora;
                });

                categories.ForEach(x =>
                {
                    if(x.nazwa_kategorii == kategoria.SelectedItem?.ToString())
                    {
                        book.Kategoria = x.id_kategorii.ToString();
                    }
                });


                _data.NewBook(_module.SqlProfile.connectionString.ToString(), book, author);
            }
            catch
            {
                buttOn.Background = Brushes.DarkRed;
            }
            refresh();
            Clean();
        }

        private void usuń_książkę(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AvailableBooks book = button.DataContext as AvailableBooks;
            _data.delBook(_module.SqlProfile.connectionString.ToString(), book);
            refresh();
        }

        private async void refresh()
        {
            DataContext = await _data.GetAvailableBooks(_module.SqlProfile.connectionString.ToString());
        }

        private void NowyAutor(object sender, RoutedEventArgs e)
        {
            NewAutor newAutor = new NewAutor(_data, _module,this);
            newAutor.InitializeComponent();
            newAutor.Visibility = Visibility.Visible;
            listAuthors();
        }

        private void Clean()
        {
            autor.SelectedItem = null;
            tytul.Text = null;
            isbn.Text = null;
            rok_wydania.Text = null;
            wydawca.Text = null;
            kategoria.Text = null;
        }

        private async void fBooks_KeyUp(object sender, KeyEventArgs e)
        {
            var filter = await _data.GetAvailableBooks(_module.SqlProfile.connectionString.ToString());
            var filtered = filter.Where(x => (x.Tytuł.ToLowerInvariant().StartsWith(fBooks.Text.ToLowerInvariant()) || x.Wydawca.ToLowerInvariant().StartsWith(fBooks.Text.ToLowerInvariant())));
            DataContext = filtered;
        }
    }
}
