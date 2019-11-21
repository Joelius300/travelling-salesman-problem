using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class Route : IEquatable<Route>
    {
        private static readonly Random s_rnd = new Random();
        private readonly DNA _dna;

        public double TotalDistance { get; private set; }
        public double Fitness { get; internal set; }
        public int Generation { get; internal set; }
        public Point[] Points => _dna.Route;

        public Route(Point[] cities)
        {
            s_rnd.Shuffle(cities);
            _dna = new DNA(cities);

            TotalDistance = CalcTotalDistance();
        }

        // This constructor will only be used whenever there is a crossover happening. 
        // After a crossover there will always be a mutation and after the mutation there will always
        // be a calculation of the distance. So we can take away almost half the calculations by not doing it here
        private Route(DNA dna)
        {
            _dna = dna;
        }

        private double CalcTotalDistance()
        {
            double totalDistance = 0;

            for (int i = 0; i < _dna.Route.Length - 1; i++)
            {
                totalDistance += CalcDistance(_dna.Route[i], _dna.Route[i + 1]);
            }

            return totalDistance;
        }

        //This method is called a LOT so its as optimized as possible (resulting in ugly code)
        private double CalcDistance(Point p1, Point p2) =>
            // Pythagoras
            Math.Sqrt(((p2.X - p1.X) * (p2.X - p1.X)) + ((p2.Y - p1.Y) * (p2.Y - p1.Y)));

        public Route Crossover(Route otherRoute) =>
            new Route(_dna.Crossover(otherRoute._dna));

        public void Mutate(double rate)
        {
            _dna.Mutate(rate);
            TotalDistance = CalcTotalDistance();
        }

        public static bool operator ==(Route a, Route b)
        {
            if (a is null)
            {
                return b is null;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Route a, Route b) => !(a == b);

        public bool Equals([AllowNull] Route other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(other, this))
                return true;

            return _dna.Equals(other._dna);
        }

        public override bool Equals(object obj)
        {
            if (obj is Route dna && obj != null)
                return Equals(dna);

            return base.Equals(obj);
        }

        public override int GetHashCode() => _dna.GetHashCode();

        public override string ToString() => $"Route: {_dna}";
    }
}
