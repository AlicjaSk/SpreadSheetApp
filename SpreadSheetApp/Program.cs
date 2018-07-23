using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var userMenu = new UserMenu();
            userMenu.StartReading();
            Console.ReadKey();
        }
    }
}
