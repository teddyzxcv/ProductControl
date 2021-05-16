using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProductControl.ClientForm;
using ProductControl.ClientSide;
using System.Linq;

namespace ProductControl
{
    public partial class LoginRegistrationForm : Form
    {
        public LoginRegistrationForm()
        {
            InitializeComponent();
            Client.AllClients = XmlSerialzation.DeserializationClient();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrationForm rg = new RegistrationForm();
            if (rg.ShowDialog() == DialogResult.OK)
                MessageBox.Show("User create successful", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string login = this.textBox1.Text;
                string password = this.textBox2.Text;

                if (Client.AllClients.Select(e => e.Email).Contains(login))
                {
                    if (Client.AllClients.Where(e => e.Email == login).First().Passoword == password)
                    {
                        var user = Client.AllClients.Where(e => e.Email == login).First();
                        if (user.IsAdmin)
                        {
                            this.DialogResult = DialogResult.Yes;
                            Form1.CurrentClient = Client.AllClients.Where(e => e.Email == login).First();
                            this.Close();
                            return;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            Form1.CurrentClient = Client.AllClients.Where(e => e.Email == login).First();
                            return;
                        }
                    }
                    else
                        throw new ArgumentException("Wrong password!");
                }
                else
                {
                    throw new ArgumentException("User doesnt exist!");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void LoginRegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBox2.PasswordChar = '\0';
            }
            else
            {
                this.textBox2.PasswordChar = '*';
            }
        }
    }
}
