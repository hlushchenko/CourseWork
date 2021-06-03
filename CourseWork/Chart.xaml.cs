using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace CourseWork
{
    public partial class Chart
    {
        private LineSeries _graph;
        private LineSeries _solution;


        public Chart()
        {
            _solution = new LineSeries();
            _solution.Title = "Solution";
            _solution.Values = new ChartValues<ObservablePoint>();
            _solution.Fill = Brushes.Transparent;
            _graph = new LineSeries();
            _graph.Title = "graph";
            _graph.Values = new ChartValues<ObservablePoint>();
            _graph.Fill = Brushes.Transparent;
            _graph.PointGeometrySize = 0;
            InitializeComponent();
            MainChart.AxisY[0].LabelFormatter = value => $"{value:F1}";
            MainChart.AxisX[0].LabelFormatter = value => $"{value:F1}";
            MainChart.Series = new SeriesCollection {_graph, _solution};
        }
        /// <summary>
        /// Будує графік за вказаними точками у вказаних межах 
        /// </summary>
        
        public void Add(double[] values, double leftLimit, double rightLimit)
        {
            if (!(leftLimit < rightLimit)) return;
            double delta = (rightLimit - leftLimit) / (values.Length-1);
            _graph.Values.Clear();
            for (int i = 0; i < values.Length; i++)
            {
                _graph.Values.Add(new ObservablePoint(leftLimit + delta * i, values[^(i+1)]));
            }
        }
        /// <summary>
        /// Відображає на графіку точку, яка є коренем даного поліному
        /// </summary>
        public void AddSolution(double x, double y)
        {
            ClearSolution();
            _solution.Values.Add(new ObservablePoint(x, y));
        }

        /// <summary>
        /// Очищає графік від відображених точок – коренів поліному
        /// </summary>
        public void ClearSolution()
        {
            _solution.Values.Clear();
        }
    }
}