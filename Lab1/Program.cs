using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Expression expression = new Expression();

            //while (true)
            //{
            //    Console.WriteLine(expression.CalculateExpression(Console.ReadLine()));
            //}

            //EquationWithOneUnknown equation = new EquationWithOneUnknown();

            //while (true) 
            //{
            //    double x1 = Convert.ToDouble(Console.ReadLine());
            //    double x2 = Convert.ToDouble(Console.ReadLine());
            //    Console.WriteLine(String.Join(' ', equation.GetArrPoints(x1,x2,10)));
            //}

            EquationWithOneUnknown equation = new EquationWithOneUnknown();

            while (true) 
            {
                string equ = Console.ReadLine();
                double x1 = Convert.ToDouble(Console.ReadLine());
                double x2 = Convert.ToDouble(Console.ReadLine());

                equation.SolveTheEquation(equ, x1, x2);
            }
        }

    }
}
