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
    public partial class Productview : Form
    {
        public string Product_Name;
        public string Article;

        public int Remaining;

        public int Price;

        public string Description;

        public string PathToPic;
        public Productview(string name, string article, int remaining, int price, string description, string pathtopic)
        {
            Product_Name = name;
            Article = article;
            Remaining = remaining;
            Price = price;
            Description = description;
            PathToPic = pathtopic;
            InitializeComponent();
        }

        private void Productview_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Product_Name;
            this.textBox2.Text = Article;
            this.numericUpDown1.Value = Price;
            this.numericUpDown2.Value = Remaining;
            this.textBox3.Text = Description;
            this.pictureBox1.AutoSize = false;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.ImageLocation = PathToPic;
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();

        }
        /// <summary>
        /// Change picture.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.ShowDialog();
            PathToPic = Path.Combine(saveStructure.PathToSavePic, Directory.GetFiles(saveStructure.PathToSavePic).Length.ToString() + "." + Path.GetExtension(ofd.FileName));
            File.Copy(ofd.FileName, PathToPic, true);
            this.pictureBox1.Image = Image.FromFile(ofd.FileName);
            this.pictureBox1.AutoSize = false;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
