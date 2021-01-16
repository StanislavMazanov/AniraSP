using System;
using System.Diagnostics;

namespace AniraSP.Console {
    class Program {
        static void Main(string[] args) {
            new ScrappyService().Run();
            System.Console.WriteLine("Read any key");
            System.Console.ReadKey();
        }
    }
}