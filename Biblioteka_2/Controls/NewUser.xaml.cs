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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : UserControl
    {
        GetDeomoData data;
        IModule _module;
        public NewUser(IModule module)
        {
            InitializeComponent();
            _module = module;
            data = new GetDeomoData();
            tytuł.ItemsSource = Enum.GetValues(typeof(GetDeomoData.Tytul));
            profil.ItemsSource = Enum.GetValues(typeof(GetDeomoData.Profil));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReaderModel model = new ReaderModel();
                model.Imię = string.IsNullOrEmpty(oplata.Text) ? null : imię.Text;
                model.Nazwisko = string.IsNullOrEmpty(oplata.Text) ? null : nazwisko.Text;
                model.Tytuł = tytuł.SelectedItem.ToString() ?? null;
                model.Telefon = telefon.Text;
                model.Adres = adres.Text;
                model.Email = email.Text;
                model.Miasto = miasto.Text;
                model.Opłata = string.IsNullOrEmpty(oplata.Text) ? 0 : Convert.ToInt32(oplata.Text);
                model.Profil = profil.SelectedItem.ToString() ?? null;
                switch (model.Profil)
                {
                    case "Student":
                        model.Wartość = 15f;
                        break;
                    case "Dziecko":
                        model.Wartość = 10f;
                        break;
                    case "Dorosły":
                        model.Wartość = 20f;
                        break;
                    default:
                        model.Wartość = 0;
                        break;
                }
                data.AddReader(_module.SqlProfile.connectionString.ToString(), model);
            }
            catch(Exception)
            {
                buttOn.Background = Brushes.DarkRed;
            }
            RefereschData();
            SetNull();
        }

        private void DelUser(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ReaderModel model = button.DataContext as ReaderModel;
            data.delUser(_module.SqlProfile.connectionString.ToString(), model);
            RefereschData();
        }

        private void SetNull()
        {
            imię.Text = null;
            nazwisko.Text = null;
            tytuł.SelectedItem = null;
            telefon.Text = null;
            adres.Text = null;
            email.Text = null;
            miasto.Text = null;
            oplata.Text = null;
            profil.SelectedItem = null;
        }

        private async void RefereschData()
        {
            DataContext = await data.GetReaders(_module.SqlProfile.connectionString.ToString());
        }
    }
}
