namespace Genetic_Algorithm
{
    partial class frmMain
    {
        /// <summary>
        /// Erfrouteliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erfrouteliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlCities = new System.Windows.Forms.Panel();
            this.btnDoTheThing = new System.Windows.Forms.Button();
            this.lblDist = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.nudPopSize = new System.Windows.Forms.NumericUpDown();
            this.nudAmountCities = new System.Windows.Forms.NumericUpDown();
            this.nudMutationRate = new System.Windows.Forms.NumericUpDown();
            this.lblGen = new System.Windows.Forms.Label();
            this.tbGens = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGenATM = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tmrEvolving = new System.Windows.Forms.Timer(this.components);
            this.nudCores = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPopSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmountCities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMutationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCores)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCities
            // 
            this.pnlCities.Location = new System.Drawing.Point(12, 12);
            this.pnlCities.Name = "pnlCities";
            this.pnlCities.Size = new System.Drawing.Size(535, 294);
            this.pnlCities.TabIndex = 0;
            // 
            // btnDoTheThing
            // 
            this.btnDoTheThing.Location = new System.Drawing.Point(12, 436);
            this.btnDoTheThing.Name = "btnDoTheThing";
            this.btnDoTheThing.Size = new System.Drawing.Size(39, 22);
            this.btnDoTheThing.TabIndex = 1;
            this.btnDoTheThing.Text = "Go";
            this.btnDoTheThing.UseVisualStyleBackColor = true;
            this.btnDoTheThing.Click += new System.EventHandler(this.btnDoTheThing_Click);
            // 
            // lblDist
            // 
            this.lblDist.AutoSize = true;
            this.lblDist.Location = new System.Drawing.Point(14, 333);
            this.lblDist.Name = "lblDist";
            this.lblDist.Size = new System.Drawing.Size(31, 13);
            this.lblDist.TabIndex = 2;
            this.lblDist.Text = "0000";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(508, 436);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(39, 22);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // nudPopSize
            // 
            this.nudPopSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPopSize.Location = new System.Drawing.Point(57, 437);
            this.nudPopSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPopSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPopSize.Name = "nudPopSize";
            this.nudPopSize.Size = new System.Drawing.Size(78, 20);
            this.nudPopSize.TabIndex = 2;
            this.nudPopSize.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPopSize.ValueChanged += new System.EventHandler(this.nudPopSize_ValueChanged);
            // 
            // nudAmountCities
            // 
            this.nudAmountCities.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudAmountCities.Location = new System.Drawing.Point(141, 437);
            this.nudAmountCities.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudAmountCities.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudAmountCities.Name = "nudAmountCities";
            this.nudAmountCities.Size = new System.Drawing.Size(69, 20);
            this.nudAmountCities.TabIndex = 3;
            this.nudAmountCities.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudAmountCities.ValueChanged += new System.EventHandler(this.nudAmountCities_ValueChanged);
            // 
            // nudMutationRate
            // 
            this.nudMutationRate.Location = new System.Drawing.Point(216, 437);
            this.nudMutationRate.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudMutationRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMutationRate.Name = "nudMutationRate";
            this.nudMutationRate.Size = new System.Drawing.Size(69, 20);
            this.nudMutationRate.TabIndex = 4;
            this.nudMutationRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMutationRate.ValueChanged += new System.EventHandler(this.nudMutationRate_ValueChanged);
            // 
            // lblGen
            // 
            this.lblGen.AutoSize = true;
            this.lblGen.Location = new System.Drawing.Point(150, 333);
            this.lblGen.Name = "lblGen";
            this.lblGen.Size = new System.Drawing.Size(13, 13);
            this.lblGen.TabIndex = 2;
            this.lblGen.Text = "0";
            // 
            // tbGens
            // 
            this.tbGens.LargeChange = 1;
            this.tbGens.Location = new System.Drawing.Point(12, 353);
            this.tbGens.Maximum = 0;
            this.tbGens.Name = "tbGens";
            this.tbGens.Size = new System.Drawing.Size(535, 45);
            this.tbGens.TabIndex = 6;
            this.tbGens.ValueChanged += new System.EventHandler(this.DisplayRoute);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Distance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Generation:";
            // 
            // lblGenATM
            // 
            this.lblGenATM.AutoSize = true;
            this.lblGenATM.Location = new System.Drawing.Point(391, 333);
            this.lblGenATM.Name = "lblGenATM";
            this.lblGenATM.Size = new System.Drawing.Size(13, 13);
            this.lblGenATM.TabIndex = 2;
            this.lblGenATM.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Generation:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(471, 333);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(13, 13);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(469, 320);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Time:";
            // 
            // tmrEvolving
            // 
            this.tmrEvolving.Interval = 1000;
            this.tmrEvolving.Tick += new System.EventHandler(this.tmrEvolving_Tick);
            // 
            // nudCores
            // 
            this.nudCores.Location = new System.Drawing.Point(291, 437);
            this.nudCores.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudCores.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCores.Name = "nudCores";
            this.nudCores.Size = new System.Drawing.Size(38, 20);
            this.nudCores.TabIndex = 4;
            this.nudCores.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCores.ValueChanged += new System.EventHandler(this.nudCores_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cores:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 421);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mutationrate:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 421);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Cities:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(54, 421);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Population size:";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(463, 436);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(39, 22);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 469);
            this.Controls.Add(this.tbGens);
            this.Controls.Add(this.nudCores);
            this.Controls.Add(this.nudMutationRate);
            this.Controls.Add(this.nudAmountCities);
            this.Controls.Add(this.nudPopSize);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblGenATM);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGen);
            this.Controls.Add(this.lblDist);
            this.Controls.Add(this.btnDoTheThing);
            this.Controls.Add(this.pnlCities);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Travelling salesman";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudPopSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmountCities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMutationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCities;
        private System.Windows.Forms.Button btnDoTheThing;
        private System.Windows.Forms.Label lblDist;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NumericUpDown nudPopSize;
        private System.Windows.Forms.NumericUpDown nudAmountCities;
        private System.Windows.Forms.NumericUpDown nudMutationRate;
        private System.Windows.Forms.Label lblGen;
        private System.Windows.Forms.TrackBar tbGens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGenATM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer tmrEvolving;
        private System.Windows.Forms.NumericUpDown nudCores;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnNew;
    }
}

