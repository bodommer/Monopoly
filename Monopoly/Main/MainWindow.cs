using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly.Main
{
    public partial class Monopoly : Form
    {

        public Monopoly()
        {
            InitializeComponent();
            this.Height = 1000;
            this.Width = 1000;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void PaintWindowMethod(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);
            Brush myBrush = new SolidBrush(Color.Red);

            g.DrawLine(pen, 2, 2, 4, 1);

        }
    }
}
