using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly.Forms
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();

        }

        private void closeButton_Click(object sender, EventArgs e)
        {

        }

        private void Form_Load(object sender, EventArgs e)
        {
            richTextBox1.Rtf = Monopoly.Properties.Resources.User_Documentation;
            richTextBox1.Update();
        }
    }
}
