using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class Population
    {
        private Route[] _currentRoutes;
        private readonly ParallelOptions _parallelOptions;

        public IReadOnlyList<PointF> Cities { get; }
        public int Size { get; set; }
        public double MutationRate { get; set; }
        public int AmountCores { get; set; }
        public Route Best { get; private set; }
        public int Generation { get; private set; }

        public Population(IReadOnlyList<PointF> cities, int size, double mutationRate)
        {
            Size = size;
            MutationRate = mutationRate;
            Cities = cities ?? throw new ArgumentNullException(nameof(cities));
            _currentRoutes = GetRandomRoutes(Size);
            Best = GetBest(_currentRoutes);
            NormalizeFitness(_currentRoutes); // for first run
            _parallelOptions = new ParallelOptions();
        }

        private Route[] GetRandomRoutes(int size)
        {
            Route[] routes = new Route[size];
            for (int i = 0; i < routes.Length; i++)
            {
                IList<int> order = ThreadSafeRandom.ThreadSafeInstance.RandomRange(0, Cities.Count).ToList();
                routes[i] = new Route(Cities, order);
            }
            return routes;
        }

        public void NextGeneration()
        {
            Route[] newRoutes = new Route[Size];

            if (AmountCores > 1 && AmountCores <= Environment.ProcessorCount)
            {
                _parallelOptions.MaxDegreeOfParallelism = AmountCores;

                Parallel.For(
                    0, newRoutes.Length,    // from, to
                    _parallelOptions,       // options
                    (i, loop) =>            // method which uses the index and the loop state (used to cancel loop)
                    {
                        newRoutes[i] = GetNewRoute(ThreadSafeRandom.ThreadSafeInstance);
                    }
                );

                // This also works but I assume it's a bit less random. The thread-local Random which is used here will be created using the default
                // constructor which, if called multiple times in a small timeframe (like here), might produce the same "random"-sequences. However if you use
                // the ThreadSafeRandom class, the thread-local (or thread-static) instance will be created using a random seed from a global (non-thread-static)
                // Random instance. The lock which is used for aquiring a random seed for a new thread-local Random instance probably makes the current solution
                // slightly less performant but not by much (only very rough and sloppy testing was done).
                // Parallel.For(
                //     0, newRoutes.Length,    // from, to
                //     _parallelOptions,       // options
                //     () => new Random(),     // initialize thread-local variable
                //     (i, loop, rng) =>       // method which uses the index, the loop state (used to cancel loop) and the thread-local Random
                //     {
                //         newRoutes[i] = GetNewRoute(rng);

                //         return rng;
                //     },
                //     rng => { } // no-op because null is not allowed
                // );
            }
            else
            {
                for (int i = 0; i < newRoutes.Length; i++)
                {
                    newRoutes[i] = GetNewRoute(ThreadSafeRandom.ThreadSafeInstance);
                }
            }

            NormalizeFitness(newRoutes); // for next run
            Best = GetBest(newRoutes);
            _currentRoutes = newRoutes;
            Generation++;

            Route GetNewRoute(Random random)
            {
                Route o1 = random.PickRouteAccordingToFitness(_currentRoutes);
                Route o2;
                do
                {
                    o2 = random.PickRouteAccordingToFitness(_currentRoutes);
                } while (o1 == o2);

                Route newRoute = o1.Crossover(o2);
                newRoute.Mutate(MutationRate);

                return newRoute;
            }
        }

        private static void NormalizeFitness(Route[] routes)
        {
            double total = 0;
            for (int i = 0; i < routes.Length; i++)
            {
                total += routes[i].Fitness;
            }

            for (int i = 0; i < routes.Length; i++)
            {
                routes[i].Fitness /= total;
            }
        }

        private static Route GetBest(IEnumerable<Route> routes)
        {
            double bestDistance = double.MaxValue;
            Route? best = null;
            foreach (Route route in routes)
            {
                if (route.TotalDistance < bestDistance)
                {
                    bestDistance = route.TotalDistance;
                    best = route;
                }
            }

            return best!; // ignore this warning, it won't be null
        }
    }
}
