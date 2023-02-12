using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PathCalculator
{
    public class Code : Behaviour
    {
        /// <summary>
        /// Types of posibles points
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Distribution center
            /// </summary>
            cd,
            /// <summary>
            /// Point where it will be unloaded
            /// </summary>
            point,
            /// <summary>
            /// Point where it can be filled up, it's not a cd!
            /// </summary>
            resuply
        }

        class Points
        {
            /// <summary>
            /// Class that 
            /// </summary>
            public class Connection
            {
                /// <summary>
                /// Connection name
                /// </summary>
                public string connection;
                /// <summary>
                /// Distance to this connection
                /// </summary>
                public float distance;
            }
            /// <summary>
            /// Name of this point
            /// </summary>
            public string pointName;
            /// <summary>
            /// Where it can be traveled to from it
            /// </summary>
            public Connection[] connections;
            /// <summary>
            /// Type of point
            /// </summary>
            public Type pointType = Type.point;
        }

        /// <summary>
        /// This is class for an route data
        /// </summary>
        class Map
        {
            /// <summary>
            /// List of points in route
            /// </summary>
            public List<Points> points = new List<Points>();
            /// <summary>
            /// Distance to travel
            /// </summary>
            public float roadDistance;
            /// <summary>
            /// price to pay for it
            /// </summary>
            public float cost = 0;

        }

        /// <summary>
        /// List of points (example)
        /// </summary>
        List<Points> mainPoints = new List<Points> {
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
                        connection = "A3",
                        distance = 5
                    },
                    new Points.Connection()
                    {
                        connection="CD",
                        distance=6
                    }
                },
                pointType=Type.point
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
                        connection = "A3",
                        distance=10
                    },
                    new Points.Connection()
                    {
                        connection = "CD",
                        distance=5
                    }
                },
                pointType=Type.point
            },
            new Points()
            {
                pointName="A3",
                connections = new Points.Connection[]
                {
                    new Points.Connection()
                    {
                        connection = "A1",
                        distance = 5
                    },
                    new Points.Connection()
                    {
                        connection = "A2",
                        distance=10
                    }
                },
                pointType=Type.point
            },
            // Punkt 3 - Base
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
                },
                pointType=Type.cd
            }
        };

        float costs = 0;

        /// <summary>
        /// Runs searching of routes
        /// </summary>
        public void Run()
        {
            Thread.CurrentThread.Name = "Main";

            print("Please write cost per one unit drived, 0 if none:", ConsoleColor.Cyan);
            costs = float.Parse(read());
            Console.CursorVisible = false;

            print("Searcher tasks - are now active", ConsoleColor.Blue);

            Task<List<Map>>[] tasks = new Task<List<Map>>[]
            {
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),1),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),2),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),3),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),4),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),5),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),6),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),7),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),8),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),9),TaskCreationOptions.LongRunning),
                Task<List<Map>>.Factory.StartNew(() => FindRoute(mainPoints.ToArray(),10),TaskCreationOptions.LongRunning)
            };

            //for (int i = 0; i < tasks.Length-1; i++)
            //{
            //    print($"Searcher task{i} - is now active", ConsoleColor.Blue);
            //    Thread.Sleep(3000);
            //    tasks[i].Wait(TimeSpan.FromSeconds(10));
            //}



            List<Map> maps = new List<Map>();
            Map theBestMap = new Map() { roadDistance = float.MaxValue };
            string roads = string.Empty;


            foreach (var item in tasks)
            {
                if (item.Result != null)
                {
                    maps.AddRange(item.Result);
                }
            }

            for (int i = 0; i < maps.Count - 1; i++)
            {
                var item = maps[i];
                if (item.roadDistance < theBestMap.roadDistance)
                {
                    theBestMap = item;
                }

                roads += $"-> Map{i}: ";
                for (int c = 0; c < item.points.Count; c++)
                {
                    var point = item.points[c];
                    if (c != item.points.Count - 1)
                    {
                        roads += $"{point.pointName}-";
                    }
                    else
                    {
                        roads += $"{point.pointName}\tDistance {item.roadDistance}\n";
                    }
                }

                print($"{roads}");
                Thread.Sleep(500);
            }
            Thread.Sleep(5000);
            clear();
            print($"The best map option have distance {theBestMap.roadDistance} units, and costs {theBestMap.cost}");

            string xxx = "";
            for (int i = 0; i < theBestMap.points.Count; i++)
            {
                var point = theBestMap.points[i];
                if (i != theBestMap.points.Count - 1)
                {
                    xxx += $"{point.pointName}-";
                }
                else
                {
                    xxx += $"{point.pointName}\n";
                }
            }
            print(xxx, ConsoleColor.Green);

            key:
            print("\nClick \"Enter\" to save routes or \"Space\" to end program");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                QuickSave(maps, "Calculated_Maps");
                Save(roads, "roads.txt");
                Save(theBestMap, "TheBestRoute.json", "TheBestMap");
                Save(xxx, "TheBestRoute.txt", "TheBestMap");
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                return;
            }
            else
            {
                goto key;
            }
        }

        /// <summary>
        /// Gets maximum combinations for routes
        /// </summary>
        /// <param name="x">Number of things to find combinations</param>
        /// <returns>Amount of posible combination</returns>
        public int GetCombos(int x) => (x * x) - x;
        /// <summary>
        /// Gets maximum combinations for routes
        /// </summary>
        /// <param name="n">Number of all things</param>
        /// <param name="k">Number of things to find combinations</param>
        /// <returns>Amount of posible combination</returns>
        public int GetCombos(int n, int k) => Silnia(n) / (Silnia(k) * Silnia(n - k));

        List<Map> FindRoute(Points[] points, int i)
        {
            List<Map> maps = new List<Map>();

            Points cd = null;

            foreach (var item in points)
            {
                if (item.pointType == Type.cd)
                {
                    cd = item;
                }
            }


            for (int x = 0; x < 10; x++)
            {
                int errorAmount = 0;
            //    print("x");
            errorRecorveryPoint:
                List<Points> pointsMade = new List<Points>();
                int pointsAmount = 0;
                maps.Add(new Map());
                int mapIndex = maps.Count - 1;
                float distanceMade = 0;

                maps[mapIndex].points.Add(cd);
                Points lastPoint = cd;
                pointsMade.Add(cd);

                while (pointsAmount < points.Length)
                {
                    int random = new Random().Next(points.Length);
                    Points point = points[random];

                    if (point != lastPoint)
                    {
                        foreach (var item in point.connections)
                        {
                            if (item.connection == lastPoint.pointName)
                            {
                                lastPoint = point;
                                pointsMade.Add(point);
                                distanceMade += item.distance;
                                pointsAmount++;
                            }
                        }
                    }
                }

                foreach (var item in pointsMade[pointsMade.Count - 1].connections)
                {
                    if (item.connection == cd.pointName)
                    {
                        maps[mapIndex].points.Add(cd);
                        lastPoint = cd;
                        pointsMade.Add(cd);
                        distanceMade += item.distance;
                    }
                    //else
                    //{
                    //    goto errorRecorveryPoint;
                    //}
                }

                if (!IsAllMached(points, pointsMade.ToArray()))
                {
                    print("[!] Not every point has been mapped! Reseting map...", ConsoleColor.Magenta);
                    Console.Beep();
                    errorAmount++;
                    maps.RemoveAt(mapIndex);
                    if(errorAmount<10)
                        goto errorRecorveryPoint;

                    print("Can't find any route, maybe something went wrong?", ConsoleColor.Red);
                    Console.Beep(5000,500);
                    break;
                }

                if (pointsMade[pointsMade.Count-1]!=cd)
                {
                    print($"[!] Last point isn't {cd.pointName}! Reseting map...", ConsoleColor.Magenta);
                    Console.Beep();
                    errorAmount++;
                    maps.RemoveAt(mapIndex);
                    if (errorAmount < 10)
                        goto errorRecorveryPoint;

                    print("Can't find any route, maybe something went wrong?", ConsoleColor.Red);
                    Console.Beep(5000, 500);
                    break;
                }

                maps[mapIndex].points = pointsMade;
                maps[mapIndex].roadDistance = distanceMade;
                maps[mapIndex].cost = distanceMade * costs;
            }
            print($"Task{i} ended succesfull", ConsoleColor.Green);
            Console.CursorVisible= true;
            return maps;
        }

        bool IsAllMached(Points[] startPoints, Points[] endPoints)
        {
            foreach (var start in startPoints)
            {
                bool isInThePoints = false;
                foreach (var end in endPoints)
                {
                    if (start == end)
                    {
                        isInThePoints = true;
                    }
                }
                if (!isInThePoints)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
