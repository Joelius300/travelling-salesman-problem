﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TravellingSalesmanProblem
{
    public class Route : IEquatable<Route>
    {
        private readonly IReadOnlyList<PointF> _cities;
        private readonly IList<int> _order;

        public double TotalDistance { get; }
        public double Fitness { get; internal set; }
        public IEnumerable<PointF> Points => GetPoints();

        public Route(IReadOnlyList<PointF> cities, IList<int> order)
        {
            _cities = cities ?? throw new ArgumentNullException(nameof(cities));
            _order = order ?? throw new ArgumentNullException(nameof(order));

            TotalDistance = CalcTotalDistance() * 100; // just so it's a bigger number
            Fitness = 1 / (Math.Pow(TotalDistance, 8) + 1); // good formula but has to be normalized over all routes
        }

        private IEnumerable<PointF> GetPoints()
        {
            for (int i = 0; i < _order.Count; i++)
            {
                // int cityIndex = _order[i];
                yield return _cities[_order[i]];
            }
        }

        private double CalcTotalDistance()
        {
            double totalDistance = 0;

            for (int i = 0; i < _order.Count - 1; i++)
            {
                // int indexA = _order[i];
                // int indexB = _order[i+1];
                totalDistance += CalcDistance(_cities[_order[i]], _cities[_order[i+1]]);
            }

            return totalDistance;
        }

        private static double CalcDistance(PointF p1, PointF p2) =>
            // Pythagoras
            Math.Sqrt(((p2.X - p1.X) * (p2.X - p1.X)) + ((p2.Y - p1.Y) * (p2.Y - p1.Y)));

        public void Mutate(double rate)
        {
            for (int i = 0; i < _order.Count / 2; i++)
            {
                if (ThreadSafeRandom.NextDouble() < rate)
                {
                    int i1 = ThreadSafeRandom.Next(_order.Count);
                    int i2 = (i1 + 1) % _order.Count;

                    int temp = _order[i2];
                    _order[i2] = _order[i1];
                    _order[i1] = temp;
                }
            }
        }

        public Route Crossover(Route other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            int start = ThreadSafeRandom.Next(_order.Count);
            int count = ThreadSafeRandom.Next(_order.Count - start + 1);

            List<int> newOrder = new List<int>(_order.Count); // we already know how many items there will be

            // add slice of our orders
            foreach (int cityIndex in _order.Skip(start).Take(count))
            {
                newOrder.Add(cityIndex); // no need to test if it exists here, it won't
            }

            // add all indices from the other route that aren't yet in the new route
            for (int i = 0; i < other._order.Count; i++)
            {
                int cityIndex = other._order[i];
                if (!newOrder.Contains(cityIndex))
                    newOrder.Add(cityIndex);
            }

            return new Route(_cities, newOrder);
        }

        public bool Equals(Route? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            // The cities have to be the same collection
            return ReferenceEquals(_cities, other._cities) &&
                (ReferenceEquals(_order, other._order) ||
                Enumerable.SequenceEqual(_order, other._order));
        }

        public override bool Equals(object? obj)
        {
            if (obj is Route route)
                return Equals(route);

            return false;
        }

        public override string? ToString() =>
            $"Distance: {TotalDistance}, Order: {string.Join(", ", _order)}";

        public override int GetHashCode() =>
            HashCode.Combine(_cities, _order, TotalDistance);

        public static bool operator ==(Route? left, Route? right)
        {
            if (left is null)
                return right is null;
            
            return EqualityComparer<Route>.Default.Equals(left, right);
        }

        public static bool operator !=(Route? left, Route? right) => !(left == right);
    }
}
