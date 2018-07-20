using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
