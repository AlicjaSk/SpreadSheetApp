using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Double;

namespace SpreadSheetApp
{
    class UserMenu
    {
        private const string EndOfSheet = ";";
        private const char InputSeparator = '|';
        private readonly SpreadSheet _spreadSheet;
    
        public UserMenu()
        {
            this._spreadSheet = new SpreadSheet();
        }

        private static string[] GetArguments(string line)
        {
            line = line.Remove(line.Length - 1);
            return line.Split(InputSeparator);
        }
        
        public void StartReading()
        {
            var line = "";
            while (!IsEndOfSpreadSheet(line))
            {
                line = Console.ReadLine();
                if (line == null) break;
                else if (line == "SHOW")
                    _spreadSheet.ShowSpreadSheet();
                else
                {
                    PassNewValuesToSpreadSheet(line);
                }
            }
        }

        private void PassNewValuesToSpreadSheet(string line)
        {
            try
            {
                _spreadSheet.AppendNewRow(GetArguments(line));
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool IsEndOfSpreadSheet(string line)
        {
            return line.EndsWith(EndOfSheet) ? true : false;
        }
    }
}
