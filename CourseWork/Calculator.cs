using System;
using System.Windows;

namespace CourseWork
{
    public class Calculator
    {
        public Polynomial Polynomial;
        private double _left;
        private double _right;
        private double _accuracy;
        
        

        public double Left
        {
            get => _left;
            set
            {
                if (_right < value)
                {
                    _left = _right;
                    _right = value;
                }
                else
                {
                    _left = value;
                }
            }
        }

        public double Right
        {
            get => _right;
            set
            {
                if (_left > value)
                {
                    _right = _left;
                    _left = value;

                }
                else
                {
                    _right = value;
                }
            }
        }

        public double Accuracy { get => _accuracy;
            set
            {
                if (value > 1)
                {
                    _accuracy = 1;
                }  else if (value <= 0)
                {
                    _accuracy = 0;
                }
                else
                {
                    _accuracy = value;
                }
            }
        }
        public Method MethodSelected = Method.Bisection;

        public Calculator()
        {
            _left = 0;
            _right = 0;
            Polynomial = new Polynomial(0);
        }

        public enum Method
        {
            Bisection,
            Newton,
            Secant,
        }

        public double? Calculate()
        {
            double? result = 0;
            switch (MethodSelected)
            {
                case Method.Bisection:
                    result = Bisection();
                    break;
                case Method.Newton:
                    result = Newton();
                    break;
                case Method.Secant:
                    result = Secant();
                    break;
            }

            if (result != null) result = Math.Round((double) result, (int)-Math.Log10(Accuracy), MidpointRounding.ToEven);
            return result;
        }

        private double? Bisection()
        {
            double middle = 0;
            if (Math.Sign(Polynomial.Value(Left)) == Math.Sign(Polynomial.Value(Right))) return null;
            while (Right - Left > Accuracy/10)
            {
                middle = Left + (Right - Left) / 2;
                if (Math.Sign(Polynomial.Value(Left)) != Math.Sign(Polynomial.Value(middle)))
                {
                    Right = middle;
                }
                else Left = middle;
            }
            return middle;
        }
        
        private double Newton()
        {
            Polynomial derivative = Polynomial.Derivative();
            double x0 = Left;
            double x1 = x0 - Polynomial.Value(x0) / derivative.Value(x0);
            while (Math.Abs(x1 - x0) > Accuracy/10)
            {
                x0 = x1;
                x1 = x0 - Polynomial.Value(x0) / derivative.Value(x0);
            }
            return x1;
        }
        
        public double? Secant()
        {
            if (Math.Sign(Polynomial.Value(Left)) == Math.Sign(Polynomial.Value(Right))) return null;
            while (Math.Abs(Left-Right) > Accuracy/10)
            {
                Left = Right - (Right - Left) * Polynomial.Value(Right) /
                    (Polynomial.Value(Right) - Polynomial.Value(Left));
                Right = Left - (Left - Right) * Polynomial.Value(Left) /
                    (Polynomial.Value(Left) - Polynomial.Value(Right));
            }
            return Right;
        }
    }
}