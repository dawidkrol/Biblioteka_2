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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        MainWindow _main;
        public Menu(MainWindow main)
        {
            _main = main;
            InitializeComponent();
        }

        private void Nieoddane_Click(object sender, RoutedEventArgs e)
        {
            var a = SelectionChangedEventArgs.Empty;
            _main.ListViewMenu_SelectionChanged(sender,null);
        }
    }
}
