using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProductControl
{
    public partial class CreateProduct : Form
    {
        public string Product_Name;
        public string Article;
        public int Remaining;
        public int Price;
        public string Description;
        public string PathToPic;
        public CreateProduct()
        {
            InitializeComponent();
            this.pictureBox1.AutoSize = false;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.Image = Image.FromFile("default-image.png");
            PathToPic = "default-image.png";
        }
        private void label6_Click(object sender, EventArgs e)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.AutoSize = false;
        }
        /// <summary>
        /// Give new picture.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                    PathToPic = Path.Combine(saveStructure.PathToSavePic, Directory.GetFiles(saveStructure.PathToSavePic).Length.ToString() + Path.GetExtension(ofd.FileName));
                File.Copy(ofd.FileName, PathToPic, true);
                if (!File.Exists(PathToPic))
                    throw new ArgumentException("Cant find picture!");
                this.pictureBox1.Image = Image.FromFile(PathToPic);
                this.pictureBox1.AutoSize = false;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Save changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text == String.Empty || this.textBox2.Text == String.Empty || this.textBox3.Text == String.Empty)
                    throw new ArgumentException("Plz set the setting correctly!!");
                Product_Name = this.textBox1.Text;
                Article = this.textBox2.Text;
                Price = (int)this.numericUpDown1.Value;
                Remaining = (int)this.numericUpDown2.Value;
                Description = this.textBox3.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }
    }
}
