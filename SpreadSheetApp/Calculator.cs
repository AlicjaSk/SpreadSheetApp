using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetApp
{
    class Calculator
    {
        private SpreadSheet _spreadSheet;
        private readonly ReversePolishNotation _reversePolishNotation; 
        public Calculator(SpreadSheet spreadSheet)
        {
            _spreadSheet = spreadSheet;
            _reversePolishNotation = new ReversePolishNotation(spreadSheet);
        }

        public double Calculate(string equation)
        {
            return _reversePolishNotation.CalculateEquation(equation);
        }
    }
}
