using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Genetic_Algorithm
{
    class Population
    {
        private static readonly Random randomForPickOne = new Random();
        private static readonly object syncLock = new object();

        Random rnd = new Random();

        public int allowedCores;
        public int mutationRate;

        CancellationTokenSource cancelTokenNextGen;

        Route[] routes;
        Route bestRoute;

        public Population(int size, Point[] cities, int mutationRate) {
            InitializeRoutes(size, cities);
            CalculateFitness();

            this.mutationRate = mutationRate;
        }

        public void SetGeneration(int gen) {
            foreach (Route o in routes)
            {
                o.generation = gen;
            }
        }

        private void InitializeRoutes(int size, Point[] cities) {
            routes = new Route[size];
            for (int i = 0; i < size; i++)
            {
                routes[i] = new Route(cities);
            }
        }

        public void CalculateFitness() {
            double totalFitness = 0;
            double record = double.MaxValue;

            //Stopwatch debugSw = new Stopwatch();

            //debugSw.Start();
            //// First type parameter is the type of the source elements
            //// Second type parameter is the type of the thread-local variable (partition subtotal)
            //Parallel.ForEach<Route, double>(routes, // source collection
            //                            () => 0, // method to initialize the local variable
            //                            (r, loop, subtotal) => // method invoked by the loop on each iteration
            //                         {
            //                             r.fitness = 1 / Math.Pow(r.totalDistance, 4);
            //                             subtotal += r.fitness; //modify local variable

            //                             if (r.totalDistance < record) {
            //                                 record = r.totalDistance;
            //                                 bestRoute = r;
            //                             }

            //                             return subtotal; // value to be passed to next iteration
            //                         },
            //                            // Method to be executed when each partition has completed.
            //                            // finalResult is the final value of subtotal for a particular partition.
            //                            (finalResult) => 
            //                                {
            //                                    totalFitness = finalResult;

            //                                    foreach (Route r in routes)
            //                                    {
            //                                        r.fitness = r.fitness / totalFitness;
            //                                    }
            //                                }
            //                            );
            //debugSw.Stop();

            //Console.WriteLine("Parallel:");
            //Console.WriteLine(debugSw.Elapsed.ToString());
            //totalFitness = 0;
            //debugSw.Reset();
            //debugSw.Start();

            foreach (Route o in routes)
            {
                o.fitness = 1 / Math.Pow(o.totalDistance, 4);
                totalFitness += o.fitness;

                if (o.totalDistance < record)
                {
                    record = o.totalDistance;
                    bestRoute = o;
                }
            }

            //normalize fitness
            foreach (Route o in routes)
            {
                o.fitness = o.fitness / totalFitness;
            }

            //debugSw.Stop();
            //Console.WriteLine("Normal:");
            //Console.WriteLine(debugSw.Elapsed.ToString());
            //debugSw.Reset();
        }

        public Route GetBestRoute() {
            return this.bestRoute;
        }

        public void Cancel() {
            try
            {
                cancelTokenNextGen.Cancel();
            }
            catch (Exception e) {
                //This just means (most of the time) that the cancel token has been deleted 
                //since the loop has already finished or not been startet yet, so its no problem
                Console.WriteLine($"Couldn't be cancelled. ({e.Message})");
            }
        }

        public void GoToNextGeneration() {
            Route[] newRoutes = new Route[routes.Length];

            //Stopwatch debugSw = new Stopwatch();

            bool needsToBeThreadSafe = false;

            //Good use of parallel class here (enough work being done)
            cancelTokenNextGen = new CancellationTokenSource();
            if (allowedCores > 1 && allowedCores <= Environment.ProcessorCount)
            {
                needsToBeThreadSafe = true;

                ParallelOptions po = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = allowedCores,
                    CancellationToken = cancelTokenNextGen.Token
                };

                //debugSw.Start();

                try
                {
                    Parallel.For(0, newRoutes.Length, po, (i) =>
                    {
                        Route o1 = PickOneRoute(needsToBeThreadSafe);
                        Route o2;
                        do
                        {
                            o2 = PickOneRoute(needsToBeThreadSafe);
                        } while (o2 != o1);

                        Route newRoute = o1.Crossover(o2);
                        newRoute.Mutate(this.mutationRate);

                        newRoutes[i] = newRoute;
                    });
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    cancelTokenNextGen.Dispose();
                }

                //debugSw.Stop();

                ////Console.WriteLine("Parallel Next Gen:");
                //Console.WriteLine(debugSw.Elapsed.ToString());
            }
            else {
                //debugSw.Reset();
                //debugSw.Start();

                for (int i = 0; i < newRoutes.Length; i++)
                {
                    Route o1 = PickOneRoute(needsToBeThreadSafe);
                    Route o2;
                    do
                    {
                        o2 = PickOneRoute(needsToBeThreadSafe);
                    } while (o2 != o1 && !o1.Equals(o2));

                    Route newRoute = o1.Crossover(o2);
                    newRoute.Mutate(this.mutationRate);

                    newRoutes[i] = newRoute;
                }

                //debugSw.Stop();

                //Console.WriteLine("Normal Next Gen:");
                //Console.WriteLine(debugSw.Elapsed.ToString());
            }

            routes = newRoutes;
        }

        private double GetRandomDoubleThreadSafe() {
            lock (syncLock) {
                return randomForPickOne.NextDouble();
            }
        }

        public Route PickOneRoute(bool needsToBeThreadSafe)
        {
            int index = 0;
            double r;

            do
            {
                r = needsToBeThreadSafe ? 
                    GetRandomDoubleThreadSafe() : 
                    randomForPickOne.NextDouble();
            } while (r == 0);

            while (r > 0)
            {
                r = r - routes[index].fitness;
                index++;
            }
            index--;

            try
            {
                return routes[index];
            }
            catch {
                return routes.Last(); //just in case; should never get to this point
            }
        }
    }
}
