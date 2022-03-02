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

            string previous = "";
            bool unaryMinus = false;
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

                if(!isNum && unaryMinus) 
                {
                    Console.WriteLine("Помилка запису");
                    return new List<string>();
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

                    if (unaryMinus)
                    {
                        InPostfixRecord.Add($"-{number}");
                        previous = $"-{number}";
                    }
                    else
                    {
                        InPostfixRecord.Add(number);
                        previous = number;
                    }

                    unaryMinus = false;
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
                    //while потрібен щоб викинути з треку всі символи до дужки(ну або щось таке, точно не памятаю) 
                    while (true)
                    {
                        //TODO: виключення. Зробити повыдомлення
                        if (stack.Count == 0)
                            throw new NotImplementedException();

                        if (stack.Peek() == Convert.ToString('('))
                            break;
                        else
                            InPostfixRecord.Add(stack.Pop());
                    }

                    previous = ";";
                }
                else if (infixRecord[i] == '(')
                {
                    stack.Push(Convert.ToString(infixRecord[i]));
                    previous = "(";
                }
                else if (infixRecord[i] == ')')
                {
                    //така ж причина як і для ";"
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
                    previous= ")";
                }
                else if (infixRecord[i] == '+')
                {
                    if (stack.Count > 0 && (
                        stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" |         //префіксна функція
                        stack.Peek() == "log" | stack.Peek() == "exp" |
                        stack.Peek() == "+" | stack.Peek() == "-" |                                                                                   //такий же приорітет як і операція
                        stack.Peek() == "*" | stack.Peek() == "/" | stack.Peek() == "^"))                                       //вищій приорітет      
                        InPostfixRecord.Add(stack.Pop().ToString());

                    stack.Push(Convert.ToString(infixRecord[i]));
                    previous = stack.Peek();
                }
                else if (infixRecord[i] == '-')
                {
                    if (previous == "(" | previous == ";" | previous == "")  //значить це унарний мінус і за ним мусить бути число
                    {
                        unaryMinus = true;
                        previous = "-";
                    }
                    else if (previous == "cos" | previous == "sin" | previous == "tan" | previous == "ctg" | previous == "log" | previous == "exp")
                    {
                        Console.WriteLine("Ппомилка запису");
                        return new List<string>();
                    }
                    else
                    {
                        if (stack.Count > 0 && (
                            stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" |         //префіксна функція
                            stack.Peek() == "log" | stack.Peek() == "exp" |
                            stack.Peek() == "-" |                                                                                   //такий же приорітет як і операція
                            stack.Peek() == "*" | stack.Peek() == "/" | stack.Peek() == "+" | stack.Peek() == "^"))                 //вищій приорітет      
                            InPostfixRecord.Add(stack.Pop().ToString());

                        stack.Push(Convert.ToString(infixRecord[i]));
                        previous = "-";
                    }
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
                    previous = stack.Peek();
                }
                else if (infixRecord[i] == '^')
                {
                    if (stack.Count > 0 && (
                        stack.Peek() == "cos" | stack.Peek() == "sin" | stack.Peek() == "tan" | stack.Peek() == "ctg" | stack.Peek() == "log" | stack.Peek() == "exp"))         //префіксна функція
                        InPostfixRecord.Add(stack.Pop().ToString());

                    stack.Push(Convert.ToString(infixRecord[i]));
                    previous = stack.Peek();
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
