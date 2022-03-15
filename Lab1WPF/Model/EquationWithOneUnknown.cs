using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1WPF
{
    internal class EquationWithOneUnknown : Equations
    {
        public EquationWithOneUnknown(){ }
        public EquationWithOneUnknown(string equation) : base(equation) { }

        //public void SolveTheEquation() 
        //{
        //    if(String.IsNullOrWhiteSpace(Equation) | String.IsNullOrEmpty(Equation)) 
        //    {
        //        Console.WriteLine("Запишіть рівняння");
        //    }
        //}

        private static List<string> EquationInPostfix;

        public List<double> SolveTheEquation(string equation, double x1, double x2) 
        {
            InfixRecord = equation;

            EquationInPostfix = ConvertToPostfixRecord(equation);
            var c = SeparationOfRoots(x1, x2, 0.01);
            return c;
        }

        public List<double> SeparationOfRoots(double x1, double x2, double epselon = 0.01) 
        {
            List<double> result = new List<double>();

            if(Math.Abs(x1 - x2) <= epselon) 
            {
                result.Add(RootInVerySmallInterval(x1, x2));
            }
            else 
            {
                double[] x = GetArrPoints(x1, x2, 10);

                //
                double[] y = new double[x.Length];
                for (int i = 0; i < y.Length; i++)              //значення функції
                {
                    y[i] = FindTheValueOfTheFunction(x[i], EquationInPostfix);
                }

                double[] derivative = new double[x.Length];         //похідна
                for (int i = 0; i < derivative.Length; i++)
                {
                    derivative[i] = (FindTheValueOfTheFunction(x[i] + 0.001, EquationInPostfix) - y[i]) / 0.001;
                }
                //

                for (int i = 0; i < x.Length - 1; i++) 
                    result.AddRange(SeparationOfRoots(x[i], x[i + 1], 1));         
            }
            return result;
        }

        private List<double> SeparationOfRoots(double x1, double x2, int numberOfRecursion = 1, double epselon = 0.01) 
        {
            List<double> result = new List<double>();

            if (numberOfRecursion < 3 | Math.Abs(x1-x2) > epselon)
            {
                double[] x = GetArrPoints(x1, x2, 5);              //всі точки на проміжку

                double[] y = new double[x.Length];
                for (int i = 0; i < y.Length; i++)              //значення функції
                {
                    y[i] = FindTheValueOfTheFunction(x[i], EquationInPostfix);
                }

                bool changeSing = false;                            //змінює знак
                int sing = 0;
                double[] derivative = new double[x.Length];         //похідна
                for(int i = 0; i < derivative.Length; i++) 
                {
                    derivative[i] = (FindTheValueOfTheFunction(x[i] + 0.001, EquationInPostfix) - y[i]) / 0.001;

                    if (derivative[i].Equals(Double.NaN))
                        return result;

                    if(i == 0)
                        sing = Math.Sign(derivative[i]);
                    else if(sing != Math.Sign(derivative[i])) 
                    {
                        changeSing = true;
                        break;
                    }
                }


                if(y[0] * y[y.Length - 1] <= 0 && changeSing == false) 
                {
                    result.Add(ClarificationOfRoots(x1, x2));
                }
                else if (y[0] * y[y.Length - 1] > 0 && changeSing == false) 
                {
                    return result;
                }
                else 
                {
                    for (int i = 0; i < x.Length - 1; i++)
                    {
                        List<double> r = SeparationOfRoots(x[i], x[i + 1], numberOfRecursion + 1);

                        foreach(double t in r) 
                        {
                            if(!t.Equals(Double.NaN)) 
                            {
                                result.Add((double)t);
                            }
                        }
                    }
                }

            }
            else if(numberOfRecursion == 3 | Math.Abs(x1-x2) <= epselon) 
            {
                result.Add(RootInVerySmallInterval(x1, x2));
            }

            return result;
        }

        private double RootInVerySmallInterval(double x1, double x2) 
        {
            double y1 = FindTheValueOfTheFunction(x1);
            double y2 = FindTheValueOfTheFunction(x2);

            double derivativeY1 = (FindTheValueOfTheFunction(x1 + 0.001, EquationInPostfix) - y1) / 0.001;
            double derivativeY2 = (FindTheValueOfTheFunction(x2 + 0.001, EquationInPostfix) - y2) / 0.001;

            //if (y1 * y2 >= 0) // && derivativeY1 * derivativeY2 >= 0) //Випадок коли коренів на проміжку немає
            //    return Double.NaN;
            //else                                                //if (y1 * y2 < 0 && derivativeY1 * derivativeY2 > 0)
            //                                                    //і випадок коми потрібно розглядати детальніше але ми його пропускаємо бо точність не дозволяє цього
            //{
            //    return ClarificationOfRoots(x1, x2);
            //}

            if(y1 * y2 <= 0) 
            {
                if (derivativeY1 * derivativeY2 > 0 && derivativeY1 != 0 && derivativeY2 != 0)
                {
                    return ClarificationOfRoots(x1, x2);
                }
                else if (                                       
                    (derivativeY1 == 0 && derivativeY2 > 0) |
                    (derivativeY2 == 0 && derivativeY1 > 0)) 
                {
                    return ClarificationOfRoots(x1, x2);
                }
                else if(
                    (derivativeY1 == 0 && derivativeY2 < 0) |
                    (derivativeY2 == 0 && derivativeY1 < 0)) 
                {
                    return Double.NaN;
                }
                else
                    return Double.NaN;
            }
            else
                return Double.NaN;

        }

        private double ClarificationOfRoots(double x1, double x2, double epselon = 0.01) 
        {
            double y1 = FindTheValueOfTheFunction(x1, EquationInPostfix);
            double y2 = FindTheValueOfTheFunction(x2, EquationInPostfix);


            double denominator = (y2 - y1);
            double x;
            if (denominator != 0) 
                x = x1 - y1 * ((x2 - x1) / denominator);
            else
                x = x1 - y1 * ((x2 - x1) / epselon);

            double y = FindTheValueOfTheFunction(x, EquationInPostfix);

            if (Math.Abs(y) > epselon) 
            {
                if (y1 * y < 0)                
                    x = ClarificationOfRoots(x1, x, epselon); 
                else if(y2 * y < 0) 
                    x = ClarificationOfRoots(x, x2, epselon);
                else 
                {
                    throw new NotImplementedException();
                }
            }
            
            return (double)x;


            //double FormulaForMethod(double x1, double x2)
            //{
            //    double y1 = FindTheValueOfTheFunction(x1, EquationInPostfixReccord);
            //    double y2 = FindTheValueOfTheFunction(x2, EquationInPostfixReccord);

            //    return x1 - y1 * ((x2 - x1) / (y2 - y1));
            //}
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
