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

namespace TravellingSalesmanProblem
{
    public partial class MainView : Form
    {
        private static readonly Random s_rnd = new Random();

        private readonly Thread _th;
        private readonly Graphics _panelGraphics;
        readonly List<Route> _bestRoutes = new List<Route>();

        private double _allTimeBestDistance = double.MaxValue;
        private volatile bool _allowedToDoTheThing = true;
        private int _gen = 1;
        private int _timeEvolving;
        private Point[] _cities;
        private Population _pop;

        // private int _citiesAmount = 10;
        // private int _popSize = 250;
        // private int _mutationRate = 3;
        // private int _allowedCores = 1;

        public MainView()
        {
            InitializeComponent();
            _panelGraphics = pnlCities.CreateGraphics();
            _th = new Thread(DoTheThing);

            nudAmountCities.Value = 10; // _citiesAmount;
            nudPopSize.Value = 250; // _popSize;
            nudMutationRate.Value = 3; // _mutationRate;
            nudCores.Maximum = Environment.ProcessorCount;
        }

        private Point[] InitializeCities()
        {
            int amountCities = (int)nudAmountCities.Value;
            Point[] cities = new Point[amountCities];
            for (int i = 0; i < amountCities; i++)
            {
                cities[i] = new Point(s_rnd.Next(0, pnlCities.Width), s_rnd.Next(0, pnlCities.Height));
            }

            return cities;
        }

        private void DoTheThing()
        {
            int popSize = (int)nudPopSize.Value;
            double mutationRate = (double)nudMutationRate.Value / 100;

            _cities = InitializeCities();
            _pop = new Population(popSize, _cities, mutationRate)
            {
                AllowedCores = (int)nudCores.Value // _allowedCores
            };

            Route bestCurrent;

            // Stopwatch debugSw = new Stopwatch();
            // Console.WriteLine("Time for one cycle (in miliseconds):");

            while (_allowedToDoTheThing)
            {
                // debugSw.Start();

                _pop.SetGeneration(_gen);
                _pop.CalculateFitness();

                bestCurrent = _pop.BestRoute;

                if (_allTimeBestDistance > bestCurrent.TotalDistance)
                {
                    _allTimeBestDistance = bestCurrent.TotalDistance;
                    _bestRoutes.Add(bestCurrent);
                    ChangeTrackBarMax(_bestRoutes.Count - 1);
                }

                ChangeLabelText(lblGenATM, _gen.ToString());

                _pop.GoToNextGeneration();
                _gen++;

                // debugSw.Stop();
                // Console.WriteLine(debugSw.ElapsedMilliseconds + " ms");
                // debugSw.Reset();
            }
        }

        private void ChangeLabelText(Label lbl, string text)
        {
            if (lbl.InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateText));
            }
            else
            {
                UpdateText();
            }

            void UpdateText()
            {
                lbl.Text = text;
            }
        }

        private void ChangeTrackBarMax(int max)
        {
            if (tbGens.InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateMaximum));
            }
            else
            {
                UpdateMaximum();
            }

            void UpdateMaximum()
            {
                tbGens.Maximum = max;
                tbGens.Value = tbGens.Maximum;
            }
        }

        private void TbGens_ValueChanged(object sender, EventArgs e)
        {
            if (_bestRoutes.Count == 0)
                return;

            if (tbGens.Value >= _bestRoutes.Count)
                return;

            Route oneLower = null;
            Route r = _bestRoutes[tbGens.Value];

            if (tbGens.Value > 0)
            {
                oneLower = _bestRoutes[tbGens.Value - 1];
            }

            DisplayRoute(r, oneLower);
        }

        private void DisplayRoute(Route route, Route oneLower = null)
        {
            _panelGraphics.Clear(Color.White);

            using (Pen p = new Pen(Color.Green, 2))
            {
                _panelGraphics.DrawLines(p, route.Points);
            }

            string text = Math.Round(route.TotalDistance, 2).ToString();
            if (oneLower != null)
            {
                text += $" ({Math.Round(route.TotalDistance - oneLower.TotalDistance, 2).ToString()})";
            }

            ChangeLabelText(lblDist, text);
            ChangeLabelText(lblGen, $"Gen: {route.Generation}");
        }

        private void BtnDoTheThing_Click(object sender, EventArgs e)
        {
            if (_th.IsAlive)
                return;

            nudAmountCities.Enabled = false;
            nudPopSize.Enabled = false;
            _th.Start();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            if (!_th.IsAlive)
                return;

            _allowedToDoTheThing = false;
            tmrEvolving.Stop();
            _gen = 1;
            _allTimeBestDistance = double.MaxValue;
            nudAmountCities.Enabled = true;
            nudPopSize.Enabled = true;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reset();
        }

        //private void NudPopSize_ValueChanged(object sender, EventArgs e)
        //{
        //    _popSize = (int)nudPopSize.Value;
        //}

        //private void NudAmountCities_ValueChanged(object sender, EventArgs e)
        //{
        //    _citiesAmount = (int)nudAmountCities.Value;
        //}

        private void NudMutationRate_ValueChanged(object sender, EventArgs e)
        {
            if (_pop == null)
                return;

            _pop.MutationRate = (double)nudMutationRate.Value / 100;
        }

        private void NudCores_ValueChanged(object sender, EventArgs e)
        {
            if (_pop == null)
                return;

            _pop.AllowedCores = (int)nudCores.Value;
        }

        private void TmrEvolving_Tick(object sender, EventArgs e)
        {
            lblTime.Text = $"{++_timeEvolving} sec.";
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            // new MainView().Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _panelGraphics.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
