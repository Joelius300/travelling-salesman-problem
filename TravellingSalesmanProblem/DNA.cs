using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class DNA : IEquatable<DNA>
    {
        public Point[] Route { get; }
        private static readonly Random s_rnd = new Random();

        public DNA(Point[] cities)
        {
            Route = cities;
        }

        private Point[] Crossover(Point[] points1, Point[] points2)
        {
            Point[] newRoute = new Point[points1.Length];
            int splitpoint = s_rnd.Next(points1.Length);

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

                // Here it might take a route that was already put in before
                // I have absolutely no idea how that didn't happen before. Maybe because of the weird do while != which i changed to ==
            }

            return newRoute;
        }

        public DNA Crossover(DNA otherDNA) => 
            new DNA(Crossover(Route, otherDNA.Route));

        public void Mutate(double rate)
        {
            for (int i = 0; i < Route.Length/2; i++)
            {
                if (s_rnd.NextDouble() < rate)
                {
                    int i1 = s_rnd.Next(Route.Length);
                    int i2 = (i1 + 1) % Route.Length;

                    Point temp = Route[i2];
                    Route[i2] = Route[i1];
                    Route[i1] = temp;
                }
            }
        }

        public static bool operator ==(DNA a, DNA b)
        {
            if (a is null)
            {
                return b is null;
            }

            return a.Equals(b);
        }

        public static bool operator !=(DNA a, DNA b) => !(a == b);

        public bool Equals([AllowNull] DNA other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(other, this))
                return true;

            return Route == other.Route || Enumerable.SequenceEqual(Route, other.Route);
        }

        public override bool Equals(object obj)
        {
            if (obj is DNA dna && obj != null)
                return Equals(dna);

            return base.Equals(obj);
        }

        public override int GetHashCode() => Route.GetHashCode();

        public override string ToString() => $"DNA: {string.Join(", ", Route)}";
    }
}
