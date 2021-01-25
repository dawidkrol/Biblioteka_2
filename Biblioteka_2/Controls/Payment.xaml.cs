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
using System.Windows.Shapes;

namespace Biblioteka_2.Controls
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        DemoDelayedBase _demo;
        delayed _delayed;
        public Payment(DemoDelayedBase demo,delayed delayed)
        {
            InitializeComponent();
            _demo = demo;
            _delayed = delayed;
            tekst.Content = $"Dodatkowa opłata: {_demo.oplata}";
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _delayed.handAsync(_demo);
        }
    }
}
