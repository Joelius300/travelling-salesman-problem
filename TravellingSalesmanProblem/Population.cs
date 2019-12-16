using System;
using System.Collections.Concurrent;
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

#if PARTITIONER_THLOCAL
                // https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-speed-up-small-loop-bodies
                OrderablePartitioner<Tuple<int, int>> rangePartitioner = Partitioner.Create(0, newRoutes.Length);
                Parallel.ForEach(
                    rangePartitioner,
                    _parallelOptions,
                    () => new Random(),
                    (range, loop, rng) =>
                    {
                        for (int i = range.Item1; i < range.Item2; i++)
                        {
                            newRoutes[i] = GetNewRoute(rng);
                        }

                        return rng;
                    },
                    rng => { } // no-op because null is not allowed
                );
#elif PARTITIONER
                // https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-speed-up-small-loop-bodies
                OrderablePartitioner<Tuple<int, int>> rangePartitioner = Partitioner.Create(0, newRoutes.Length);
                Parallel.ForEach(rangePartitioner, _parallelOptions, (range, loop) =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        newRoutes[i] = GetNewRoute(ThreadSafeRandom.ThreadSafeInstance);
                    }
                });
#elif FOR
                // This would also work perfectly fine but we don't need to use the ThreadSafeRandom class here because the Parallel
                // class has an elegant way of working with thread-local variables itself.
                Parallel.For(
                    0, newRoutes.Length,    // from, to
                    _parallelOptions,       // options
                    (i, loop) =>            // method which uses the index and the loop state (used to cancel loop)
                    {
                        newRoutes[i] = GetNewRoute(ThreadSafeRandom.ThreadSafeInstance);
                    }
                );
#else
                // Contradictory to what I said before, using the default constructor will NOT yield random instances with the same
                // "random" sequence ANYMORE (https://github.com/dotnet/runtime/blob/master/src/libraries/System.Private.CoreLib/src/System/Random.cs#L38)
                // Therefore, this is as probably the best way of doing it. It's also very very slightly faster according to my cheap testing.
                Parallel.For(
                    0, newRoutes.Length,    // from, to
                    _parallelOptions,       // options
                    () => new Random(),     // initialize thread-local variable
                    (i, loop, rng) =>       // method which uses the index, the loop state (used to cancel loop) and the thread-local Random
                    {
                        newRoutes[i] = GetNewRoute(rng);

                        return rng;
                    },
                    rng => { } // no-op because null is not allowed
                );
#endif
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
