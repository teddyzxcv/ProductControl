using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ProductControl
{
    public partial class OrderForm : Form
    {
        bool IsAdmin;
        Client CurrentClient;

        List<Order> CurrentListOrder = Order.AllOrder;
        public OrderForm(bool isadmin, Client client)
        {
            InitializeComponent();
            IsAdmin = isadmin;
            CurrentClient = client;
            if (!IsAdmin)
                this.changetableStripMenuItem1.Visible = false;
        }
        /// <summary>
        /// Initiazation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderForm_Load(object sender, EventArgs e)
        {
            if (CurrentClient.IsAdmin)
            {
                this.payStripMenuItem1.Visible = false;
                this.changeStripMenuItem1.Visible = true;
            }
            else
            {
                this.changeStripMenuItem1.Visible = false;
                this.payStripMenuItem1.Visible = true;
            }
            RefreshList();
        }
        /// <summary>
        /// Refresh order list.
        /// </summary>
        private void RefreshList()
        {
            this.listView1.Clear();
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            if (IsAdmin)
            {
                this.listView1.Columns.Add("Order Index");
                this.listView1.Columns.Add("Client email");
                this.listView1.Columns.Add("Create Time");
                this.listView1.Columns.Add("All Price");
                this.listView1.Columns.Add("Status");
                foreach (var item in CurrentListOrder)
                {
                    var row = new string[] { item.Index.ToString(), item.OrderClient.Email, item.CreateTime.ToString(), item.Products.Select(e => e.Price).Sum().ToString(), item.Status.ToString() };
                    var lvi = new ListViewItem(row);
                    this.listView1.Items.Add(lvi);
                }
            }
            else
            {
                this.listView1.Columns.Add("Order Index");
                this.listView1.Columns.Add("Create Time");
                this.listView1.Columns.Add("All Price");
                this.listView1.Columns.Add("Status");
                foreach (var item in CurrentClient.Orders)
                {
                    var row = new string[] { item.Index.ToString(), item.CreateTime.ToString(), item.Products.Select(e => e.Price).Sum().ToString(), item.Status.ToString() };
                    var lvi = new ListViewItem(row);
                    this.listView1.Items.Add(lvi);
                }
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        /// <summary>
        /// Payed status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void payStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;
            else
            {
                string index = this.listView1.SelectedItems[0].SubItems[0].Text;
                if (CurrentClient.Orders.Where(e => e.Index.ToString() == index).First().Status.HasFlag(Order.OrderStatus.None))
                    CurrentClient.Orders.Where(e => e.Index.ToString() == index).First().Status &= ~Order.OrderStatus.None;
                if (!CurrentClient.Orders.Where(e => e.Index.ToString() == index).First().Status.HasFlag(Order.OrderStatus.Payed))
                    CurrentClient.Orders.Where(e => e.Index.ToString() == index).First().Status |= Order.OrderStatus.Payed;
                RefreshList();
            }
        }
        /// <summary>
        /// Processed status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void processedStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;
            else
            {
                string index = this.listView1.SelectedItems[0].SubItems[0].Text;
                var currentorder = Order.AllOrder.Where(e => e.Index.ToString() == index).First();
                if (currentorder.Status.HasFlag(Order.OrderStatus.Executed))
                    currentorder.Status &= ~Order.OrderStatus.Executed;
                if (currentorder.Status.HasFlag(Order.OrderStatus.Shipped))
                    currentorder.Status &= ~Order.OrderStatus.Shipped;
                if (currentorder.Status.HasFlag(Order.OrderStatus.None))
                    currentorder.Status &= ~Order.OrderStatus.None;
                if (!currentorder.Status.HasFlag(Order.OrderStatus.Processed))
                    currentorder.Status |= Order.OrderStatus.Processed;
                RefreshList();
            }
        }
        /// <summary>
        /// Shiped status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shipedStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;
            else
            {
                string index = this.listView1.SelectedItems[0].SubItems[0].Text;
                var currentorder = Order.AllOrder.Where(e => e.Index.ToString() == index).First();
                if (currentorder.Status.HasFlag(Order.OrderStatus.Executed))
                    currentorder.Status &= ~Order.OrderStatus.Executed;
                if (currentorder.Status.HasFlag(Order.OrderStatus.Processed))
                    currentorder.Status &= ~Order.OrderStatus.Processed;
                if (currentorder.Status.HasFlag(Order.OrderStatus.None))
                    currentorder.Status &= ~Order.OrderStatus.None;
                if (!currentorder.Status.HasFlag(Order.OrderStatus.Shipped))
                    currentorder.Status |= Order.OrderStatus.Shipped;
                RefreshList();
            }
        }
        /// <summary>
        /// Execute status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executedStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;
            else
            {
                string index = this.listView1.SelectedItems[0].SubItems[0].Text;
                var currentorder = Order.AllOrder.Where(e => e.Index.ToString() == index).First();
                if (currentorder.Status.HasFlag(Order.OrderStatus.Processed))
                    currentorder.Status &= ~Order.OrderStatus.Processed;
                if (currentorder.Status.HasFlag(Order.OrderStatus.Shipped))
                    currentorder.Status &= ~Order.OrderStatus.Shipped;
                if (currentorder.Status.HasFlag(Order.OrderStatus.None))
                    currentorder.Status &= ~Order.OrderStatus.None;
                if (!currentorder.Status.HasFlag(Order.OrderStatus.Executed))
                    currentorder.Status |= Order.OrderStatus.Executed;
                RefreshList();
            }
        }
        /// <summary>
        /// Change table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changetableStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.changetableStripMenuItem1.Text == "Show Active Orders")
            {
                this.changetableStripMenuItem1.Text = "Show All Orders";
                CurrentListOrder = CurrentListOrder.Where(e => !e.Status.HasFlag(Order.OrderStatus.Executed)).ToList();
            }
            else
            {
                this.changetableStripMenuItem1.Text = "Show Active Orders";
                CurrentListOrder = Order.AllOrder;
            }
            RefreshList();
        }
    }
}

