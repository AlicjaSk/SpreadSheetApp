using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetApp
{
    class Equation
    {
        private readonly SpreadSheet _spreadSheet;
        public string EquationStr { get; }
        private readonly Dictionary<char, int> _supportedOperators = Globals.SupportedOperators;
        public char[] Operators { get; set; }
        public List<string> Variables { get; set; }

        public Equation(string equation, SpreadSheet spreadSheet)
        {
            _spreadSheet = spreadSheet;
            EquationStr = equation;
            SetOperators();
            SetVariables();
        }

        private string GetValueFromSpreadSheet(string key)
        {
            try { 
                return _spreadSheet.Cells[key].ToString();
            }
            catch(KeyNotFoundException)
            {
                throw new ValidationException($"Key {key} doesn't have a value yet!");
            }
        }

        private void ReplaceVariablesWithValues()
        {
            for (var i = 0; i < Variables.Count; i++)
            {
                var v = Variables[i];
                if (!double.TryParse(v, out _))
                    Variables[i] = GetValueFromSpreadSheet(Variables[i]);
            }
        }

        private void AddZeroAtTheBeginning()
        {
            // To simplify calculations change equation from e.g. -3-5 = 0-3-5
            Variables.RemoveAt(0);
            Variables.Insert(0, "0");
        }

        private void SetOperators()
        {
            var operators = "";
            foreach (var o in EquationStr)
            {
                if (_supportedOperators.Keys.Any(x => x == o))
                    operators += o;
            }

            var operArray = operators.ToCharArray();
            Operators = operArray;
        }

        private void SetVariables()
        {
            
            var variables = EquationStr.Split(_supportedOperators.Keys.ToArray());
            Variables = new List<string>(variables);
            if (EquationStr.StartsWith("-"))
                AddZeroAtTheBeginning();
            ReplaceVariablesWithValues();

        }
    }
}
