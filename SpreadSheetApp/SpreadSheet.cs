using System;
using System.Collections.Generic;
using System.Linq;
using SpreadSheetApp.Validators;
using static System.Double;


namespace SpreadSheetApp
{
    class SpreadSheet
    {
        private int _currentRow = 1;
        public Dictionary<string, double> Cells { get; set; }
        private readonly char[] _supportedOperators = Globals.SupportedOperators.Keys.ToArray();

        public SpreadSheet()
        {
            this.Cells = new Dictionary<string, double>();
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

        private void AppendNumber(string key, string arg)
        {
            try
            {
                var number = Parse(arg);
                this.Cells.Add(key, number);
            }
            catch (FormatException)
            {
                throw new ValidationException($"Argument : {arg} is not a number!");
            }
        }

        private void AppendEquation(string key, string arg)
        {
            var eq = new Equation(arg, this);
            var reversePolishNotation = new ReversePolishNotation(this);
            var result = reversePolishNotation.CalculateEquation(eq);
            this.Cells.Add(key, result);
        }
        
        public void AppendNewRow(string[] arguments)
        {
            var currentColumn = 'A';
            foreach (var arg in arguments)
            {
                var key = currentColumn + _currentRow.ToString();
                if (this.IsEquation(arg))
                    AppendEquation(key, arg);
                else
                    AppendNumber(key, arg);

                currentColumn++;
            }

            _currentRow++;
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
