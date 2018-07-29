using System.Drawing;

namespace Monopoly.Main
{
    partial class Monopoly
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.drawArea = new System.Windows.Forms.PictureBox();
            this.propertyCardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gameButton1 = new System.Windows.Forms.Button();
            this.gameButton2 = new System.Windows.Forms.Button();
            this.gameButton3 = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.Label();
            this.mainMenuButton = new System.Windows.Forms.Button();
            this.detailBox = new System.Windows.Forms.PictureBox();
            this.playerDetail = new System.Windows.Forms.PictureBox();
            this.playerMoneyLabel = new System.Windows.Forms.Label();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.tradeViewer = new System.Windows.Forms.ListView();
            this.Property = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mortgage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mortgaged = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Owner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player1 = new System.Windows.Forms.PictureBox();
            this.player2 = new System.Windows.Forms.PictureBox();
            this.player3 = new System.Windows.Forms.PictureBox();
            this.player4 = new System.Windows.Forms.PictureBox();
            this.player5 = new System.Windows.Forms.PictureBox();
            this.player6 = new System.Windows.Forms.PictureBox();
            this.diceLabel = new System.Windows.Forms.Label();
            this.Card = new System.Windows.Forms.PictureBox();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.CardTitle = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.paymentLabel1 = new System.Windows.Forms.Label();
            this.paymentLabel2 = new System.Windows.Forms.Label();
            this.paymentLabel3 = new System.Windows.Forms.Label();
            this.paymentLabel4 = new System.Windows.Forms.Label();
            this.mortgageLabel = new System.Windows.Forms.Label();
            this.apartmentCostLabel = new System.Windows.Forms.Label();
            this.cardNameLabel = new System.Windows.Forms.Label();
            this.cardContentLabel = new System.Windows.Forms.Label();
            this.moneyTrackBar = new System.Windows.Forms.TrackBar();
            this.trackBarShower = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyCardBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerDetail)).BeginInit();
            this.playerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moneyTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(400, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 0);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // drawArea
            // 
            this.drawArea.Location = new System.Drawing.Point(424, 0);
            this.drawArea.Name = "drawArea";
            this.drawArea.Size = new System.Drawing.Size(760, 760);
            this.drawArea.TabIndex = 1;
            this.drawArea.TabStop = false;
            this.drawArea.Click += new System.EventHandler(this.drawArea_Click);
            // 
            // gameButton1
            // 
            this.gameButton1.BackColor = System.Drawing.Color.IndianRed;
            this.gameButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gameButton1.FlatAppearance.BorderSize = 0;
            this.gameButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.gameButton1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gameButton1.Location = new System.Drawing.Point(20, 270);
            this.gameButton1.Name = "gameButton1";
            this.gameButton1.Size = new System.Drawing.Size(115, 50);
            this.gameButton1.TabIndex = 3;
            this.gameButton1.Text = "gameButton1";
            this.gameButton1.UseVisualStyleBackColor = false;
            this.gameButton1.Click += new System.EventHandler(this.gameButton1_click);
            // 
            // gameButton2
            // 
            this.gameButton2.BackColor = System.Drawing.Color.IndianRed;
            this.gameButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gameButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.gameButton2.ForeColor = System.Drawing.SystemColors.Control;
            this.gameButton2.Location = new System.Drawing.Point(155, 270);
            this.gameButton2.Name = "gameButton2";
            this.gameButton2.Size = new System.Drawing.Size(115, 50);
            this.gameButton2.TabIndex = 4;
            this.gameButton2.Text = "gameButton2";
            this.gameButton2.UseVisualStyleBackColor = false;
            this.gameButton2.Click += new System.EventHandler(this.gameButton2_Click);
            // 
            // gameButton3
            // 
            this.gameButton3.BackColor = System.Drawing.Color.IndianRed;
            this.gameButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.gameButton3.ForeColor = System.Drawing.SystemColors.Control;
            this.gameButton3.Location = new System.Drawing.Point(290, 270);
            this.gameButton3.Name = "gameButton3";
            this.gameButton3.Size = new System.Drawing.Size(115, 50);
            this.gameButton3.TabIndex = 5;
            this.gameButton3.Text = "gameButton3";
            this.gameButton3.UseVisualStyleBackColor = false;
            this.gameButton3.Click += new System.EventHandler(this.gameButton3_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Gainsboro;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.exitButton.Location = new System.Drawing.Point(230, 690);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(175, 50);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox.BackColor = System.Drawing.Color.Gray;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.textBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox.Location = new System.Drawing.Point(20, 200);
            this.textBox.Margin = new System.Windows.Forms.Padding(0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(385, 50);
            this.textBox.TabIndex = 8;
            this.textBox.Text = "textBox";
            this.textBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textBox.Click += new System.EventHandler(this.textBox_Click);
            // 
            // mainMenuButton
            // 
            this.mainMenuButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mainMenuButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mainMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainMenuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.mainMenuButton.Location = new System.Drawing.Point(20, 690);
            this.mainMenuButton.Name = "mainMenuButton";
            this.mainMenuButton.Size = new System.Drawing.Size(175, 50);
            this.mainMenuButton.TabIndex = 9;
            this.mainMenuButton.Text = "Main Menu";
            this.mainMenuButton.UseVisualStyleBackColor = false;
            this.mainMenuButton.Click += new System.EventHandler(this.mainMenuButton_Click);
            // 
            // detailBox
            // 
            this.detailBox.Location = new System.Drawing.Point(20, 560);
            this.detailBox.Name = "detailBox";
            this.detailBox.Size = new System.Drawing.Size(385, 115);
            this.detailBox.TabIndex = 12;
            this.detailBox.TabStop = false;
            // 
            // playerDetail
            // 
            this.playerDetail.BackColor = System.Drawing.Color.White;
            this.playerDetail.Location = new System.Drawing.Point(-14, 0);
            this.playerDetail.Name = "playerDetail";
            this.playerDetail.Size = new System.Drawing.Size(424, 177);
            this.playerDetail.TabIndex = 13;
            this.playerDetail.TabStop = false;
            // 
            // playerMoneyLabel
            // 
            this.playerMoneyLabel.BackColor = System.Drawing.Color.Gold;
            this.playerMoneyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.playerMoneyLabel.Location = new System.Drawing.Point(0, 90);
            this.playerMoneyLabel.Name = "playerMoneyLabel";
            this.playerMoneyLabel.Size = new System.Drawing.Size(424, 90);
            this.playerMoneyLabel.TabIndex = 15;
            this.playerMoneyLabel.Text = "playerMoneyLabel";
            this.playerMoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerPanel
            // 
            this.playerPanel.Controls.Add(this.playerNameLabel);
            this.playerPanel.Controls.Add(this.playerMoneyLabel);
            this.playerPanel.Controls.Add(this.playerDetail);
            this.playerPanel.Location = new System.Drawing.Point(0, 0);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(424, 180);
            this.playerPanel.TabIndex = 7;
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.BackColor = System.Drawing.Color.Goldenrod;
            this.playerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.playerNameLabel.Location = new System.Drawing.Point(0, 0);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(424, 90);
            this.playerNameLabel.TabIndex = 16;
            this.playerNameLabel.Text = "playerNameLabel";
            this.playerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playerNameLabel.Click += new System.EventHandler(this.playerNameLabel_Click);
            // 
            // tradeViewer
            // 
            this.tradeViewer.BackColor = System.Drawing.SystemColors.Window;
            this.tradeViewer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Property,
            this.Cost,
            this.Mortgage,
            this.Mortgaged,
            this.Group,
            this.Owner});
            this.tradeViewer.FullRowSelect = true;
            this.tradeViewer.GridLines = true;
            this.tradeViewer.Location = new System.Drawing.Point(20, 340);
            this.tradeViewer.MultiSelect = false;
            this.tradeViewer.Name = "tradeViewer";
            this.tradeViewer.Size = new System.Drawing.Size(385, 150);
            this.tradeViewer.TabIndex = 1;
            this.tradeViewer.UseCompatibleStateImageBehavior = false;
            this.tradeViewer.View = System.Windows.Forms.View.Details;
            // 
            // Property
            // 
            this.Property.Text = " Property";
            this.Property.Width = 83;
            // 
            // Cost
            // 
            this.Cost.Text = "Cost";
            this.Cost.Width = 37;
            // 
            // Mortgage
            // 
            this.Mortgage.Text = "Mortgage";
            this.Mortgage.Width = 62;
            // 
            // Mortgaged
            // 
            this.Mortgaged.Text = "Mortgaged?";
            this.Mortgaged.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Mortgaged.Width = 69;
            // 
            // Group
            // 
            this.Group.DisplayIndex = 5;
            this.Group.Text = "Group";
            this.Group.Width = 43;
            // 
            // Owner
            // 
            this.Owner.DisplayIndex = 4;
            this.Owner.Text = "Owner";
            this.Owner.Width = 86;
            // 
            // player1
            // 
            this.player1.BackColor = System.Drawing.Color.Transparent;
            this.player1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.player1.Location = new System.Drawing.Point(461, 29);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(20, 20);
            this.player1.TabIndex = 14;
            this.player1.TabStop = false;
            // 
            // player2
            // 
            this.player2.Location = new System.Drawing.Point(483, 29);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(20, 20);
            this.player2.TabIndex = 15;
            this.player2.TabStop = false;
            // 
            // player3
            // 
            this.player3.Location = new System.Drawing.Point(461, 55);
            this.player3.Name = "player3";
            this.player3.Size = new System.Drawing.Size(20, 20);
            this.player3.TabIndex = 16;
            this.player3.TabStop = false;
            // 
            // player4
            // 
            this.player4.Location = new System.Drawing.Point(483, 55);
            this.player4.Name = "player4";
            this.player4.Size = new System.Drawing.Size(20, 20);
            this.player4.TabIndex = 17;
            this.player4.TabStop = false;
            // 
            // player5
            // 
            this.player5.Location = new System.Drawing.Point(461, 81);
            this.player5.Name = "player5";
            this.player5.Size = new System.Drawing.Size(20, 20);
            this.player5.TabIndex = 18;
            this.player5.TabStop = false;
            // 
            // player6
            // 
            this.player6.Location = new System.Drawing.Point(483, 81);
            this.player6.Name = "player6";
            this.player6.Size = new System.Drawing.Size(20, 20);
            this.player6.TabIndex = 19;
            this.player6.TabStop = false;
            // 
            // diceLabel
            // 
            this.diceLabel.BackColor = System.Drawing.Color.Firebrick;
            this.diceLabel.Enabled = false;
            this.diceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F);
            this.diceLabel.ForeColor = System.Drawing.Color.White;
            this.diceLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.diceLabel.Location = new System.Drawing.Point(754, 230);
            this.diceLabel.Name = "diceLabel";
            this.diceLabel.Size = new System.Drawing.Size(100, 100);
            this.diceLabel.TabIndex = 21;
            this.diceLabel.Text = "1";
            this.diceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Card
            // 
            this.Card.BackColor = System.Drawing.Color.Black;
            this.Card.Location = new System.Drawing.Point(654, 180);
            this.Card.Name = "Card";
            this.Card.Size = new System.Drawing.Size(300, 400);
            this.Card.TabIndex = 22;
            this.Card.TabStop = false;
            // 
            // logoBox
            // 
            this.logoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.logoBox.Location = new System.Drawing.Point(655, 230);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(298, 140);
            this.logoBox.TabIndex = 28;
            this.logoBox.TabStop = false;
            this.logoBox.Click += new System.EventHandler(this.logoBox_Click);
            // 
            // CardTitle
            // 
            this.CardTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.CardTitle.Location = new System.Drawing.Point(655, 181);
            this.CardTitle.Name = "CardTitle";
            this.CardTitle.Size = new System.Drawing.Size(298, 48);
            this.CardTitle.TabIndex = 29;
            this.CardTitle.Text = "cardTitle";
            this.CardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // priceLabel
            // 
            this.priceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.priceLabel.Location = new System.Drawing.Point(655, 371);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(298, 29);
            this.priceLabel.TabIndex = 31;
            this.priceLabel.Text = "priceLabel";
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // paymentLabel1
            // 
            this.paymentLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.paymentLabel1.Location = new System.Drawing.Point(655, 400);
            this.paymentLabel1.Name = "paymentLabel1";
            this.paymentLabel1.Size = new System.Drawing.Size(298, 30);
            this.paymentLabel1.TabIndex = 32;
            this.paymentLabel1.Text = "paymentLabel1";
            this.paymentLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // paymentLabel2
            // 
            this.paymentLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.paymentLabel2.Location = new System.Drawing.Point(655, 430);
            this.paymentLabel2.Name = "paymentLabel2";
            this.paymentLabel2.Size = new System.Drawing.Size(298, 30);
            this.paymentLabel2.TabIndex = 33;
            this.paymentLabel2.Text = "paymentLabel2";
            this.paymentLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // paymentLabel3
            // 
            this.paymentLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.paymentLabel3.Location = new System.Drawing.Point(655, 460);
            this.paymentLabel3.Name = "paymentLabel3";
            this.paymentLabel3.Size = new System.Drawing.Size(298, 30);
            this.paymentLabel3.TabIndex = 34;
            this.paymentLabel3.Text = "paymentLabel3";
            this.paymentLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // paymentLabel4
            // 
            this.paymentLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.paymentLabel4.Location = new System.Drawing.Point(655, 490);
            this.paymentLabel4.Name = "paymentLabel4";
            this.paymentLabel4.Size = new System.Drawing.Size(298, 30);
            this.paymentLabel4.TabIndex = 35;
            this.paymentLabel4.Text = "paymentLabel4";
            this.paymentLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mortgageLabel
            // 
            this.mortgageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.mortgageLabel.Location = new System.Drawing.Point(655, 550);
            this.mortgageLabel.Name = "mortgageLabel";
            this.mortgageLabel.Size = new System.Drawing.Size(298, 29);
            this.mortgageLabel.TabIndex = 36;
            this.mortgageLabel.Text = "mortgageLabel";
            this.mortgageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // apartmentCostLabel
            // 
            this.apartmentCostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.apartmentCostLabel.Location = new System.Drawing.Point(655, 520);
            this.apartmentCostLabel.Name = "apartmentCostLabel";
            this.apartmentCostLabel.Size = new System.Drawing.Size(298, 30);
            this.apartmentCostLabel.TabIndex = 37;
            this.apartmentCostLabel.Text = "apartmentCostLabel";
            this.apartmentCostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardNameLabel
            // 
            this.cardNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cardNameLabel.Location = new System.Drawing.Point(20, 560);
            this.cardNameLabel.Name = "cardNameLabel";
            this.cardNameLabel.Size = new System.Drawing.Size(385, 28);
            this.cardNameLabel.TabIndex = 38;
            this.cardNameLabel.Text = "cardNameLabel";
            this.cardNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cardNameLabel.Click += new System.EventHandler(this.cardNameLabel_Click);
            // 
            // cardContentLabel
            // 
            this.cardContentLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cardContentLabel.Location = new System.Drawing.Point(20, 587);
            this.cardContentLabel.Name = "cardContentLabel";
            this.cardContentLabel.Size = new System.Drawing.Size(385, 89);
            this.cardContentLabel.TabIndex = 39;
            this.cardContentLabel.Text = "cardContentLabel";
            this.cardContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // moneyTrackBar
            // 
            this.moneyTrackBar.AutoSize = false;
            this.moneyTrackBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.moneyTrackBar.LargeChange = 1;
            this.moneyTrackBar.Location = new System.Drawing.Point(20, 495);
            this.moneyTrackBar.Name = "moneyTrackBar";
            this.moneyTrackBar.Size = new System.Drawing.Size(345, 45);
            this.moneyTrackBar.TabIndex = 2;
            this.moneyTrackBar.ValueChanged += new System.EventHandler(this.MoneyTrackBarChanged);
            // 
            // trackBarShower
            // 
            this.trackBarShower.Location = new System.Drawing.Point(345, 495);
            this.trackBarShower.Name = "trackBarShower";
            this.trackBarShower.Size = new System.Drawing.Size(60, 30);
            this.trackBarShower.TabIndex = 41;
            this.trackBarShower.Text = "0.2";
            this.trackBarShower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Monopoly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.ControlBox = false;
            this.Controls.Add(this.trackBarShower);
            this.Controls.Add(this.moneyTrackBar);
            this.Controls.Add(this.cardContentLabel);
            this.Controls.Add(this.cardNameLabel);
            this.Controls.Add(this.apartmentCostLabel);
            this.Controls.Add(this.mortgageLabel);
            this.Controls.Add(this.paymentLabel4);
            this.Controls.Add(this.paymentLabel3);
            this.Controls.Add(this.paymentLabel2);
            this.Controls.Add(this.paymentLabel1);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.CardTitle);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.Card);
            this.Controls.Add(this.diceLabel);
            this.Controls.Add(this.player6);
            this.Controls.Add(this.player5);
            this.Controls.Add(this.player4);
            this.Controls.Add(this.player3);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.tradeViewer);
            this.Controls.Add(this.detailBox);
            this.Controls.Add(this.mainMenuButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.gameButton3);
            this.Controls.Add(this.gameButton2);
            this.Controls.Add(this.gameButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playerPanel);
            this.Controls.Add(this.drawArea);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Monopoly";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Monopoly v1.0";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintWindowMethod);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WindowKeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyCardBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerDetail)).EndInit();
            this.playerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.player1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moneyTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox drawArea;
        private System.Windows.Forms.Label textBox;
        private System.Windows.Forms.PictureBox detailBox;
        private System.Windows.Forms.BindingSource propertyCardBindingSource;
        private System.Windows.Forms.PictureBox playerDetail;
        public System.Windows.Forms.Label playerMoneyLabel;
        private System.Windows.Forms.Panel playerPanel;
        public System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.ListView tradeViewer;
        private System.Windows.Forms.ColumnHeader Property;
        private System.Windows.Forms.ColumnHeader Cost;
        private System.Windows.Forms.ColumnHeader Mortgage;
        private System.Windows.Forms.ColumnHeader Owner;
        private System.Windows.Forms.PictureBox player1;
        private System.Windows.Forms.PictureBox player2;
        private System.Windows.Forms.PictureBox player3;
        private System.Windows.Forms.PictureBox player4;
        private System.Windows.Forms.PictureBox player5;
        private System.Windows.Forms.PictureBox player6;
        private System.Windows.Forms.Label diceLabel;
        private System.Windows.Forms.PictureBox Card;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label CardTitle;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label paymentLabel1;
        private System.Windows.Forms.Label paymentLabel2;
        private System.Windows.Forms.Label paymentLabel3;
        private System.Windows.Forms.Label paymentLabel4;
        private System.Windows.Forms.Label mortgageLabel;
        private System.Windows.Forms.Label apartmentCostLabel;
        private System.Windows.Forms.Label cardNameLabel;
        private System.Windows.Forms.Label cardContentLabel;
        private System.Windows.Forms.TrackBar moneyTrackBar;
        private System.Windows.Forms.ColumnHeader Group;
        private System.Windows.Forms.Label trackBarShower;
        private System.Windows.Forms.ColumnHeader Mortgaged;
        public System.Windows.Forms.Button gameButton1;
        public System.Windows.Forms.Button gameButton2;
        public System.Windows.Forms.Button gameButton3;
        public System.Windows.Forms.Button exitButton;
        public System.Windows.Forms.Button mainMenuButton;
    }
#endregion
}