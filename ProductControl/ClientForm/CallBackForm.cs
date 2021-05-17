using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductControl.ClientForm
{
    public partial class CallBackForm : Form
    {
        public CallBackForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                AllClientsForm.Date = this.dateTimePicker1.Value;
                AllClientsForm.CurrentArticle = this.textBox1.Text;
                this.DialogResult = DialogResult.Yes;
                this.Close();
                return;

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
