
namespace ProductControl
{
    partial class OrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.payStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.processedStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.shipedStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.executedStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.changetableStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(503, 422);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.payStripMenuItem1,
            this.changeStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // payStripMenuItem1
            // 
            this.payStripMenuItem1.Name = "payStripMenuItem1";
            this.payStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.payStripMenuItem1.Text = "Pay";
            this.payStripMenuItem1.Click += new System.EventHandler(this.payStripMenuItem1_Click);
            // 
            // changeStripMenuItem1
            // 
            this.changeStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processedStripMenuItem1,
            this.shipedStripMenuItem1,
            this.executedStripMenuItem1});
            this.changeStripMenuItem1.Name = "changeStripMenuItem1";
            this.changeStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.changeStripMenuItem1.Text = "Change status to";
            // 
            // processedStripMenuItem1
            // 
            this.processedStripMenuItem1.Name = "processedStripMenuItem1";
            this.processedStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.processedStripMenuItem1.Text = "Processed";
            this.processedStripMenuItem1.Click += new System.EventHandler(this.processedStripMenuItem1_Click);
            // 
            // shipedStripMenuItem1
            // 
            this.shipedStripMenuItem1.Name = "shipedStripMenuItem1";
            this.shipedStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.shipedStripMenuItem1.Text = "Shiped";
            this.shipedStripMenuItem1.Click += new System.EventHandler(this.shipedStripMenuItem1_Click);
            // 
            // executedStripMenuItem1
            // 
            this.executedStripMenuItem1.Name = "executedStripMenuItem1";
            this.executedStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.executedStripMenuItem1.Text = "Executed";
            this.executedStripMenuItem1.Click += new System.EventHandler(this.executedStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changetableStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(503, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // changetableStripMenuItem1
            // 
            this.changetableStripMenuItem1.Name = "changetableStripMenuItem1";
            this.changetableStripMenuItem1.Size = new System.Drawing.Size(134, 21);
            this.changetableStripMenuItem1.Text = "Show Active Orders";
            this.changetableStripMenuItem1.Click += new System.EventHandler(this.changetableStripMenuItem1_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.listView1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OrderForm";
            this.Text = "Orders";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem payStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changeStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem processedStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem shipedStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem executedStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changetableStripMenuItem1;
    }
}