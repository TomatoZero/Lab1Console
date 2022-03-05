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
        
        public List<double> SeparationOfRoots(double x1, double x2, double exponent = 0.01) 
        {
            List<double> result = new List<double>();

            if(Math.Abs(x1 - x2) < exponent) 
            {
                result.Add(RootInVerySmallInterval(x1, x2));
            }
            else 
            {
                double[] x = GetArrPoints(x1, x2, 10);

                for(int i = 0; i < x.Length - 2; i++) 
                    result.AddRange(SeparationOfRoots(x[i], x[i + 1], 1));         
            }
            return result;
        }

        public List<double> SeparationOfRoots(double x1, double x2, int numberOfRecursion = 1, double exponent = 0.01) 
        {
            List<double> result = new List<double>();

            if (numberOfRecursion < 3 | Math.Abs(x1-x2) > exponent)
            {
                double[] x = GetArrPoints(x1, x2, 10);              

                double[] y = new double[x.Length];
                for (int i = 0; i < y.Length - 1; i++)              //значення функції
                {
                    y[i] = FindTheValueOfTheFunction(x[i], EquationInPostfixReccord);
                }

                bool changeSing = false;                            //змінює знак
                int sing = 0;
                double[] derivative = new double[x.Length];         //похідна
                for(int i = 0; i < derivative.Length - 1; i++) 
                {
                    derivative[i] = (FindTheValueOfTheFunction(x[i] + 0.05, EquationInPostfixReccord) - y[i]) / 0.05;

                    if(i == 0)
                        sing = Math.Sign(derivative[i]);
                    else if(sing != Math.Sign(derivative[i])) 
                    {
                        changeSing = true;
                        break;
                    }
                }

                if(y[0] * y[y.Length - 1] < 0 && changeSing == false) 
                {
                    //TODO: Перейти до уточнення коренів
                    //result.Add(Метод який вертає корень)
                }
                else if (y[0] * y[y.Length - 1] > 0 && changeSing == false) 
                {
                    return result;
                }
                else 
                {
                    for (int i = 0; i < x.Length - 2; i++)
                        result.AddRange(SeparationOfRoots(x[i], x[i + 1], 1));
                }

            }
            else if(numberOfRecursion == 3 | Math.Abs(x1-x2) < exponent) 
            {
                result.Add(RootInVerySmallInterval(x1, x2));
            }

            return result;
        }

        private double RootInVerySmallInterval(double x1, double x2) 
        {
            double y1 = FindTheValueOfTheFunction(x1);
            double y2 = FindTheValueOfTheFunction(x2);

            double derivativeY1 = (FindTheValueOfTheFunction(x1 + 0.05) - y1) / 0.05;
            double derivativeY2 = (FindTheValueOfTheFunction(x2 + 0.05) - y2) / 0.05;

            if (y1 * y2 < 0 && derivativeY1 * derivativeY2 > 0)
            {
                //TODO: Перейти до уточнення коренів
            }

        }

        private double[] GetArrPoints(double x1, double x2, int numberOfInterval = 5) 
        {
            double[] points = new double[numberOfInterval + 1];
            points[0] = x1;
            points[points.Length - 1] = x2;

            double distance = Math.Sqrt(Math.Pow(x1 - x2, 2));
            double minDistance = distance / numberOfInterval;

            for (int i = 1; i < numberOfInterval; i++) 
            {
                points[i] = points[i - 1] + minDistance;
            }

            return points;
        }

        
    }
}
