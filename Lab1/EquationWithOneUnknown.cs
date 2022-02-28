using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class EquationWithOneUnknown : Equations
    {
        public EquationWithOneUnknown(){ }
        public EquationWithOneUnknown(string equation) : base(equation) { }

        public void SolveTheEquation() 
        {
            if(String.IsNullOrWhiteSpace(Equation) | String.IsNullOrEmpty(Equation)) 
            {
                Console.WriteLine("Запишіть рівняння");
            }


        }
        
        private List<(double x1, double x2)> SeparationOfRoots(double x1, double x2, double epsilont = 0.01) 
        {
            List<(double x1, double x2)> intervals = new List<(double x1, double x2)>();

            if(Math.Abs(x1 - x2) < epsilont) 
            {
                //TODO: перейти до уточнення коренів
            }

            double distance = Math.Sqrt(Math.Pow(x1 - x2, 2));
            double minDistance = distance / 10;




            return intervals;
        }
    }
}
