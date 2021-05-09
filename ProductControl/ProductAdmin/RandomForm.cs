using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductControl
{
    public partial class RandomForm : Form
    {
        public int nFolder;
        public int nProduct;

        public int nLevel;


        public RandomForm()
        {
            InitializeComponent();
            this.label4.Text = this.trackBar1.Value.ToString();
            this.label5.Text = this.trackBar2.Value.ToString();
            this.label6.Text = this.trackBar3.Value.ToString();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label4.Text = this.trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.label5.Text = this.trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            this.label6.Text = this.trackBar3.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nFolder = this.trackBar1.Value;
            nProduct = this.trackBar2.Value;
            nLevel = this.trackBar3.Value;
            this.Close();
        }
    }
}
