using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    class Route
    {
        Random rnd = new Random();

        private DNA dna;
        public double totalDistance;
        public double fitness = 0;

        public int generation;

        public Route(Point[] cities) {
            dna = new DNA(cities, true);

            CalcTotalDistance();
        }

        //This constructor will only be used whenever there is a crossover happening. 
        //After a crossover there will always be a mutation and after the mutation there will always
        //be a calculation of the distance. So we can take away almost half the calculations by not doing it here
        public Route(DNA dna) {
            this.dna = dna;

            //CalcTotalDistance();
        }

        public void CalcTotalDistance()
        {
            double totDistance = 0;

            for (int i = 0; i < this.dna.route.Length - 1; i++)
            {
                totDistance += CalcDistance(dna.route[i], dna.route[i + 1]);
            }

            //Parallel.For(0, dna.route.Length - 1, (i, loop, subTotDistance) =>
            //{
            //    totDistance += CalcDistance(dna.route[i], dna.route[i + 1]);
            //});

            this.totalDistance = totDistance;
        }

        //This method is called a LOT so its as optimized as possible (resulting in ugly code)
        private double CalcDistance(Point p1, Point p2) {
            //Pytagoras: wurzel aus x^2 + y^2
            //p2.x - p1.x = delta x (same with y)
            return Math.Sqrt(((p2.X - p1.X) * (p2.X - p1.X)) + ((p2.Y - p1.Y) * (p2.Y - p1.Y)));
        }

        public Route Crossover(Route otherRoute) {
            return new Route(this.dna.Crossover(otherRoute.dna));
        }

        public void Mutate(int rate) {
            this.dna.Mutate(rate);

            CalcTotalDistance();
        }

        public Point[] GetPoints() {
            return this.dna.route;
        }
    }
}
