using Biblioteka_2.Data;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Module _module = new Module();
        GetData _data = null;

        public MainWindow()
        {
            _module.PropertyChanged += allowShow;
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
            _data = new GetData();
        }

        private void allowShow(object sender, PropertyChangedEventArgs e)
        {
            Module module = (Module)sender;
            if (module.IsLogged == true)
            {
                this.Visibility = Visibility.Visible;
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

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                //usc = new UserControlHome();
                //GridMain.Children.Add(usc);
                //break;
                case "ItemCreate":
                //usc = new UserControlCreate();
                //GridMain.Children.Add(usc);
                //break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _module.logout();
        }
    }
}
