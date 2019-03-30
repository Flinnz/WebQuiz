using System;
using SpreadSheetParser.Parsers;

namespace SpreadSheetParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var sheetParser = new SheetParser();

            sheetParser.GetValues("B2:C");
        }
    }
}
