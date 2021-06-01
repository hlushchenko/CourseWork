using System;
using System.Text;

namespace CourseWork
{
    public class Calculator
    {
        public Polynomial _polynomial;
        public double Left = 0;
        public double Right = 0;
        public Method MethodSelected = Method.Bisection;
        public enum Method
        {
            Bisection,
            Newton,
            Secant,
        }
        
        
    }
}