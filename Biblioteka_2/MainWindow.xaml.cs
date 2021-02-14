using Biblioteka_2.Controls;
using Biblioteka_2.Data;
using Biblioteka_2.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka_2
{
    public partial class MainWindow : Window
    {
        IModule _module;
        GetDeomoData _data = new GetDeomoData();

        public MainWindow(IModule module)
        {
            _module = module;
            _module.LoginProperty += allowShow;
            InitializeComponent();
            title.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
            LogoutPanel.Visibility = Visibility.Hidden;
        }

        private void allowShow(object sender, PropertyChangedEventArgs e)
        {
            IModule module = (ModuleViewModel)sender;
            if (module.IsLogged == true)
            {
                this.Visibility = Visibility.Visible;
                _ = GetThisData();
            }
            else
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        public async void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "delayed":
                    usc = new delayed(_data, _module);
                    GridMain.Children.Add(usc);
                    usc.DataContext = await _data.GetNotDelivered(_module.SqlProfile.connectionString.ToString(), false);
                    break;
                case "stats":
                    usc = new stats(await _data.GetStats(_module.SqlProfile.connectionString.ToString()));
                    GridMain.Children.Add(usc);
                    break;
                case "UserAdd":
                    usc = new NewUser(_module);
                    usc.DataContext = await _data.GetReaders(_module.SqlProfile.connectionString.ToString());
                    GridMain.Children.Add(usc);
                    break;
                case "Lista_książek":
                    usc = new Books(_module);
                    GridMain.Children.Add(usc);
                    usc.DataContext = await _data.GetAvailableBooks(_module.SqlProfile.connectionString.ToString());
                    break;
                case "ItemCreate":
                    usc = new Wypozyczanie(_data, _module);
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private async Task<string> GetThisData()
        {
            Progress<int> _progress = new Progress<int>();
            _progress.ProgressChanged += Progress;
            string output = await Task.Run(() => _data.thisIsData(_progress));
            return output;
        }

        private void Progress(object sender, int e)
        {
            this.Dispatcher.Invoke(() =>
            {
                pogersBarUpper.Value = e;
                progresBarLeft.Value = e;
                if (e == 100)
                {
                    title.Visibility = Visibility.Visible;
                    LogoutPanel.Visibility = Visibility.Visible;
                    //BlinkingLogo();
                }
                else
                {
                    title.Visibility = Visibility.Hidden;
                    LogoutPanel.Visibility = Visibility.Hidden;
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _module.logout();
            GridMain.Children.Clear();
        }
    }
}
