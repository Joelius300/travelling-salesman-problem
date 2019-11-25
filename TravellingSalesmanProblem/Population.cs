using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TravellingSalesmanProblem
{
    public class Population
    {
        private Route[] _currentRoutes;

        public IReadOnlyList<Point> Cities { get; }
        public int Size { get; set; }
        public double MutationRate { get; set; }

        public Population(IReadOnlyList<Point> cities, int size)
        {
            Size = size;
            Cities = cities ?? throw new ArgumentNullException(nameof(cities));
            _currentRoutes = GetRandomRoutes(Size);
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

            for (int i = 0; i < newRoutes.Length; i++)
            {
                Route o1 = ThreadSafeRandom.ThreadSafeInstance.PickRouteAccordingToFitness(_currentRoutes);
                Route o2;
                do
                {
                    o2 = ThreadSafeRandom.ThreadSafeInstance.PickRouteAccordingToFitness(_currentRoutes);
                } while (o1 == o2);

                Route newRoute = o1.Crossover(o2);
                newRoute.Mutate(MutationRate);

                newRoutes[i] = newRoute;
            }

            _currentRoutes = newRoutes;
        }
    }
}
