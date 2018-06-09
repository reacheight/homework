using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clocks
{
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
        }

        private void timerTick(object sender, EventArgs e)
        {
            label.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
