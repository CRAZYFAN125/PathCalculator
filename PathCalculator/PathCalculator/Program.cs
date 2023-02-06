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
            Code code = new Code();
            code.Run();
            Console.ReadKey();
        }


        class Code
        {
            class Points
            {
                public class Connection
                {
                    public string connection;
                    public float distance;
                }
                public string pointName;
                public Connection[] connections;
            }

            Points[] mainPoints = new Points[] {
                // Punkt 1 - A1
                new Points() {
                    pointName = "A1",
                    connections=new Points.Connection[] {
                        new Points.Connection()
                        {
                            connection = "A2",
                            distance=15
                        },
                        new Points.Connection()
                        {
                            connection="CD",
                            distance=6
                        }
                    }
                },
                // Punkt 2 - A2
                new Points()
                {
                    pointName="A2",
                    connections = new Points.Connection[]
                    {
                        new Points.Connection()
                        {
                            connection = "A1",
                            distance = 15
                        },
                        new Points.Connection()
                        {
                            connection = "CD",
                            distance=5
                        }
                    }
                },
                // Punkt 3 - Baza
                new Points()
                {
                    pointName="CD",
                    connections = new Points.Connection[]
                    {
                        new Points.Connection()
                        {
                            connection = "A1",
                            distance = 6
                        },
                        new Points.Connection()
                        {
                            connection = "A2",
                            distance=5
                        }
                    }
                }
            };


            public void Run()
            {
                Thread.CurrentThread.Name = "Main";
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < getCombos(mainPoints.Length); i++)
                {
                    tasks.Add(new Task(()=>FindRoute(mainPoints, i)));
                    tasks[i].Start();
                }
            }
            public int getCombos(int x) { return (x * x) - x; }

            void FindRoute(Points[] points, int i)
            {

            }
        }
    }
}
