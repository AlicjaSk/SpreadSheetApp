using System;


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
