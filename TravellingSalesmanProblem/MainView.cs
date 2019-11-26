using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
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
        private const string ZeroString = "0";
        private const string TimeFormatString = "{0:hh\\:mm\\:ss}";

        private readonly Random _random;
        private readonly List<(Route route, int gen)> _routeHistory;
        private ImmutableArray<PointF> _cities;
        private Population? _population;
        private CancellationTokenSource? _cts;
        // private Task? _runningTask;
        private DateTime _startTime;
        private Route? _bestRoute;
        private Route? _currentRoute;

        public MainView()
        {
            _random = new Random();
            _routeHistory = new List<(Route route, int gen)>();
            InitializeComponent();
            _nudCores.Maximum = Environment.ProcessorCount;
        }

        private void Reset()
        {
            _routeHistory.Clear();
            _currentRoute = null;
            _pnlCities.Invalidate(); // panel clear
            _bestRoute = null;
            _cts?.Cancel();
            _lblDist.Text = ZeroString;
            _lblGen.Text = ZeroString;
            _lblGenATM.Text = ZeroString;
            _lblTime.Text = string.Format(TimeFormatString, TimeSpan.Zero);
        }

        private void Start(int amountCities, int populationSize, double mutationRate)
        {
            if (_population != null) // we only need to reset if there was something before
                Reset();

            _cts = new CancellationTokenSource();
            _cities = ImmutableArray.ToImmutableArray(_random.GenerateRandomPoints(amountCities));
            _population = new Population(_cities, populationSize, mutationRate)
            {
                AmountCores = (int)_nudCores.Value
            };

            CancellationToken token = _cts.Token;
            _ = Task.Run(() => DoTheThing(token), token);
            _startTime = DateTime.UtcNow;
            _tmrEvolving.Start();
            _nudAmountCities.Enabled = false;
            _btnStart.Enabled = false;
            _tbGens.Focus();
            _btnStop.Enabled = true;

            void DoTheThing(CancellationToken cancelToken)
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    NextGeneration();
                    _lblGenATM.SetText(_population!.Generation.ToString());
                }
            }
        }

        private void NextGeneration()
        {
            if (_population == null)
                return;

            _population.NextGeneration();

            if (_bestRoute == null || _population.Best.TotalDistance < _bestRoute.TotalDistance)
            {
                _bestRoute = _population.Best;
                int gen = _population.Generation;
                _routeHistory.Add((_bestRoute, gen));
                _tbGens.SetMaximum(_routeHistory.Count - 1);

                if (_routeHistory.Count == 1)
                {
                    DisplayRoute(0);
                }
                else
                {
                    _tbGens.SetValue(_routeHistory.Count - 1);
                }
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            int amountCities = (int)_nudAmountCities.Value;
            int popSize = (int)_nudPopSize.Value;
            double mutationRate = (double)_nudMutationRate.Value / 100;

            Start(amountCities, popSize, mutationRate);
        }

        private void PnlCities_Paint(object sender, PaintEventArgs e)
        {
            if (_currentRoute == null)
                return;

            using Graphics graphics = e.Graphics;
            using Pen pen = new Pen(Color.Green, 2);
            graphics.DrawLines(pen, _currentRoute.Points.Select(p => MapRelativePointFToSize(p, _pnlCities.Size)).ToArray());
        }

        private void TmrEvolving_Tick(object sender, EventArgs e)
        {
            _lblTime.Text = string.Format(TimeFormatString, DateTime.UtcNow.Subtract(_startTime));
        }

        private void TbGens_ValueChanged(object sender, EventArgs e)
        {
            DisplayRoute(_tbGens.Value);
        }

        private void DisplayRoute(int historyIndex)
        {
            var (route, gen) = _routeHistory[historyIndex];
            _currentRoute = route;
            _lblGen.SetText(gen.ToString());
            string distanceText = $"{route.TotalDistance:F2}";
            if (historyIndex > 0)
            {
                distanceText += $" ({route.TotalDistance - _routeHistory[historyIndex - 1].route.TotalDistance:F2})";
            }
            _lblDist.SetText(distanceText);
            _pnlCities.Invalidate();
        }

        private void NudPopSize_ValueChanged(object sender, EventArgs e)
        {
            if (_population == null)
                return;

            _population.Size = (int)_nudPopSize.Value;
        }

        private void NudMutationRate_ValueChanged(object sender, EventArgs e)
        {
            if (_population == null)
                return;

            _population.MutationRate = (double)_nudMutationRate.Value / 100;
        }

        private void NudCores_ValueChanged(object sender, EventArgs e)
        {
            if (_population == null)
                return;

            _population.AmountCores = (int)_nudCores.Value;
        }

        private void MainView_Resize(object sender, EventArgs e)
        {
            _pnlCities.Invalidate();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            _tmrEvolving.Stop();
            _nudAmountCities.Enabled = true;
            _btnStart.Enabled = true;
            _btnStart.Focus();
            _btnStop.Enabled = false;
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts?.Cancel();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cts?.Dispose();
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private static Point MapRelativePointFToSize(PointF value, Size size) =>
            new Point
            {
                X = (int)(value.X * size.Width),
                Y = (int)(value.Y * size.Height)
            };
    }
}
