using System;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Calculator _calculator;

        public MainWindow()
        {
            _calculator = new Calculator();
            InitializeComponent();
            BisectionRadioButton.IsChecked = true;
        }
        
        /// <summary>
        /// Опрацювання події зміни даних у текстовому файлі
        /// </summary>
        private void PolynomialSizeInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int size = 0;
            if (int.TryParse(PolynomialSizeInput.Text, out size))
            {
                if (size > 16)
                {
                    size = 16;
                    PolynomialSizeInput.Text = "16";
                }

                Box.Count = size + 1;
            }
            else
            {
                Box.Count = 0;
            }

            Box.Show();
        }

        /// <summary>
        /// Опрацювання подій для вибору методу
        /// </summary>
        
        #region RadioButtons

        private void BisectionRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            _calculator.MethodSelected = Calculator.Method.Bisection;
            RightArgumensPanel.Visibility = Visibility.Visible;
            RightLimitTextBlock.Visibility = Visibility.Visible;
            StartValueTextBlock.Visibility = Visibility.Collapsed;
            LeftLimitTextBlock.Visibility = Visibility.Visible;
            RightSecantTextBlock.Visibility = Visibility.Collapsed;
            LeftSecantTextBlock.Visibility = Visibility.Collapsed;
        }

        private void NewtonRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            _calculator.MethodSelected = Calculator.Method.Newton;
            LeftLimitTextBlock.Visibility = Visibility.Collapsed;
            StartValueTextBlock.Visibility = Visibility.Visible;
            LeftSecantTextBlock.Visibility = Visibility.Collapsed;
            RightArgumensPanel.Visibility = Visibility.Collapsed;
        }

        private void SecantRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            RightArgumensPanel.Visibility = Visibility.Visible;
            _calculator.MethodSelected = Calculator.Method.Secant;
            RightLimitTextBlock.Visibility = Visibility.Collapsed;
            LeftLimitTextBlock.Visibility = Visibility.Collapsed;
            StartValueTextBlock.Visibility = Visibility.Collapsed;
            RightSecantTextBlock.Visibility = Visibility.Visible;
            RightLimitTextBox.Visibility = Visibility.Visible;
            LeftSecantTextBlock.Visibility = Visibility.Visible;
        }

        #endregion
        
        /// <summary>
        /// Опрацювання події натискання на кнопку
        /// </summary>
        private void CalculateButton_OnClick(object sender, RoutedEventArgs e)
        {
            double leftGraphBound, rightGraphBound;
            if (double.TryParse(LeftLimitTextBox.Text.Replace('.', ','), out var left))
                _calculator.Left = left;
            else
                _calculator.Left = 0;
            if (double.TryParse(RightLimitTextBox.Text.Replace('.', ','), out var right))
                _calculator.Right = right;
            else
                _calculator.Right = 0;
            if (double.TryParse(AccuracyTextBox.Text.Replace('.', ','), out var accuracy))
                _calculator.Accuracy = accuracy;
            else
                _calculator.Accuracy = 0.1;
            if (!double.TryParse(GraphLeftTextBox.Text.Replace('.', ','), out leftGraphBound))
            {
                leftGraphBound = -1;
                GraphLeftTextBox.Text = "-1";
            }

            if (!double.TryParse(GraphRightTextBox.Text.Replace('.', ','), out rightGraphBound))
            {
                rightGraphBound = 1;
                GraphRightTextBox.Text = "1";
            }

            if (leftGraphBound > rightGraphBound)
            {
                double temp = leftGraphBound;
                leftGraphBound = rightGraphBound;
                rightGraphBound = temp;
            }

            _calculator.Polynomial = Box.Value;
            MainChart.Add(_calculator.Polynomial.Values(rightGraphBound, leftGraphBound, 10), leftGraphBound,
                rightGraphBound);
            double? result = _calculator.Calculate();
            MainChart.ClearSolution();
            if (result != null)
            {
                if ((double) result <= rightGraphBound && (double) result >= leftGraphBound)
                {
                    MainChart.AddSolution((double) result, _calculator.Polynomial.Value((double) result));
                }

                ResultTextBlock.Text = "Результат: " + result;
            }
            else
            {
                ResultTextBlock.Text = "Неможливо знайти корінь";
            }
        }
    }
}