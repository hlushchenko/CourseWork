using System;

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
                value += _coefficient[^(i+1)] * Math.Pow(x, i);
            }
            return value;
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
            for (int i = 0; i < _coefficient.Length-1; i++)
            {
                output += $"{_coefficient[i]} x^{_coefficient.Length - 1 - i} + ";
            }
            output += $"{_coefficient[^1]}";
            return output;
        }
    }
}