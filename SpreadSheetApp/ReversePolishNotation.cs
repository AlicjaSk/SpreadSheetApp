using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetApp
{
    class ReversePolishNotation
    {
        private readonly Stack stack = new Stack();

        private readonly Dictionary<char, int> _supportedOperators = Globals.SupportedOperators;
        private readonly ArrayList _onpArguments;
        private SpreadSheet _spreadSh;

        public ReversePolishNotation(SpreadSheet spreadSh)
        {
            this._spreadSh = spreadSh;
            _onpArguments = new ArrayList();
        }

        private void AppendNewArgumentToNotation(object newArgument)
        {
            _onpArguments.Add(newArgument);
        }

        private void CheckPrioritiesOnStack(char newOper)
        {
            if (stack.Count != 0)
            {
                var top = (char) stack.Peek();
                if (_supportedOperators[top] >= _supportedOperators[newOper])
                {
                    AppendNewArgumentToNotation(stack.Pop());
                }
            }
            stack.Push(newOper);
        }

        private void CreateNotation(List<string> variables, char[] operators)
        {
            for (var i = 0; i < variables.Count; i++)
            {
                AppendNewArgumentToNotation(variables[i]);
                if(i<operators.Length)
                    CheckPrioritiesOnStack(operators[i]);
            }
        }

        public void ConvertToReversePolishNotation(Equation equation)
        {
            var variables = equation.Variables;
            var operators = equation.Operators;

            CreateNotation(variables, operators);
            
            while (stack.Count != 0)
                AppendNewArgumentToNotation(stack.Pop());
        }

        private static bool IsVariable(object arg)
        {
            try
            { 
                var variable = double.Parse(arg.ToString());
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        private double CalculateSingleExpression(double a, double b, char oper)
        {
            double result = 0;
            switch (oper)
            {
                case '-':
                    result = b - a;
                    break;
                case '+':
                    result = b + a;
                    break;
                case '*':
                    result = b * a;
                    break;
                case '/':
                    result = b / a;
                    break;
            }

            return result;
        }

        public double CalculateEquation(Equation equation)
        {
            ConvertToReversePolishNotation(equation);
            var computeStack = new Stack();
            foreach (var arg in _onpArguments)
            {
                if (IsVariable(arg))
                    computeStack.Push(arg);
                else
                {
                    var a = computeStack.Pop();
                    var b = computeStack.Pop();
                    var result = CalculateSingleExpression(double.Parse(a.ToString()), double.Parse(b.ToString()), (char)arg);
                    computeStack.Push(result);
                }
            }
            return double.Parse(computeStack.Pop().ToString()); 
        }
    }
}
