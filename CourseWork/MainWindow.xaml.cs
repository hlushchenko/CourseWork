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

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Calculator Calculator;
        public MainWindow()
        {
            Calculator = new Calculator();
            InitializeComponent();
            BisectionRadioButton.IsChecked = true;
        }
        
        private void PolynomialSizeInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int size = 0;
            if (int.TryParse(PolynomialSizeInput.Text, out size))
            {
                if (size>16)
                {
                    size = 16;
                    PolynomialSizeInput.Text = "16";
                }
                Box.Count = size+1;
            }
            else
            {
                Box.Count = 0;
            }
            Box.Show();
        }
        
        

        #region RadioButtons

        private void BisectionRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Calculator.MethodSelected = Calculator.Method.Bisection;
            RightArgumensPanel.Visibility = Visibility.Visible;
            StartValueTextBlock.Visibility = Visibility.Collapsed;
            LeftLimitTextBlock.Visibility = Visibility.Visible;
        }

        private void NewtonRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Calculator.MethodSelected = Calculator.Method.Newton;
            LeftLimitTextBlock.Visibility = Visibility.Collapsed;
            StartValueTextBlock.Visibility = Visibility.Visible;
            RightArgumensPanel.Visibility = Visibility.Collapsed;
        }

        private void SecantRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Calculator.MethodSelected = Calculator.Method.Secant;
            LeftLimitTextBlock.Visibility = Visibility.Visible;
            StartValueTextBlock.Visibility = Visibility.Collapsed;
            RightArgumensPanel.Visibility = Visibility.Visible;
        }

        #endregion

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
            if (!double.TryParse(LeftLimitTextBox.Text,out Calculator.Left))
            {
                Calculator.Left = 0;
            }
            if (!double.TryParse(RightLimitTextBox.Text,out Calculator.Right))
            {
                Calculator.Right = 0;
            }
            
            Calculator._polynomial = Box.Value;
            List<double> values = new List<double>();
            MainChart.Add(Calculator._polynomial.Values(Calculator.Left, Calculator.Right, 10), Calculator.Left, Calculator.Right);
        }
        
    }
}