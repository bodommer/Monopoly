namespace Monopoly.Forms
{
    partial class NewGameDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.moneyTrackBar = new System.Windows.Forms.TrackBar();
            this.nextButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.playerCount = new System.Windows.Forms.NumericUpDown();
            this.startingMoneyLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.moneyTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Starting money (millions):";
            // 
            // moneyTrackBar
            // 
            this.moneyTrackBar.LargeChange = 20;
            this.moneyTrackBar.Location = new System.Drawing.Point(15, 104);
            this.moneyTrackBar.Maximum = 500;
            this.moneyTrackBar.Minimum = 250;
            this.moneyTrackBar.Name = "moneyTrackBar";
            this.moneyTrackBar.Size = new System.Drawing.Size(243, 45);
            this.moneyTrackBar.SmallChange = 10;
            this.moneyTrackBar.TabIndex = 4;
            this.moneyTrackBar.TickFrequency = 10;
            this.moneyTrackBar.Value = 250;
            this.moneyTrackBar.ValueChanged += new System.EventHandler(this.trackBarChanged);
            // 
            // nextButton
            // 
            this.nextButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.nextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.nextButton.Location = new System.Drawing.Point(165, 190);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(100, 50);
            this.nextButton.TabIndex = 5;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.AllowDrop = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cancelButton.Location = new System.Drawing.Point(15, 190);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // playerCount
            // 
            this.playerCount.Location = new System.Drawing.Point(155, 31);
            this.playerCount.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.playerCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playerCount.Name = "playerCount";
            this.playerCount.Size = new System.Drawing.Size(103, 20);
            this.playerCount.TabIndex = 7;
            this.playerCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // startingMoneyLabel
            // 
            this.startingMoneyLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startingMoneyLabel.Location = new System.Drawing.Point(152, 84);
            this.startingMoneyLabel.Name = "startingMoneyLabel";
            this.startingMoneyLabel.Size = new System.Drawing.Size(50, 20);
            this.startingMoneyLabel.TabIndex = 8;
            this.startingMoneyLabel.Text = "250 m";
            this.startingMoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewGameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.startingMoneyLabel);
            this.Controls.Add(this.playerCount);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.moneyTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Game Settings";
            ((System.ComponentModel.ISupportInitialize)(this.moneyTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar moneyTrackBar;
        private System.Windows.Forms.Button nextButton;
        public System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown playerCount;
        private System.Windows.Forms.Label startingMoneyLabel;
    }
}