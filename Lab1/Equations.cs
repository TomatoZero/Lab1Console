using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Equations : Expression
    {
        public Equations() { }
        public Equations(string equation): base(equation) { }

        public void AddEquation(string str) 
        {
            InfixRecord = str;
        }

        /// <summary>
        /// Знаходить занчення функції при заданному х
        /// </summary>
        /// <param name="x"></param>
        public double FindTheValueOfTheFunction(double x) 
        {
            if(String.IsNullOrEmpty(InfixRecord) | String.IsNullOrWhiteSpace(InfixRecord)) 
            {
                Console.WriteLine("Запишіть рівняння");
                return 0.0;
            }

            return FindTheValueOfTheFunction(x, InPostfixRecord);
        }

        public double FindTheValueOfTheFunction(double x, List<string> equationPostfix) 
        {
            InPostfixRecord = new List<string>(equationPostfix);
            for (int i = 0; i < InPostfixRecord.Count; i++)
            {
                if (InPostfixRecord[i] == "x")
                    InPostfixRecord[i] = $"{x}";
                else if (InPostfixRecord[i] == "-x")
                    InPostfixRecord[i] = $"-{x}";
            }

            return CalculateExpression(InPostfixRecord);
        }
    }
}
