﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ProductControl.ClientForm
{
    public partial class CartForm : Form
    {
        Client CurrentClient;
        public CartForm(Client client)
        {
            this.CurrentClient = client;
            InitializeComponent();
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            this.listView1.Clear();
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Columns.Add("Article");
            this.listView1.Columns.Add("Name");
            this.listView1.Columns.Add("Price");
            this.listView1.Columns.Add("Amount");
            foreach (var item in CurrentClient.Cart.Distinct())
            {
                var row = new string[] { item.Article, item.Name, item.Price.ToString(), CurrentClient.Cart.Where(e => e.FullPath == item.FullPath && e.Article == item.Article).Count().ToString() };
                var lvi = new ListViewItem(row);
                this.listView1.Items.Add(lvi);
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void deleteStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;
            else
            {
                string article = this.listView1.SelectedItems[0].SubItems[0].Text;
                string name = this.listView1.SelectedItems[0].SubItems[1].Text;
                CurrentClient.Cart.RemoveAll(e => e.Article == article && e.Name == name);
                RefreshList();
            }
        }
    }
}
