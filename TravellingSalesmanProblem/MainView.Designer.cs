namespace TravellingSalesmanProblem
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._pnlCities = new System.Windows.Forms.Panel();
            this._btnStart = new System.Windows.Forms.Button();
            this._lblDist = new System.Windows.Forms.Label();
            this._btnStop = new System.Windows.Forms.Button();
            this._nudPopSize = new System.Windows.Forms.NumericUpDown();
            this._nudAmountCities = new System.Windows.Forms.NumericUpDown();
            this._nudMutationRate = new System.Windows.Forms.NumericUpDown();
            this._lblGen = new System.Windows.Forms.Label();
            this._tbGens = new System.Windows.Forms.TrackBar();
            this._label1 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._lblGenATM = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._lblTime = new System.Windows.Forms.Label();
            this._label5 = new System.Windows.Forms.Label();
            this._tmrEvolving = new System.Windows.Forms.Timer(this.components);
            this._nudCores = new System.Windows.Forms.NumericUpDown();
            this._label3 = new System.Windows.Forms.Label();
            this._label6 = new System.Windows.Forms.Label();
            this._label7 = new System.Windows.Forms.Label();
            this._label8 = new System.Windows.Forms.Label();
            this._btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._nudPopSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudAmountCities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudMutationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tbGens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudCores)).BeginInit();
            // 
            // _pnlCities
            // 
            this._pnlCities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlCities.BackColor = System.Drawing.Color.White;
            this._pnlCities.Location = new System.Drawing.Point(14, 14);
            this._pnlCities.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._pnlCities.Name = "_pnlCities";
            this._pnlCities.Size = new System.Drawing.Size(624, 314);
            this._pnlCities.TabIndex = 0;
            this._pnlCities.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlCities_Paint);
            // 
            // _btnStart
            // 
            this._btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._btnStart.Location = new System.Drawing.Point(14, 462);
            this._btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(46, 25);
            this._btnStart.TabIndex = 1;
            this._btnStart.Text = "Go";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // _lblDist
            // 
            this._lblDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblDist.AutoSize = true;
            this._lblDist.Location = new System.Drawing.Point(16, 366);
            this._lblDist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblDist.Name = "_lblDist";
            this._lblDist.Size = new System.Drawing.Size(31, 15);
            this._lblDist.TabIndex = 2;
            this._lblDist.Text = "0000";
            // 
            // _btnStop
            // 
            this._btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStop.Location = new System.Drawing.Point(593, 462);
            this._btnStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._btnStop.Name = "_btnStop";
            this._btnStop.Size = new System.Drawing.Size(46, 25);
            this._btnStop.TabIndex = 5;
            this._btnStop.Text = "Stop";
            this._btnStop.UseVisualStyleBackColor = true;
            this._btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // _nudPopSize
            // 
            this._nudPopSize.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._nudPopSize.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this._nudPopSize.Location = new System.Drawing.Point(66, 463);
            this._nudPopSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._nudPopSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._nudPopSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nudPopSize.Name = "_nudPopSize";
            this._nudPopSize.Size = new System.Drawing.Size(91, 23);
            this._nudPopSize.TabIndex = 2;
            this._nudPopSize.Value = new decimal(new int[] {
            750,
            0,
            0,
            0});
            this._nudPopSize.ValueChanged += new System.EventHandler(this.NudPopSize_ValueChanged);
            // 
            // _nudAmountCities
            // 
            this._nudAmountCities.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._nudAmountCities.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._nudAmountCities.Location = new System.Drawing.Point(164, 463);
            this._nudAmountCities.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._nudAmountCities.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nudAmountCities.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._nudAmountCities.Name = "_nudAmountCities";
            this._nudAmountCities.Size = new System.Drawing.Size(80, 23);
            this._nudAmountCities.TabIndex = 3;
            this._nudAmountCities.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // _nudMutationRate
            // 
            this._nudMutationRate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._nudMutationRate.Location = new System.Drawing.Point(252, 463);
            this._nudMutationRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._nudMutationRate.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this._nudMutationRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nudMutationRate.Name = "_nudMutationRate";
            this._nudMutationRate.Size = new System.Drawing.Size(80, 23);
            this._nudMutationRate.TabIndex = 4;
            this._nudMutationRate.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this._nudMutationRate.ValueChanged += new System.EventHandler(this.NudMutationRate_ValueChanged);
            // 
            // _lblGen
            // 
            this._lblGen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblGen.AutoSize = true;
            this._lblGen.Location = new System.Drawing.Point(175, 366);
            this._lblGen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblGen.Name = "_lblGen";
            this._lblGen.Size = new System.Drawing.Size(13, 15);
            this._lblGen.TabIndex = 2;
            this._lblGen.Text = "0";
            // 
            // _tbGens
            // 
            this._tbGens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbGens.LargeChange = 1;
            this._tbGens.Location = new System.Drawing.Point(14, 389);
            this._tbGens.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._tbGens.Maximum = 0;
            this._tbGens.Name = "_tbGens";
            this._tbGens.Size = new System.Drawing.Size(624, 45);
            this._tbGens.TabIndex = 6;
            this._tbGens.ValueChanged += new System.EventHandler(this.TbGens_ValueChanged);
            // 
            // _label1
            // 
            this._label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(14, 351);
            this._label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(55, 15);
            this._label1.TabIndex = 2;
            this._label1.Text = "Distance:";
            // 
            // _label2
            // 
            this._label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(173, 351);
            this._label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(68, 15);
            this._label2.TabIndex = 2;
            this._label2.Text = "Generation:";
            // 
            // _lblGenATM
            // 
            this._lblGenATM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._lblGenATM.AutoSize = true;
            this._lblGenATM.Location = new System.Drawing.Point(456, 366);
            this._lblGenATM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblGenATM.Name = "_lblGenATM";
            this._lblGenATM.Size = new System.Drawing.Size(13, 15);
            this._lblGenATM.TabIndex = 2;
            this._lblGenATM.Text = "0";
            // 
            // _label4
            // 
            this._label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._label4.AutoSize = true;
            this._label4.Location = new System.Drawing.Point(454, 351);
            this._label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(68, 15);
            this._label4.TabIndex = 2;
            this._label4.Text = "Generation:";
            // 
            // _lblTime
            // 
            this._lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTime.AutoSize = true;
            this._lblTime.Location = new System.Drawing.Point(550, 366);
            this._lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblTime.Name = "_lblTime";
            this._lblTime.Size = new System.Drawing.Size(13, 15);
            this._lblTime.TabIndex = 2;
            this._lblTime.Text = "0";
            // 
            // _label5
            // 
            this._label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._label5.AutoSize = true;
            this._label5.Location = new System.Drawing.Point(547, 351);
            this._label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label5.Name = "_label5";
            this._label5.Size = new System.Drawing.Size(36, 15);
            this._label5.TabIndex = 2;
            this._label5.Text = "Time:";
            // 
            // _tmrEvolving
            // 
            this._tmrEvolving.Interval = 1000;
            this._tmrEvolving.Tick += new System.EventHandler(this.TmrEvolving_Tick);
            // 
            // _nudCores
            // 
            this._nudCores.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._nudCores.Location = new System.Drawing.Point(340, 463);
            this._nudCores.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._nudCores.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._nudCores.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nudCores.Name = "_nudCores";
            this._nudCores.Size = new System.Drawing.Size(44, 23);
            this._nudCores.TabIndex = 4;
            this._nudCores.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nudCores.ValueChanged += new System.EventHandler(this.NudCores_ValueChanged);
            // 
            // _label3
            // 
            this._label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(336, 445);
            this._label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(40, 15);
            this._label3.TabIndex = 2;
            this._label3.Text = "Cores:";
            // 
            // _label6
            // 
            this._label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._label6.AutoSize = true;
            this._label6.Location = new System.Drawing.Point(248, 445);
            this._label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(79, 15);
            this._label6.TabIndex = 2;
            this._label6.Text = "Mutationrate:";
            // 
            // _label7
            // 
            this._label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._label7.AutoSize = true;
            this._label7.Location = new System.Drawing.Point(161, 445);
            this._label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label7.Name = "_label7";
            this._label7.Size = new System.Drawing.Size(39, 15);
            this._label7.TabIndex = 2;
            this._label7.Text = "Cities:";
            // 
            // _label8
            // 
            this._label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._label8.AutoSize = true;
            this._label8.Location = new System.Drawing.Point(63, 445);
            this._label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._label8.Name = "_label8";
            this._label8.Size = new System.Drawing.Size(90, 15);
            this._label8.TabIndex = 2;
            this._label8.Text = "Population size:";
            // 
            // _btnNew
            // 
            this._btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnNew.Location = new System.Drawing.Point(540, 462);
            this._btnNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(46, 25);
            this._btnNew.TabIndex = 5;
            this._btnNew.Text = "New";
            this._btnNew.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 500);
            this.Controls.Add(this._tbGens);
            this.Controls.Add(this._nudCores);
            this.Controls.Add(this._nudMutationRate);
            this.Controls.Add(this._nudAmountCities);
            this.Controls.Add(this._nudPopSize);
            this.Controls.Add(this._btnNew);
            this.Controls.Add(this._btnStop);
            this.Controls.Add(this._label5);
            this.Controls.Add(this._label4);
            this.Controls.Add(this._lblTime);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._lblGenATM);
            this.Controls.Add(this._label8);
            this.Controls.Add(this._label7);
            this.Controls.Add(this._label6);
            this.Controls.Add(this._label3);
            this.Controls.Add(this._label1);
            this.Controls.Add(this._lblGen);
            this.Controls.Add(this._lblDist);
            this.Controls.Add(this._btnStart);
            this.Controls.Add(this._pnlCities);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._nudPopSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudAmountCities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudMutationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tbGens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudCores)).EndInit();

        }

        #endregion

        private System.Windows.Forms.Panel _pnlCities;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.Label _lblDist;
        private System.Windows.Forms.Button _btnStop;
        private System.Windows.Forms.NumericUpDown _nudPopSize;
        private System.Windows.Forms.NumericUpDown _nudAmountCities;
        private System.Windows.Forms.NumericUpDown _nudMutationRate;
        private System.Windows.Forms.Label _lblGen;
        private System.Windows.Forms.TrackBar _tbGens;
        private System.Windows.Forms.Label _label1;
        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.Label _lblGenATM;
        private System.Windows.Forms.Label _label4;
        private System.Windows.Forms.Label _lblTime;
        private System.Windows.Forms.Label _label5;
        private System.Windows.Forms.Timer _tmrEvolving;
        private System.Windows.Forms.NumericUpDown _nudCores;
        private System.Windows.Forms.Label _label3;
        private System.Windows.Forms.Label _label6;
        private System.Windows.Forms.Label _label7;
        private System.Windows.Forms.Label _label8;
        private System.Windows.Forms.Button _btnNew;
    }
}