using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Equations : Expression
    {
        private string equation;
        private List<string> equationPostfixRecord;

        public string Equation 
        { 
            get => equation; 
            set 
            { 
                if(value != null && !String.IsNullOrWhiteSpace(value)) 
                    equation = value;     
            } }

        public List<string> EquationPostfixRecord 
        {
            get => equationPostfixRecord;
            set 
            {
                if(value != null) 
                    equationPostfixRecord = value;
            } }

        public Equations() { }

        public Equations(string equation)
        {
            Equation = equation;
        }

        public void AddEquation(string str) 
        {
            Equation = str;
        }


        /// <summary>
        /// Знаходить занчення функції при заданному х
        /// </summary>
        /// <param name="x"></param>
        public void FindTheValueOfTheFunction(int x) 
        {
            if(String.IsNullOrEmpty(Equation) | String.IsNullOrWhiteSpace(Equation)) 
            {
                Console.WriteLine("Запишіть рівняння");
                return;
            }

            EquationPostfixRecord = ConvertToPostfixRecord(Equation);

            for(int i = 0; i < InPostfixRecord.Count; i++) 
            {
                if (InPostfixRecord[i] == "x")
                    InPostfixRecord[i] = $"{x}";
                else if (InPostfixRecord[i] == "-x")
                    InPostfixRecord[i] = $"-{x}";
            }
            
            CalculateExpression(Equation);
        }

        
    }
}
