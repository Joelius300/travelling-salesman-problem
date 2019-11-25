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
        private const int AmountCities = 30;
        private const int PopulationSize = 500;
        private const double MutationRate = 0.03;

        private readonly ImmutableArray<Point> _cities;
        private readonly Random _random;
        private readonly Population _pop;
        private readonly CancellationTokenSource _cts;
        private Route? _best;
        private Task? _runningTask;

        public MainView()
        {
            _random = new Random();
            InitializeComponent();
            _panel.Paint += Panel_Paint;
            DoubleBuffered = true;
            _cities = ImmutableArray.ToImmutableArray(_random.GenerateRandomPoints(AmountCities, _panel.Size));
            _pop = new Population(_cities, PopulationSize, MutationRate);
            Load += MainView_FormLoad;
            FormClosing += MainView_FormClosing;
            _cts = new CancellationTokenSource();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (_best is null)
                return;

            using Graphics graphics = e.Graphics;
            using Pen pen = new Pen(Color.Green, 2);

            graphics.Clear(Color.White);
            graphics.DrawLines(pen, _best.Points.ToArray());
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts.Cancel();
        }

        private void MainView_FormLoad(object? sender, EventArgs e)
        {
            CancellationToken token = _cts.Token;
            _runningTask = Task.Run(() => DoTheThing(token), token);

            void DoTheThing(CancellationToken cancelToken)
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    NextGeneration();
                }
            }
        }

        private void NextGeneration()
        {
            Console.WriteLine($"Generation: {_pop.Generation}");
            Console.WriteLine($"Best: {_best}");
            _pop.NextGeneration();

            if (_best is null || _pop.Best.TotalDistance < _best.TotalDistance)
            {
                _best = _pop.Best;
                _panel.Invalidate();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _cts.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
