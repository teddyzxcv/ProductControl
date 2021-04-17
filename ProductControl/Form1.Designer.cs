
namespace ProductControl
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Warehouse");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newProductStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToCSVStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.randomStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeNameStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileStripMenuItem1,
            this.newFolderStripMenuItem1,
            this.newProductStripMenuItem1,
            this.ToCSVStripMenuItem1,
            this.randomStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(870, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileStripMenuItem1
            // 
            this.fileStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenStripMenuItem1,
            this.saveStripMenuItem1,
            this.newStripMenuItem1});
            this.fileStripMenuItem1.Name = "fileStripMenuItem1";
            this.fileStripMenuItem1.Size = new System.Drawing.Size(39, 21);
            this.fileStripMenuItem1.Text = "File";
            // 
            // OpenStripMenuItem1
            // 
            this.OpenStripMenuItem1.Name = "OpenStripMenuItem1";
            this.OpenStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.OpenStripMenuItem1.Text = "Open warehouse";
            this.OpenStripMenuItem1.Click += new System.EventHandler(this.OpenStripMenuItem1_Click);
            // 
            // saveStripMenuItem1
            // 
            this.saveStripMenuItem1.Name = "saveStripMenuItem1";
            this.saveStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.saveStripMenuItem1.Text = "Save warehouse";
            this.saveStripMenuItem1.Click += new System.EventHandler(this.saveStripMenuItem1_Click);
            // 
            // newStripMenuItem1
            // 
            this.newStripMenuItem1.Name = "newStripMenuItem1";
            this.newStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.newStripMenuItem1.Text = "New warehouse";
            this.newStripMenuItem1.Click += new System.EventHandler(this.newStripMenuItem1_Click);
            // 
            // newFolderStripMenuItem1
            // 
            this.newFolderStripMenuItem1.Name = "newFolderStripMenuItem1";
            this.newFolderStripMenuItem1.Size = new System.Drawing.Size(87, 21);
            this.newFolderStripMenuItem1.Text = "New Folder";
            this.newFolderStripMenuItem1.Click += new System.EventHandler(this.newPrjStripMenuItem1_Click);
            // 
            // newProductStripMenuItem1
            // 
            this.newProductStripMenuItem1.Name = "newProductStripMenuItem1";
            this.newProductStripMenuItem1.Size = new System.Drawing.Size(108, 21);
            this.newProductStripMenuItem1.Text = "Create product";
            this.newProductStripMenuItem1.Click += new System.EventHandler(this.newProductStripMenuItem1_Click);
            // 
            // ToCSVStripMenuItem1
            // 
            this.ToCSVStripMenuItem1.Name = "ToCSVStripMenuItem1";
            this.ToCSVStripMenuItem1.Size = new System.Drawing.Size(58, 21);
            this.ToCSVStripMenuItem1.Text = "ToCSV";
            this.ToCSVStripMenuItem1.Click += new System.EventHandler(this.ToCSVStripMenuItem1_Click);
            // 
            // randomStripMenuItem1
            // 
            this.randomStripMenuItem1.Name = "randomStripMenuItem1";
            this.randomStripMenuItem1.Size = new System.Drawing.Size(69, 21);
            this.randomStripMenuItem1.Text = "Random";
            this.randomStripMenuItem1.Click += new System.EventHandler(this.randomStripMenuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(870, 436);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Warehouse";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.Size = new System.Drawing.Size(250, 436);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(616, 436);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeNameStripMenuItem1,
            this.deleteStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 48);
            // 
            // changeNameStripMenuItem1
            // 
            this.changeNameStripMenuItem1.Name = "changeNameStripMenuItem1";
            this.changeNameStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.changeNameStripMenuItem1.Text = "Change Name";
            this.changeNameStripMenuItem1.Click += new System.EventHandler(this.changeNameStripMenuItem1_Click);
            // 
            // deleteStripMenuItem1
            // 
            this.deleteStripMenuItem1.Name = "deleteStripMenuItem1";
            this.deleteStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.deleteStripMenuItem1.Text = "Delete";
            this.deleteStripMenuItem1.Click += new System.EventHandler(this.deleteStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "Product Control";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newFolderStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeNameStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem newProductStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToCSVStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OpenStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem randomStripMenuItem1;
    }
}

