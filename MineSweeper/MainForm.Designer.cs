namespace MineSweeper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.nudNumMinas = new System.Windows.Forms.NumericUpDown();
            this.lblNumberOfMines = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumMinas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.Location = new System.Drawing.Point(405, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(12, 39);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(500, 500);
            this.pnlMain.TabIndex = 1;
            // 
            // nudNumMinas
            // 
            this.nudNumMinas.BackColor = System.Drawing.Color.Black;
            this.nudNumMinas.Font = new System.Drawing.Font("Consolas", 16F);
            this.nudNumMinas.ForeColor = System.Drawing.Color.Red;
            this.nudNumMinas.Location = new System.Drawing.Point(172, 4);
            this.nudNumMinas.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudNumMinas.Name = "nudNumMinas";
            this.nudNumMinas.Size = new System.Drawing.Size(54, 32);
            this.nudNumMinas.TabIndex = 2;
            // 
            // lblNumberOfMines
            // 
            this.lblNumberOfMines.AutoSize = true;
            this.lblNumberOfMines.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfMines.Location = new System.Drawing.Point(8, 10);
            this.lblNumberOfMines.Name = "lblNumberOfMines";
            this.lblNumberOfMines.Size = new System.Drawing.Size(154, 20);
            this.lblNumberOfMines.TabIndex = 3;
            this.lblNumberOfMines.Text = "Number of mines?";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(523, 563);
            this.Controls.Add(this.lblNumberOfMines);
            this.Controls.Add(this.nudNumMinas);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MineSweeper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNumMinas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.NumericUpDown nudNumMinas;
        private System.Windows.Forms.Label lblNumberOfMines;
    }
}

