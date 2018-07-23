namespace Monopoly.Main
{
    partial class MainMenu
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
            this.newGameButton = new System.Windows.Forms.Button();
            this.loadGameButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.BackColor = System.Drawing.Color.SteelBlue;
            this.newGameButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.newGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.newGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.newGameButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.newGameButton.Location = new System.Drawing.Point(60, 60);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(200, 70);
            this.newGameButton.TabIndex = 0;
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = false;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // loadGameButton
            // 
            this.loadGameButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.loadGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.loadGameButton.Location = new System.Drawing.Point(320, 60);
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(200, 70);
            this.loadGameButton.TabIndex = 1;
            this.loadGameButton.Text = "Load Game";
            this.loadGameButton.UseVisualStyleBackColor = true;
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.settingsButton.Location = new System.Drawing.Point(60, 190);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(200, 70);
            this.settingsButton.TabIndex = 2;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.exitButton.Location = new System.Drawing.Point(320, 190);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(200, 70);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(584, 321);
            this.ControlBox = false;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.loadGameButton);
            this.Controls.Add(this.newGameButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.ShowInTaskbar = false;
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Button loadGameButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button exitButton;
    }
}