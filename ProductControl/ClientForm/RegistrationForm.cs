using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProductControl.ClientSide;

namespace ProductControl.ClientForm
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            this.textBox4.PasswordChar = '*';
            this.textBox5.PasswordChar = '*';
        }
        /// <summary>
        /// Input registration data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            string number = this.textBox2.Text;
            string email = this.textBox3.Text;
            string password1 = this.textBox4.Text;
            string password2 = this.textBox5.Text;
            try
            {
                if (name == string.Empty || number == string.Empty || email == string.Empty || password1 == string.Empty || password2 == string.Empty)
                    throw new ArgumentException("You must input something in every textbox!");
                if (password1 != password2)
                    throw new ArgumentException("Password must match the comfirm password!");
                Client client = new Client();
                if (this.checkBox2.Checked)
                    client = new Client(name, number, email, password1, true);
                else
                    client = new Client(name, number, email, password1, false);
                if (Client.AllClients.Contains(client))
                    throw new ArgumentException("User with this email is already exist!");
                Client.AllClients.Add(client);
                XmlSerialzation.ClientSerialzation();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBox4.PasswordChar = '\0';
                this.textBox5.PasswordChar = '\0';

            }
            else
            {
                this.textBox4.PasswordChar = '*';
                this.textBox5.PasswordChar = '*';
            }
        }
    }
}
