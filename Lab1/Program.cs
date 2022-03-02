using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expression expression = new Expression();
            while (true) 
            {
                Console.WriteLine(expression.CalculateExpression(expression.ConvertToPostfixRecord(Console.ReadLine())));
            }

            /*
            Equations equations = new Equations();

            while (true) 
            {
                equations.AddEquation(Console.ReadLine());
                double x = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(equations.FindTheValueOfTheFunction(x));
            }*/
        }

    }
}
