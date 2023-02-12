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
            Code code = new Code();
            code.Run();

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
