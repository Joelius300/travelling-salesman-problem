using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TravellingSalesmanProblem
{
    public partial class MainView : Form
    {
        private const int AmountCities = 20;
        private const int PopulationSize = 20;

        private readonly Graphics _panelGraphics;
        private readonly ImmutableArray<Point> _cities;
        private readonly Random _random;
        private readonly Population _pop;

        public MainView()
        {
            _random = new Random();
            InitializeComponent();
            _panelGraphics = _panel.CreateGraphics();
            _cities = ImmutableArray.ToImmutableArray(_random.GenerateRandomPoints(AmountCities, _panel.Size));
            _pop = new Population(_cities, PopulationSize);
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
                _panelGraphics.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
