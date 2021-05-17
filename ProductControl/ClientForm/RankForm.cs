using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductControl.ClientForm
{
    public partial class RankForm : Form
    {
        public RankForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Input data for order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.numericUpDown1.Value >= 0)
                {
                    AllClientsForm.RankPrice = (double)this.numericUpDown1.Value;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                    return;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.No;
                this.Close();
                return;
            }
        }


    }
}
