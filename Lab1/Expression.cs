using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Expression : PostfixRecord
    {
        public Expression() { }

        public Expression(string expressionInfixRecord) : base(expressionInfixRecord)
        {
            InfixRecord = expressionInfixRecord;
        }

        /// <summary>
        /// Знаходить значення виразу
        /// </summary>
        /// <param name="expression"></param>
        public double CalculateExpression(string expression)
        {
            if(string.IsNullOrEmpty(expression) || expression == " ") 
            {
                Console.WriteLine("Запишіть рівняння");
                return 0;
            }

            InfixRecord = expression;
            InPostfixRecord = ConvertToPostfixRecord(expression);

            return CalculateExpression(InPostfixRecord);
        }

        /// <summary>
        /// Знаходить значення виразу
        /// </summary>
        /// <param name="potfixRecord"></param>
        /// <returns></returns>
        public double CalculateExpression(List<string> potfixRecord) 
        {
            Stack<string> stack = new Stack<string>();

            foreach (string str in potfixRecord)
            {
                double num;
                bool Continue = true;

                if (Double.TryParse(str, out num))
                    stack.Push(str);
                else
                {
                    double b = Convert.ToDouble(stack.Pop());
                    double a;

                    switch (str)
                    {
                        case "+":
                            a = EmptyStack();
                            stack.Push($"{a + b}");
                            break;
                        case "-":
                            a = EmptyStack();
                            stack.Push($"{a - b}");
                            break;
                        case "*":
                            a = EmptyStack();
                            stack.Push($"{a * b}");
                            break;
                        case "/":

                            if (b == 0)
                            {
                                Console.WriteLine("Знаменник не може бути рівним 0");
                                Continue = false;
                                break;
                            }

                            a = EmptyStack();
                            stack.Push($"{a / b}");
                            break;
                        case "^":
                            a = Convert.ToDouble(stack.Pop());
                            stack.Push($"{Math.Pow(a, b)}");
                            break;
                        case "!":
                            stack.Push($"{Factorial(Convert.ToInt32(b))}");
                            break;
                        case "cos":
                            stack.Push($"{Math.Cos(b * Math.PI / 180)}");
                            break;
                        case "sin":
                            stack.Push($"{Math.Sin(b * Math.PI / 180)}");
                            break;
                        case "tan":

                            if (b == 90)
                            {
                                Console.WriteLine("При b = 90 функцыя невизначенна");
                                Continue = false;
                                break;
                            }

                            stack.Push($"{Math.Sin(b * Math.PI / 180) / Math.Cos(b * Math.PI / 180)}");
                            break;
                        case "ctg":

                            if (b == 90)
                            {
                                Console.WriteLine("При b=90 функцыя невизначенна");
                                Continue = false;
                                break;
                            }
                            stack.Push($"{Math.Cos(b * Math.PI / 180) / Math.Sin(b * Math.PI / 180)}");
                            break;
                        case "log":

                            if (b <= 0)
                            {
                                Console.WriteLine("При х < 0 функція не вищначеннаo");
                                Continue = false;
                                return 0;
                            }

                            a = Convert.ToDouble(stack.Pop());
                            stack.Push($"{Math.Log(b,a)}");
                            break;
                        case "exp":
                            stack.Push($"{Math.Exp(b)}");
                            break;
                        default:
                            stack.Push("Некоректні данні");
                            Continue = true;
                            break;
                    }

                    double EmptyStack()
                    {
                        if (stack.Count > 0)
                            return Convert.ToDouble(stack.Pop());
                        else
                            return 0;
                    }

                }

                if (Continue == false)
                    break;
            }

            string result = "0,0";

            if (stack.Count > 0)
                result = stack.Pop();

            return Convert.ToDouble(result);
        }


        private int Factorial(int n)
        {
            int number = 1;

            for (int i = 1; i <= n; i++)
                number *= i;
            return number;
        }
    }
}
