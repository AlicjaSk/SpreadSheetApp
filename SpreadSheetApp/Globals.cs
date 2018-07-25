using System;
using System.Collections.Generic;


namespace SpreadSheetApp
{
    public static class Globals
    {
        public static Dictionary<char, int> SupportedOperators = new Dictionary<char, int>
        {
            {'-', 1},
            {'+', 1},
            {'*', 2},
            {'/', 2}
        };

        public const string EndOfSheet = ";";
        public const char InputSeparator = '|';
    }
}
