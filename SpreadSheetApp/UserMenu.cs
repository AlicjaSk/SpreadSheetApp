using System;
using SpreadSheetApp.Validators;


namespace SpreadSheetApp
{
    class UserMenu
    {
        private readonly SpreadSheet _spreadSheet;
    
        public UserMenu()
        {
            this._spreadSheet = new SpreadSheet();
        }

        private static string[] GetArguments(string line)
        {
            line = line.Remove(line.Length - 1);
            return line.Split(Globals.InputSeparator);
        }

        private void ValidateUserInput(string line)
        {
            var validator = new Validator();
            validator.Validate(line);
        }

        private static void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome in SpreadSheet Application!");
            Console.WriteLine("Requiremenets for input: \n" +
                              "1. Numbers should be separated by '|'.\n" +
                              "2. If you want to finish providing numbers add ';' at the end of line.\n" +
                              "You can enter 'SHOW' to show the spreadsheet.\n" +
                              "Please start providing your spreadsheet: ");
        }

        public void StartReading()
        {
            ShowWelcomeMessage();
            var line = "";
            while (!IsEndOfSpreadSheet(line))
            {
                line = Console.ReadLine();
                if (line == "SHOW")
                    _spreadSheet.ShowSpreadSheet();
                else
                {
                    PassNewValuesToSpreadSheet(line);
                }
            }

            DoCalculationsBasedOnProvidedSpreadSheet();
        }

        private void DoCalculationsBasedOnProvidedSpreadSheet()
        {
            Console.WriteLine("Now you can provide equations.\n" +
                              "! Please note that results won't be remebered in the spreadsheet!\n" +
                              "You can enter 'SHOW' to show the spreadsheet.\n" +
                              "You can enter 'EXIT' to close the program.");
            var line = "";
            while (true)
            {
                line = Console.ReadLine();
                if (line == "SHOW")
                    _spreadSheet.ShowSpreadSheet();
                else if (line == "EXIT") { 
                    Console.WriteLine("See you! Press enter to close.");
                    break;
                }
                else
                    ExpressionCalculation(line);
            }
        }

        private void ExpressionCalculation(string line)
        {
            try
            {
                var eq = new Equation(line, _spreadSheet);
                var reversePolishNotation = new ReversePolishNotation(_spreadSheet);
                var result = reversePolishNotation.CalculateEquation(eq);
                Console.WriteLine($"\tResult of {line}={result}");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PassNewValuesToSpreadSheet(string line)
        {
            try
            {
                ValidateUserInput(line);
                _spreadSheet.AppendNewRow(GetArguments(line));
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool IsEndOfSpreadSheet(string line)
        {
            return line.EndsWith(Globals.EndOfSheet) ? true : false;
        }
    }
}
