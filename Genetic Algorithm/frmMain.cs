using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genetic_Algorithm
{
    public partial class frmMain : Form
    {
        double allTimeBestDistance = double.MaxValue;
        Thread th;

        private int gen = 1;
        private int timeEvolving;

        Graphics panelGraphics;

        List<Route> bestRoutes = new List<Route>();

        volatile bool allowedToDoTheThing = true;

        private Point[] cities;

        Population pop;

        private int citiesAmount = 10;
        private int popSize = 250;
        private int mutationRate = 3;
        private int allowedCores = 1;

        private Random rnd = new Random();

        public frmMain()
        {
            InitializeComponent();

            panelGraphics = pnlCities.CreateGraphics();

            th = new Thread(doTheThing);

            nudAmountCities.Value = citiesAmount;
            nudPopSize.Value = popSize;
            nudMutationRate.Value = mutationRate;
            nudCores.Maximum = Environment.ProcessorCount;
        }

        private void InitializeCities() {
            cities = new Point[citiesAmount];
            for (int i = 0; i < citiesAmount; i++)
            {
                cities[i] = new Point(rnd.Next(0, pnlCities.Width), rnd.Next(0, pnlCities.Height));
            }
        }

        private void doTheThing()
        {
            pop = new Population(popSize, cities, mutationRate);
            pop.allowedCores = this.allowedCores;
            Route bestCurrent;

            Stopwatch debugSw = new Stopwatch();
            Console.WriteLine("Time for one cycle (in miliseconds):");

            while (allowedToDoTheThing) {
                debugSw.Start();

                pop.SetGeneration(gen);
                pop.CalculateFitness();

                bestCurrent = pop.GetBestRoute();

                if (allTimeBestDistance > bestCurrent.totalDistance)
                {
                    allTimeBestDistance = bestCurrent.totalDistance;

                    bestRoutes.Add(bestCurrent);

                    ChangeTrackBarMax(bestRoutes.Count - 1);

                    //will be displayed (event fired on changing the value of the trackbar)
                    //DisplayBest();
                }

                ChangeLabelText(lblGenATM, gen.ToString());

                pop.GoToNextGeneration();
                gen++;

                debugSw.Stop();
                ////Format for ten-thousandths of a second
                ////e.g. 0893 = 89 miliseconds -> (0)893 ten-thousandths of a second
                Console.WriteLine(debugSw.ElapsedMilliseconds + " ms");
                debugSw.Reset();
            }
        }

        //private bool IsGenTrackBarAtMax() {
        //    if (tbGens.InvokeRequired)
        //    {
        //        Func<bool> isAtMax = new Func<bool>(delegate
        //        {
        //            if (tbGens.Value == tbGens.Maximum)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        });

        //        return (bool)this.Invoke(isAtMax);
        //    }
        //    else {
        //        if (tbGens.Value == tbGens.Maximum)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        private void ChangeLabelText(Label lbl, string text) {
            if (lbl.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    lbl.Text = text;
                }));
            }
            else {
                lbl.Text = text;
            }
        }

        private void ChangeTrackBarMax(int max) {
            if (tbGens.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    tbGens.Maximum = max;
                    tbGens.Value = tbGens.Maximum;
                }));
            }
            else {
                tbGens.Maximum = max;
                tbGens.Value = tbGens.Maximum;
            }
        }

        private void DisplayRoute(object sender, EventArgs e) {
            if (bestRoutes.Count > 0) {
                Route r = null;
                Route oneLower = null;
                try
                {
                    r = bestRoutes[tbGens.Value];

                    if (tbGens.Value > 0) {
                        oneLower = bestRoutes[tbGens.Value - 1];
                    }
                }
                catch {}

                if (r != null) {
                    DisplayRoute(r, oneLower);
                }
            }
        }

        private void DisplayRoute(Route route, Route oneLower = null) {
            panelGraphics.Clear(Color.White);

            //Point[] points = route.GetPoints();

            using (Pen p = new Pen(Color.Green, 2))
            {
                panelGraphics.DrawLines(p, route.GetPoints());

                //not needed, nativ method probably better (definitely shorter in code)
                //but has the same result
                //for (int i = 0; i < points.Length - 1; i++)
                //{
                //    panelGraphics.DrawLine(p, points[i], points[i + 1]);
                //}
            }

            if (oneLower != null) {
                ChangeLabelText(lblDist, Math.Round(route.totalDistance, 2).ToString() + $" ({Math.Round(route.totalDistance - oneLower.totalDistance, 2).ToString()})");
            }
            else{
                ChangeLabelText(lblDist, Math.Round(route.totalDistance, 2).ToString());
            }

            ChangeLabelText(lblGen, route.generation.ToString());
        }

        private void btnDoTheThing_Click(object sender, EventArgs e)
        {
            if (!th.IsAlive)
            {
                timeEvolving = 0;
                tmrEvolving.Start();

                bestRoutes = new List<Route>();

                //tbGens.Enabled = false;
                //lblGen.Visible = true;

                //nudMutationRate.Enabled = false;
                nudAmountCities.Enabled = false;
                nudPopSize.Enabled = false;


                allowedToDoTheThing = true;

                InitializeCities();

                th = new Thread(doTheThing);
                th.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset() {
            if (th.IsAlive)
            {
                //these should be first just to give the most possible time to cancel the evolution
                allowedToDoTheThing = false;
                pop?.Cancel();

                tmrEvolving.Stop();
                lblTime.Text = "0";

                //nudMutationRate.Enabled = true;
                nudAmountCities.Enabled = true;
                nudPopSize.Enabled = true;

                gen = 1;

                allTimeBestDistance = double.MaxValue;

                //this should be last (same reason as the first stuff)
                Thread.Sleep(100);
                th.Abort();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reset();
        }

        private void nudPopSize_ValueChanged(object sender, EventArgs e)
        {
            this.popSize = (int)nudPopSize.Value;
        }

        private void nudAmountCities_ValueChanged(object sender, EventArgs e)
        {
            this.citiesAmount = (int)nudAmountCities.Value;
        }

        private void nudMutationRate_ValueChanged(object sender, EventArgs e)
        {
            this.mutationRate = (int)nudMutationRate.Value;
            try
            {
                pop.mutationRate = this.mutationRate;
            }
            catch {}
        }

        private void tmrEvolving_Tick(object sender, EventArgs e)
        {
            timeEvolving++;
            lblTime.Text = timeEvolving.ToString();
        }

        private void nudCores_ValueChanged(object sender, EventArgs e)
        {
            allowedCores = (int)nudCores.Value;
            try
            {
                pop.allowedCores = this.allowedCores;
            }
            catch { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new frmMain().Show();
        }
    }
}
