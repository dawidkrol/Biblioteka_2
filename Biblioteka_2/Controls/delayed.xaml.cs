using Biblioteka_2.Data;
using Biblioteka_2.DemoData;
using System.Windows.Controls;

namespace Biblioteka_2.Controls
{
    /// <summary>
    /// Interaction logic for delayed.xaml
    /// </summary>
    public partial class delayed : UserControl
    {
        enum stan
        {
            Nieoddane,
            Opóźnienie
        }
        GetDeomoData _data;
        IModule _module;
        public delayed(GetDeomoData data,IModule module)
        {
            _data = data;
            _module = module;
            InitializeComponent();
            DataContext = _data.GetNotDelivered(_module.SqlProfile.connectionString.ToString(), false);
            title.Text = ((stan)stan.Nieoddane).ToString().ToUpperInvariant();
            buttOn.Content = ((stan)stan.Opóźnienie).ToString().ToUpperInvariant();
        }

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (buttOn.Content.Equals(((stan)stan.Nieoddane).ToString().ToUpperInvariant()))
            {
                DataContext = await _data.GetNotDelivered(_module.SqlProfile.connectionString.ToString(),false);
                buttOn.Content = ((stan)stan.Opóźnienie).ToString().ToUpperInvariant();
                title.Text = ((stan)stan.Nieoddane).ToString().ToUpperInvariant();
            }
            else
            {
                DataContext = await _data.GetNotDelivered(_module.SqlProfile.connectionString.ToString(),true);
                buttOn.Content = ((stan)stan.Nieoddane).ToString().ToUpperInvariant();
                title.Text = ((stan)stan.Opóźnienie).ToString().ToUpperInvariant();
            }
        }

        private async void handing(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            DemoDelayedBase demo = button.DataContext as DemoDelayedBase;
            if (!string.IsNullOrWhiteSpace(demo.oplata))
            {
                Payment payment = new Payment(demo,this);
                payment.Show();
            }
            else
            {
                handAsync(demo);
            }
        }
        public async void handAsync(DemoDelayedBase demo)
        {
            _data.InsertHandingOverTheBook(_module.SqlProfile.connectionString.ToString(), demo);
            DataContext = await _data.GetNotDelivered(_module.SqlProfile.connectionString.ToString(), false);
            buttOn.Content = ((stan)stan.Nieoddane).ToString().ToUpperInvariant();
            title.Text = ((stan)stan.Nieoddane).ToString().ToUpperInvariant();
        }
    }
}
