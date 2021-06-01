using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace CourseWork
{
    public class Polynomial
    {
        private double[] _coefficient;

        public Polynomial(params double[] coefficient)
        {
            _coefficient = coefficient;
        }


        public double Value(double x)
        {
            double value = 0;
            for (int i = 0; i < _coefficient.Length; i++)
            {
                value += _coefficient[^(i + 1)] * Math.Pow(x, i);
            }

            return value;
        }

        public double[] Values(double left, double right, int count)
        {
            double delta = (right - left) / (count - 1);
            List<double> values = new List<double>();
            for (int i = 0; i < count; i++)
            {
                values.Add(Value(left + i * delta));
            }
            return values.ToArray();
        }

        public Polynomial Derivative()
        {
            double[] newCoefficient = new double[_coefficient.Length - 1];
            for (int i = 0; i < _coefficient.Length - 1; i++)
            {
                newCoefficient[i] = _coefficient[i] * (newCoefficient.Length - i);
            }

            return new Polynomial(newCoefficient);
        }

        public override string ToString()
        {
            string output = string.Empty;
            for (int i = 0; i < _coefficient.Length - 1; i++)
            {
                output += $"{_coefficient[i]} x^{_coefficient.Length - 1 - i} + ";
            }
            output += $"{_coefficient[^1]}";
            return output;
        }
    }
}