using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    class DNA
    {
        public Point[] route { get; }
        Random rnd = new Random();

        public DNA(Point[] cities, bool needsToBeShuffled)
        {
            if (needsToBeShuffled)
            {
                this.route = cities.OrderBy(x => rnd.Next()).ToArray();

                for (int i = 0; i < 5; i++)
                {
                    this.route = route.OrderBy(x => rnd.Next()).ToArray();
                }
            }
            else
            {
                this.route = cities;
            }
        }

        public DNA Crossover(DNA otherDNA)
        {
            return new DNA(Crossover(this.route, otherDNA.route), false);
        }

        private Point[] Crossover(Point[] points1, Point[] points2)
        {
            Point[] newRoute = new Point[points1.Length];
            int splitpoint = rnd.Next(points2.Length);

            for (int i = 0; i < newRoute.Length; i++)
            {
                if (i >= splitpoint)
                {
                    newRoute[i] = points1[i];
                }
                else
                {
                    newRoute[i] = points2[i];
                }
            }

            return newRoute;
        }

        public void Mutate(int rate)
        {
            for (int i = 0; i < route.Length/2; i++)
            {
                if (rnd.Next(100) < rate)
                {
                    int i1 = rnd.Next(route.Length);
                    int i2 = (i1 + 1) % route.Length;

                    Point temp = route[i2];
                    route[i2] = route[i1];
                    route[i1] = temp;
                }
            }
        }
    }
}
