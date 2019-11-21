using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TravellingSalesmanProblem
{
    public class Population
    {
        private static readonly Random s_rnd = new Random();
        private Route[] _routes;

        public Route BestRoute { get; private set; }
        public int AllowedCores { get; set; }
        public double MutationRate { get; set; }

        public Population(int size, Point[] cities, double mutationRate)
        {
            _routes = InitializeRoutes(size, cities);
            MutationRate = mutationRate;
        }

        public void SetGeneration(int gen)
        {
            for (int i = 0; i < _routes.Length; i++)
            {
                _routes[i].Generation = gen;
            }
        }

        private Route[] InitializeRoutes(int size, Point[] cities)
        {
            Route[] routes = new Route[size];
            for (int i = 0; i < size; i++)
            {
                Point[] copiedCities = new Point[cities.Length];
                Array.Copy(cities, copiedCities, cities.Length);
                routes[i] = new Route(copiedCities); // Pass in a copy otherwise all routes reference the same array
            }

            return routes;
        }

        public void CalculateFitness()
        {
            double totalFitness = 0;
            double max = double.MaxValue;

            for (int i = 0; i < _routes.Length; i++)
            {
                Route route = _routes[i];
                route.Fitness = 1 / Math.Pow(route.TotalDistance, 4);
                totalFitness += route.Fitness;

                if (route.TotalDistance < max)
                {
                    max = route.TotalDistance;
                    BestRoute = route;
                }
            }

            // normalize
            for (int i = 0; i < _routes.Length; i++)
            {
                _routes[i].Fitness /= totalFitness;
            }
        }

        public void GoToNextGeneration()
        {
            Console.WriteLine("Current routes:");
            for (int i = 0; i < _routes.Length; i++)
            {
                Console.WriteLine(_routes[i]);
            }
            
            Route[] newRoutes = new Route[_routes.Length];

            // Good use of parallel class here (enough work being done)
            if (AllowedCores > 1 && AllowedCores <= Environment.ProcessorCount)
            {
                ParallelOptions po = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = AllowedCores,
                };

                try
                {
                    Parallel.For(
                        0, newRoutes.Length,            // from (incl), to (excl)
                        po,                             // options
                        () => new Random(s_rnd.Next()), // initialize thread-local variable with random seed
                        (i, _, rng) =>                  // method which uses the inumerator, the loop state and the thread-local variable
                    {
                        Route o1 = PickOneRoute(rng);
                        Route o2;

                        do
                        {
                            o2 = PickOneRoute(rng);
                        } while (o2 == o1);

                        Route newRoute = o1.Crossover(o2);
                        newRoute.Mutate(MutationRate);

                        newRoutes[i] = newRoute;

                        return rng;
                    },
                        rng => { } // Dispose the thread-local but random can't be disposed -> just no-op
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Couldn't go to next generation: {e.Message}");
                }
            }
            else
            {
                for (int i = 0; i < newRoutes.Length; i++)
                {
                    Route o1 = PickOneRoute(s_rnd);
                    Route o2;
                    do
                    {
                        o2 = PickOneRoute(s_rnd);
                    } while (o1 == o2);

                    Route newRoute = o1.Crossover(o2);
                    newRoute.Mutate(MutationRate);

                    newRoutes[i] = newRoute;
                }
            }

            _routes = newRoutes;
        }

        private Route PickOneRoute(Random rng)
        {
            int index = 0;
            double r;

            do
            {
                r = rng.NextDouble();
                // Console.WriteLine($"Random double in PickOneRoute: {r}");
            } while (r == 0);

            while (r > 0)
            {
                r -= _routes[index].Fitness;
                index++;
            }
            index--;
            Console.WriteLine($"Index used: {index}");

            return _routes[index];
        }
    }
}
