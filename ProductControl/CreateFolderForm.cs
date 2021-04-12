using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductControl
{
    public partial class CreateFolderForm : Form
    {
        public string FolderName = "";
        public CreateFolderForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Save new project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Give him a name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            FolderName = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CreatePrjForm_Load(object sender, EventArgs e)
        {

        }
    }
}
