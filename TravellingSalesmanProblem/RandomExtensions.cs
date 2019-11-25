using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TravellingSalesmanProblem
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            if (rng == null)
                throw new ArgumentNullException(nameof(rng));

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static IEnumerable<Point> GenerateRandomPoints(this Random rng, int amount, Size size)
        {
            if (rng == null)
                throw new ArgumentNullException(nameof(rng));

            for (int i = 0; i < amount; i++)
            {
                yield return new Point(rng.Next(0, size.Width), rng.Next(0, size.Height));
            }
        }

        public static Route PickRouteAccordingToFitness(this Random rng, IReadOnlyList<Route> routes)
        {
            if (rng == null)
                throw new ArgumentNullException(nameof(rng));

            if (routes == null)
                throw new ArgumentNullException(nameof(routes));

            int index = 0;
            double r;

            do
            {
                r = rng.NextDouble();
            } while (r == 0);

            while (r > 0)
            {
                r -= routes[index].Fitness;
                index++;
            }
            index--;

            // The fitness has to be normalized in routes otherwise this might fail
            return routes[index];
        }

        public static IEnumerable<int> RandomRange(this Random rng, int start, int count) =>
            Enumerable.Range(start, count).OrderBy(x => rng.Next());
    }
}
