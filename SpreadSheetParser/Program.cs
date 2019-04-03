using System;
using SpreadSheetParser.Parsers;

namespace SpreadSheetParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var sheetParser = new SheetParser("1FgbLOKoa1FuXnyiDLsweLoAtU60u3i0MlnMLJRCnE38");

            sheetParser.GetValues("B2:C");
        }
    }
}
