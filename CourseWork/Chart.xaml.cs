using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace CourseWork
{
    public partial class Chart : UserControl
    {
        private LineSeries _series;
        
        public Chart()
        {
            _series = new LineSeries();
            _series.Values = new ChartValues<ObservablePoint>();
            _series.Fill = Brushes.Transparent;
            _series.PointGeometrySize = 0;
            InitializeComponent();
            MainChart.AxisY[0].LabelFormatter = value => $"{value:F1}";
            MainChart.AxisX[0].LabelFormatter = value => $"{value:F1}";
            MainChart.Series = new SeriesCollection {_series};
        }
        
        public void Add(double[] values, double leftLimit, double rightLimit)
        {
            if (leftLimit<rightLimit)
            {
                double delta = (rightLimit - leftLimit) / (values.Length-1);
                _series.Values.Clear();
                for (int i = 0; i < values.Length; i++)
                {
                    _series.Values.Add(new ObservablePoint(leftLimit + delta * i, values[i]));
                }
            }
            
        }
    }
}