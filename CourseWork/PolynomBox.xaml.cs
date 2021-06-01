using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace CourseWork
{
    public partial class PolynomBox : UserControl
    {
        private List<TextBox> _fields;

        public int Count { get; set; }

        public PolynomBox()
        {
            InitializeComponent();
        }

        public void Show()
        {
            _fields = new List<TextBox>();
            WrapPanel.Children.Clear();
            for (int i = 1; i <= Count; i++)
            {
                StackPanel stackPanel = new StackPanel {Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 0)};
                var field = new TextBox {Width = 30, Height = 20, TextAlignment = TextAlignment.Right};
                _fields.Add(field);
                stackPanel.Children.Add(field);
                if (i != Count)
                {
                    TextBlock textBlock = PowerGenerator(Count - i);
                    stackPanel.Children.Add(textBlock);
                }

                WrapPanel.Children.Add(stackPanel);
            }
        }

        private static TextBlock PowerGenerator(int i)
        {
            TextBlock output = new TextBlock {FontSize = 14};
            output.Text += "x";

            output.Typography.Variants = FontVariants.Superscript;
            if (i > 1) output.Text += $"{i}";
            else output.Text += $" ";
            output.Text += " + ";
            return output;
        }

        public Polynomial Value {
            get
            {
                List<double> polynomial = new List<double>();
                foreach (var field in _fields)
                {
                    string currString = field.Text.Replace('.', ',');
                    double curr = 0;
                    if (double.TryParse(currString, out curr) )
                    {
                        polynomial.Add(curr);
                    }
                    else
                    {
                        polynomial.Add(0);
                    }
                }
                return new Polynomial(polynomial.ToArray());
            }
        }
    }
}