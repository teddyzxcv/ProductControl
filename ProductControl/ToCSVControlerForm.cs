using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductControl
{
    public partial class ToCSVControlerForm : Form
    {
        public ToCSVControlerForm()
        {
            InitializeComponent();
        }
        public int N;

        private void ToCSVControlerForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            N = (int)this.numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
