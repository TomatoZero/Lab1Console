using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class PostfixRecord
    {
        private string infixRecort;
        private List<string> postfixRecort;

        public string InfixRecord
        {
            get => infixRecort;
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && value != "")
                    infixRecort = value;
            }
        }
        public List<string> InPostfixRecord
        {
            get => postfixRecort;
            set
            {
                if (value != null)
                    postfixRecort = value;
                
            }
        }

        public PostfixRecord() {}

        public PostfixRecord(string expressionInfixRecord)
        {
            InfixRecord = expressionInfixRecord;
        }

        private List<string> ConvertToPostfixRecord() 
        {
            return ConvertToPostfixRecord(InfixRecord);
        }

        /// <summary>
        /// Вираз в інфіксній формі переробляє в постфіксу
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> ConvertToPostfixRecord(string infixRecord)
        {
            if (infixRecord == null | String.IsNullOrWhiteSpace(infixRecord))
            {
                Console.WriteLine("Заповніть данні");
                return null;
            }

            InPostfixRecord = new List<string>();
            Stack<string> stack = new Stack<string>();

            string previous;
            for (int i = 0; i < infixRecord.Length; i++)
            {
                double num;
                bool isNum;

                //випадок коли в рівнянні одне невідоме
                if(infixRecord[i] == 'x') 
                {
                    isNum = true;
                }
                else 
                {
                    isNum = double.TryParse(Convert.ToString(infixRecord[i]), out num);
                }

                if (isNum)
                {
                    string number = Convert.ToString(infixRecord[i]);
                    while (true)
                    {
                        i++;

                        if (i >= infixRecord.Length)
                        {
                            i--;
                            break;
                        }

                        isNum = double.TryParse(Convert.ToString(infixRecord[i]), out num);

                        if (isNum == true | infixRecord[i] == ',')
                        {
                            number += infixRecord[i];
                        }
                        else
                        {
                            i--;
                            break;
                        }
                    }

                    //перевірка на те чи є число відємним
                    if (stack.Count > 0 && stack.Peek() == "-")
                    {
                        if (stack.Count == 1 && InPostfixRecord.Count == 0)
                        {
                            InPostfixRecord.Add($"-{number}");
                            stack.Pop();
                        }
                        else if (stack.Count != 1 | stack.Peek() == "(" | stack.Peek() == ";")
                        {
                            InPostfixRecord.Add($"-{number}");
                            stack.Pop();
                        }
                        else
                            InPostfixRecord.Add(number);
                    }
                    else
                        InPostfixRecord.Add(number);
                }
                else if (infixRecord[i] == '!')
                {
                    InPostfixRecord.Add(Convert.ToString('!'));
                    previous = "!";
                }
                else if (infixRecord[i] == 't' | infixRecord[i] == 'c' | infixRecord[i] == 's' | infixRecord[i] == 'l' | infixRecord[i] == 'e')
                {
                    stack.Push(Convert.ToString($"{infixRecord[i]}{infixRecord[i + 1]}{infixRecord[i + 2]}"));
                    previous = stack.Peek();
                    i += 2;
                }
                else if (infixRecord[i] == ';')
                {
                    //UNDONE: тут був while(true) до кінця if. НАшо його добавив непомню но здається він лишній тому видалив. Видалив тільки while і дужки решта не змінена

                    //TODO: виключення. Зробити повыдомлення
                    if (stack.Count == 0)
                        throw new NotImplementedException();

                    if (stack.Peek() == Convert.ToString('('))
                    {
                        //break;
                    }
                    else
                        InPostfixRecord.Add(stack.Pop());

                    previous = ";";
                }
                else if (infixRecord[i] == '(')
                {
                    stack.Push(Convert.ToString(infixRecord[i]));
                    previous = "(";
                }
                else if (infixRecord[i] == ')')
                {
                    while (true)
                    {
                        if (stack.Count == 0)
                            throw new NotImplementedException();

                        if (stack.Peek() == Convert.ToString('('))
                        {
                            stack.Pop();
                            break;
                        }
                        else
                            InPostfixRecord.Add(stack.Pop());
                    }
                }
                else if (infixRecord[i] == '+')
                {
                    if (stack.Count > 0 && (
                        stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" |         //префіксна функція
                        stack.Peek() == "log" | stack.Peek() == "exp" |
                        stack.Peek() == "+" |                                                                                   //такий же приорітет як і операція
                        stack.Peek() == "*" | stack.Peek() == "/" | stack.Peek() == "^"))                                       //вищій приорітет      
                        InPostfixRecord.Add(stack.Pop().ToString());

                    stack.Push(Convert.ToString(infixRecord[i]));
                }
                else if (infixRecord[i] == '-')
                {
                    if (stack.Count > 0 && (
                        stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" |         //префіксна функція
                        stack.Peek() == "log" | stack.Peek() == "exp" |
                        stack.Peek() == "-" |                                                                                   //такий же приорітет як і операція
                        stack.Peek() == "*" | stack.Peek() == "/" | stack.Peek() == "+" | stack.Peek() == "^"))                 //вищій приорітет      
                        InPostfixRecord.Add(stack.Pop().ToString());

                    stack.Push(Convert.ToString(infixRecord[i]));
                }
                else if (infixRecord[i] == '/' | infixRecord[i] == '*')
                {
                    if (stack.Count > 0 && (
                        stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" |         //префіксна функція
                        stack.Peek() == "log" | stack.Peek() == "exp" |
                        stack.Peek() == "*" | stack.Peek() == "/" |                                                             //такий же приорітет як і операція
                        stack.Peek() == "^"))                                                                                   //вищій приорітет
                        InPostfixRecord.Add(stack.Pop().ToString());

                    stack.Push(Convert.ToString(infixRecord[i]));
                }
                else if (infixRecord[i] == '^')
                {
                    if (stack.Count > 0 && (
                        stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" | stack.Peek() == "log" | stack.Peek() == "exp"))         //префіксна функція
                        InPostfixRecord.Add(stack.Pop().ToString());

                    stack.Push(Convert.ToString(infixRecord[i]));
                }

            }

            while (stack.Count > 0)
            {
                InPostfixRecord.Add(stack.Pop().ToString());
            }

            Console.WriteLine(String.Join(' ', InPostfixRecord));
            return InPostfixRecord;
        }


    }
}
