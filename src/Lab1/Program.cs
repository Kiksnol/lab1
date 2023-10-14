using System;
using System.Collections.Generic;
using ship_parts;

namespace lab1_y26_oop
{
    public struct part_of_route
    {
        float dist;
        environment_type env_type;
        space_objects.Object obj;

        public void SetDist(float new_dist)
        {
            dist = new_dist;
        }
        public float GetDist()
        {
            return dist;
        }

        public void SetEnvType(environment_type new_env_type)
        {
            env_type = new_env_type;
        }

        public environment_type GetEnvType()
        {
            return env_type;
        }

        public void SetObj(space_objects.Object Obj) {
            obj = Obj;
        }

        public space_objects.Object GetObj() {
            return obj; 
        }

        
    };

    internal class Program
    {
        static void Main(string[] args) {
            List<part_of_route> Route = new List<part_of_route>();

            part_of_route POR = new part_of_route();

            POR.SetDist(10);
            POR.SetEnvType(environment_type.nitrin_parts);
            POR.SetObj(new space_objects.whale());

            Route.Add(POR);

            POR.SetDist(10);
            POR.SetEnvType(environment_type.nitrin_parts);
            POR.SetObj(new space_objects.whale());

            Route.Add(POR);

            List<spaceship> Ships = new List<spaceship>();

            Ships.Add(new spaceship(ship_type.shuttle, false));
            Ships.Add(new spaceship(ship_type.vaklas, false));
            Ships.Add(new spaceship(ship_type.meredian, true));
            Ships.Add(new spaceship(ship_type.avgur, false));
            Ships.Add(new spaceship(ship_type.stella, true));

            int i = 0;
            ship_type best = ship_type.shuttle;
            double min_cost = 120312941094103;
            bool is_chosen_best = false;
            foreach (var val in Ships) {
                if (val.Run(Route)) {
                    switch (val.GetShipType())
                    {
                        case ship_type.stella:
                            Console.Write("stella ");
                            break;
                        case ship_type.avgur:
                            Console.Write("avgur ");
                            break;
                        case ship_type.vaklas:
                            Console.Write("vaklas ");
                            break;
                        case ship_type.shuttle:
                            Console.Write("shuttle ");
                            break;
                        case ship_type.meredian:
                            Console.Write("meredian ");
                            break;
                    }
                    Console.Write("finished succesfully, cost: {0}\n", val.total_flight_cost);
                    if (val.total_flight_cost < min_cost) { 
                        min_cost = val.total_flight_cost;
                        best = val.GetShipType();
                        is_chosen_best = true;
                    }
                } 
                else
                {
                    switch (val.GetShipType())
                    {
                        case ship_type.stella:
                            Console.Write("stella ");
                            break;
                        case ship_type.avgur:
                            Console.Write("avgur ");
                            break;
                        case ship_type.vaklas:
                            Console.Write("vaklas ");
                            break;
                        case ship_type.shuttle:
                            Console.Write("shuttle ");
                            break;
                        case ship_type.meredian:
                            Console.Write("meredian ");
                            break;
                    }
                    Console.Write("failed\n");
                }
                i++;
            }

            if (is_chosen_best) {
                switch (best)
                {
                    case ship_type.stella:
                        Console.Write("stella ");
                        break;
                    case ship_type.avgur:
                        Console.Write("avgur ");
                        break;
                    case ship_type.vaklas:
                        Console.Write("vaklas ");
                        break;
                    case ship_type.shuttle:
                        Console.Write("shuttle ");
                        break;
                    case ship_type.meredian:
                        Console.Write("meredian ");
                        break;
                }
                Console.Write("is the best variant\n");
            }
        }
    }
}
