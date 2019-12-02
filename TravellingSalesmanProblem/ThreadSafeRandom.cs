using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TravellingSalesmanProblem
{
    public static class ThreadSafeRandom
    {
        private static readonly Random s_global = new Random();
        private static readonly ThreadLocal<Random> s_threadLocal = new ThreadLocal<Random>(GetNewRandom);

        public static Random ThreadSafeInstance => s_threadLocal.Value!; // can't be null because GetNewRandom never returns null

        public static int Next() => ThreadSafeInstance.Next();
        public static int Next(int maxValue) => ThreadSafeInstance.Next(maxValue);
        public static int Next(int minValue, int maxValue) => ThreadSafeInstance.Next(minValue, maxValue);
        public static double NextDouble() => ThreadSafeInstance.NextDouble();

        private static Random GetNewRandom()
        {
            int seed;
            lock (s_global)
            {
                seed = s_global.Next();
            }
            return new Random(seed);
        }
    }
}
