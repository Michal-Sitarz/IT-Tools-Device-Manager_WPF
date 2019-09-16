using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace PieChart_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[] MyChartValues { get; set; }

        public MainWindow()
        {

            InitializeComponent();

            mainPieChart.StartingRotationAngle = 330.0;

            mainPieChart.Series.Add(
                new PieSeries
                {
                    Title = "Desktops",
                    DataLabels=true,
                    Fill = Brushes.DarkOrange,
                    Values = new ChartValues<int> { 225 }
                    
                });

            mainPieChart.Series.Add(
                new PieSeries
                {
                    Title = "Notebooks",
                    DataLabels = true,
                    Fill = Brushes.MediumVioletRed,
                    Values = new ChartValues<int> { 138 }
                });

            updatePieChart();

        }

        private void updatePieChart()
        {
            mainPieChart.Series[0].Values[0] = 15;
            mainPieChart.Series[1].Values[0] = 29;

        }
    }

}
