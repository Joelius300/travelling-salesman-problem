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
        private Point[]? _cities;
        private Population? _pop;
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
            DoubleBuffered = true;
        }

        private void Reset()
        {
            _routeHistory.Clear();
            _currentRoute = null;
            _bestRoute = null;
            _cts?.Cancel();
            _lblDist.Text = ZeroString;
            _lblGen.Text = ZeroString;
            _lblGenATM.Text = ZeroString;
            _lblTime.Text = string.Format(TimeFormatString, TimeSpan.Zero);
        }

        private void Start(int amountCities, int populationSize, double mutationRate, Size area)
        {
            if (_pop != null) // we only need to reset if there was something before
                Reset();

            _cts = new CancellationTokenSource();
            _cities = _random.GenerateRandomPoints(amountCities, area).ToArray();
            _pop = new Population(_cities, populationSize, mutationRate)
            {
                AmountCores = (int)_nudCores.Value
            };

            CancellationToken token = _cts.Token;
            _ = Task.Run(() => DoTheThing(token), token);
            _startTime = DateTime.UtcNow;
            _tmrEvolving.Start();
            _nudAmountCities.Enabled = false;

            void DoTheThing(CancellationToken cancelToken)
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    NextGeneration();
                    _lblGenATM.SetText(_pop!.Generation.ToString());
                }
            }
        }

        private void NextGeneration()
        {
            if (_pop == null)
                return;

            _pop.NextGeneration();

            if (_bestRoute == null || _pop.Best.TotalDistance < _bestRoute.TotalDistance)
            {
                _bestRoute = _pop.Best;
                int gen = _pop.Generation;
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
            Size panelSize = _pnlCities.Size;

            Start(amountCities, popSize, mutationRate, panelSize);
        }

        private void PnlCities_Paint(object sender, PaintEventArgs e)
        {
            if (_currentRoute == null)
                return;

            using Graphics graphics = e.Graphics;
            using Pen pen = new Pen(Color.Green, 2);

            graphics.Clear(Color.White);
            graphics.DrawLines(pen, _currentRoute.Points.ToArray());
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
            if (_pop == null)
                return;

            _pop.Size = (int)_nudPopSize.Value;
        }

        private void NudMutationRate_ValueChanged(object sender, EventArgs e)
        {
            if (_pop == null)
                return;

            _pop.MutationRate = (double)_nudMutationRate.Value / 100;
        }

        private void NudCores_ValueChanged(object sender, EventArgs e)
        {
            if (_pop == null)
                return;

            _pop.AmountCores = (int)_nudCores.Value;
        }

        // doesn't work because the TotalDistances would have to be recalculated because the coordinates are not normalized (yet)
        // private void MainView_ResizeEnd(object sender, EventArgs e)
        // {
        //     if (_cities == null)
        //         return;

        //     Size newSize = _pnlCities.Size;
        //     for (int i = 0; i < _cities.Length; i++)
        //     {
        //         Point oldPoint = _cities[i];
        //         Point newPoint = new Point
        //         {
        //             X = (int)Map(oldPoint.X, 0, _lastSize.Width, 0, newSize.Width),
        //             Y = (int)Map(oldPoint.Y, 0, _lastSize.Width, 0, newSize.Width)
        //         };
        //         _cities[i] = newPoint;
        //     }

        //     _lastSize = newSize;
        //     _pnlCities.Invalidate();

        //     static decimal Map(decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget) =>
        //         (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        // }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _nudAmountCities.Enabled = true;
            _tmrEvolving.Stop();
            _cts?.Cancel();
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
    }
}
