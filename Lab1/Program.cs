using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PostfixRecord postfix = new PostfixRecord();

            while (true) 
            {
                postfix.ConvertToPostfixRecord(Console.ReadLine());
            }


        }

    }
}
