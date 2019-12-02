using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TravellingSalesmanProblem
{
    public static class ThreadSafeRandom
    {
        private static readonly ThreadLocal<Random> s_threadLocal = new ThreadLocal<Random>(() => new Random());

        public static Random ThreadSafeInstance => s_threadLocal.Value!; // can't be null because new Random() never returns null

        public static int Next() => ThreadSafeInstance.Next();
        public static int Next(int maxValue) => ThreadSafeInstance.Next(maxValue);
        public static int Next(int minValue, int maxValue) => ThreadSafeInstance.Next(minValue, maxValue);
        public static double NextDouble() => ThreadSafeInstance.NextDouble();
        public static void NextBytes(byte[] buffer) => ThreadSafeInstance.NextBytes(buffer);
        public static void NextBytes(Span<byte> buffer) => ThreadSafeInstance.NextBytes(buffer);
    }
}
