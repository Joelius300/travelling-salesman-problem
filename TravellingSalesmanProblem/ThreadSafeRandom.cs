using System;
using System.Collections.Generic;
using System.Text;

namespace TravellingSalesmanProblem
{
    public static class ThreadSafeRandom
    {
        private static readonly Random s_global = new Random();
        [ThreadStatic]
        private static Random? _local;

        public static Random ThreadSafeInstance => AssureExisting();

        public static int Next() => AssureExisting().Next();

        public static int Next(int maxValue) => AssureExisting().Next(maxValue);

        public static int Next(int minValue, int maxValue) => AssureExisting().Next(minValue, maxValue);

        public static double NextDouble() => AssureExisting().NextDouble();

        private static Random AssureExisting()
        {
            if (_local == null)
            {
                int seed;
                lock (s_global) seed = s_global.Next();
                _local = new Random(seed);
            }

            return _local;
        }
    }
}
