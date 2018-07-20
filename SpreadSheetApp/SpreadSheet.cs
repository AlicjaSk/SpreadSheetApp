using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Double;

namespace SpreadSheetApp
{
    class SpreadSheet
    {
        private int CurrentRow = 1;
        public Dictionary<string, double> Cells { get; set; }
        private readonly char[] _supportedOperators = Globals.SupportedOperators.Keys.ToArray();
        private readonly Calculator _calculator;
        public SpreadSheet()
        {
            this.Cells = new Dictionary<string, double>();
            _calculator = new Calculator(this);
        }


        private bool IsEquation(string line)
        {
            // implication p=>q in c# is !p | q
            const int numbersOfOperatorsIfNegativesValuesExist = 2;
            var numberOfOperators = _supportedOperators.Sum(o => line.Count(x => x == o));

            return _supportedOperators.Any(oper => line.Contains(oper)) 
                   && (!line.StartsWith("-") 
                       | numberOfOperators >= numbersOfOperatorsIfNegativesValuesExist);
        }

        private void ReplaceVariables(string equation)
        {
            var variables = equation.Split(_supportedOperators);
            double result;
            for (var i = 0; i < variables.Length; i++)
            {
                var v = variables[i];
                if (!Double.TryParse(v, out result))
                {
                    variables[i] = Cells[v].ToString();
                }
            }
//            equation = variables.Join
        }
        
        public void AppendNewRow(string[] arguments)
        {
            var currentColumn = 'A';
            foreach (var arg in arguments)
            {
                var key = currentColumn + CurrentRow.ToString();
                if (this.IsEquation(arg))
                {
                    ReplaceVariables(arg);
                    var result = _calculator.Calculate(arg);
                    this.Cells.Add(key, result);
                }
                else
                {
                    var number = Parse(arg);
                    this.Cells.Add(key, number);
                }

                currentColumn++;
            }

            CurrentRow++;
        }

        public void ShowSpreadSheet()
        {
            foreach (KeyValuePair<string, double> cell in Cells)
            {
                Console.WriteLine($"Key: {cell.Key} Value: {cell.Value}");
            }
        }
    }
}
