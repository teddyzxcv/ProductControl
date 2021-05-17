using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace ProductControl.ClientForm
{
    public partial class AllClientsForm : Form
    {
        public static double RankPrice = 0;
        static List<Client> Clients = new List<Client>();
        public static DateTime Date = DateTime.Now;
        public static string CurrentArticle;
        public AllClientsForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// All Client load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllClientsForm_Load(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listView1.Clear();
            Clients = Client.AllClients.Where(e => !e.IsAdmin).ToList();
            if (Clients.Count != 0)
            {
                this.listBox1.DataSource = Clients.Select(e => e.Name).ToList();
                this.listBox1.SelectedIndex = 0;
            }

        }
        /// <summary>
        /// Refresh list after select.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshListView();
        }
        /// <summary>
        /// Refresh listview.
        /// </summary>
        void RefreshListView()
        {
            this.listView1.Clear();
            this.listView1.Columns.Add("Order Index");
            this.listView1.Columns.Add("Client email");
            this.listView1.Columns.Add("Create Time");
            this.listView1.Columns.Add("All Price");
            this.listView1.Columns.Add("Status");
            foreach (var item in Clients[this.listBox1.SelectedIndex].Orders)
            {
                var row = new string[] { item.Index.ToString(), item.OrderClient.Email, item.CreateTime.ToString(), item.Products.Select(e => e.Price).Sum().ToString(), item.Status.ToString() };
                var lvi = new ListViewItem(row);
                this.listView1.Items.Add(lvi);
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        /// <summary>
        /// Call back refresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void callbackStripMenuItem1_Click(object sender, EventArgs e)
        {
            CallBackForm cbf = new CallBackForm();
            if (cbf.ShowDialog() == DialogResult.Yes)
            {
                this.listView1.Clear();
                Clients = Clients.Where(e => e.Orders.Exists(e => e.CreateTime.Date == Date.Date)).ToList();
                Clients = Client.AllClients.Where(e => e.Orders.Where(e => e.CreateTime.Date == Date.Date).ToList().Exists(e => e.Products.Exists(e => e.Article == CurrentArticle))).ToList();
                if (Clients.Count != 0)
                {
                    this.listBox1.DataSource = Clients.Select(e => e.Name).ToList();
                    this.listBox1.SelectedIndex = 0;
                    RefreshListView();
                }
                else
                {
                    this.listBox1.DataSource = new List<string>();
                }
            }
        }
        /// <summary>
        /// Pay order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderpayedStripMenuItem1_Click(object sender, EventArgs e)
        {
            RankForm rf = new RankForm();
            if (rf.ShowDialog() == DialogResult.Yes)
            {
                this.listView1.Clear();
                Clients = Client.AllClients.Where(e => !e.IsAdmin).Where(e => e.AllPriceAmount > RankPrice).ToList();
                if (Clients.Count != 0)
                {
                    this.listBox1.DataSource = Clients.OrderByDescending(e => e.AllPriceAmount).Select(e => e.Name).ToList();
                    Clients = Clients.OrderByDescending(e => e.AllPriceAmount).ToList();
                    this.listBox1.SelectedIndex = 0;
                    RefreshListView();
                }
                else
                {
                    this.listBox1.DataSource = new List<string>();
                }

            }
        }
    }
}
