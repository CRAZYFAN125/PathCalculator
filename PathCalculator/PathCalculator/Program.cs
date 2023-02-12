using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PathCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Path Calculator";
            FancyConsoleMenu SelectMenu = new FancyConsoleMenu("Select what you want to do:", new string[]
            {
                "Find Route",
                //"Delate data",
                "Author",
                "Exit"
            });

            var x = SelectMenu.Run();
            switch (x)
            {
                case 0:
                    Code code = new Code();
                    code.Run();
                    break;
                /*case 1:
                    Console.WriteLine("Are you sure?");
                    Console.WriteLine("\"Enter\"-Yes \"Space\"-No");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Behaviour behaviour = new Behaviour();
                        behaviour.DeleteFolder("");
                    }
                    break;*/
                case 1:
                    Console.WriteLine("[ENG] Application was made in 2023 year by student from ZSTU - Alan");
                    Console.WriteLine("[PL] Aplikacja zostala stworzona w 2023 roku przez ucznia ZSTU - Alana");
                    Thread.Sleep(5000);
                    break;
            }

                    /*
                    List<string> list = new List<string>()
                    {
                        "1",
                        "2",
                        "3"
                    };
                    list.Add("4");

                    foreach (var item in list)
                    {
                        Console.WriteLine(item);
                    }
                    Console.ReadKey();
                    */
            }
    }
}
