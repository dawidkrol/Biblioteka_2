using Biblioteka_2.DemoData;
using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Biblioteka_2.Controls
{
    /// <summary>
    /// Interaction logic for stats.xaml
    /// </summary>
    public partial class stats : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public stats(List<Stats> stats)
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            double[] vjeden = new double[12];
            double[] vdwa = new double[12];
            double[] vtrzy = new double[12];

            stats.ForEach(x =>
            {
                if (x.rok == DateTime.Now.Year)
                {
                    vjeden[x.miesiac - 1] = x.ilosc;
                }
                if (x.rok == (DateTime.Now.Year - 1))
                {
                    vdwa[x.miesiac - 1] = x.ilosc;
                }
                if (x.rok == (DateTime.Now.Year - 2))
                {
                    vtrzy[x.miesiac - 1] = x.ilosc;
                }
            });

            SeriesCollection.Add(
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = (DateTime.Now.Year - 2).ToString(),
                    Values = new ChartValues<double>(vtrzy)
                });

            SeriesCollection.Add(
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = (DateTime.Now.Year - 1).ToString(),
                    Values = new ChartValues<double>(vdwa)
                });

            SeriesCollection.Add(
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = DateTime.Now.Year.ToString(),
                    Values = new ChartValues<double>(vjeden)
                });

            Labels = new[] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
