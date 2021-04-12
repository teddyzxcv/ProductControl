using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductControl.ProductLib;

namespace ProductControl
{
    public partial class Form1 : Form
    {
        static List<Folder> WareHouse = saveStructure.LoadWarehouseList();

        static void CreateFolder(string name, Folder parent)
        {
            Folder folder = new Folder(name, parent);
            if (parent == null)
                WareHouse.Add(folder);
            else
            {
                parent.ElementsList.Add(folder);
                parent.Type = Folder.FolderType.FolderFolder;
            }
        }
        private void RefreshFolder(TreeNodeCollection nodes, Folder fol)
        {
            if (fol.Type == Folder.FolderType.FolderFolder)
                for (int i = 0; i < fol.ElementsList.Count; i++)
                {
                    TreeNode node = new TreeNode(fol.ElementsList[i].Name);
                    if (((Folder)(fol.ElementsList[i])).ElementsList.Count != 0)
                        RefreshFolder(node.Nodes, (Folder)fol.ElementsList[i]);
                    nodes.Add(node);
                }
        }
        public static TreeNode FindTreeNodeByFullPath(TreeNodeCollection collection, string fullPath, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            var foundNode = collection.Cast<TreeNode>().FirstOrDefault(tn => string.Equals(tn.FullPath, fullPath, comparison));
            if (null == foundNode)
            {
                foreach (var childNode in collection.Cast<TreeNode>())
                {
                    var foundChildNode = FindTreeNodeByFullPath(childNode.Nodes, fullPath, comparison);
                    if (null != foundChildNode)
                    {
                        return foundChildNode;
                    }
                }
            }
            return foundNode;
        }

        private void RefreshTree()
        {
            var savestate = this.treeView1.Nodes.GetExpansionState();
            string selectednodepath = null;
            if (this.treeView1.SelectedNode != null)
                selectednodepath = this.treeView1.SelectedNode.FullPath;
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.Add("Warehouse");
            for (int i = 0; i < WareHouse.Count; i++)
            {
                TreeNode node = new TreeNode(WareHouse[i].Name);
                if (WareHouse[i].ElementsList.Count != 0)
                    RefreshFolder(node.Nodes, WareHouse[i]);
                this.treeView1.Nodes[0].Nodes.Add(node);
            }
            this.treeView1.Nodes.SetExpansionState(savestate);
            if (selectednodepath != null)
            {
                var sn = FindTreeNodeByFullPath(this.treeView1.Nodes, selectednodepath);
                if (sn != null)
                    sn.Expand();
            }
        }
        public Folder NodeSearch(string fullpath)
        {
            List<string> path = fullpath.Split('\\').ToList();
            if (path.Count == 1)
                return null;
            Elements targetFolder = WareHouse.Find(e => e.Name == path[1]);
            for (int i = 1; i < path.Count - 1; i++)
            {
                targetFolder = ((Folder)(targetFolder)).ElementsList.Find(e => e.Name == path[i + 1]);
            }
            Folder output = (Folder)targetFolder;
            return output;
        }
        public Form1()
        {
            InitializeComponent();
            WareHouse = saveStructure.LoadWarehouseList();
            RefreshTree();
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView1.DefaultCellStyle.BackColor = Color.White;
            this.dataGridView1.Rows.Clear();
        }
        /// <summary>
        /// Create new Folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newPrjStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var parent = NodeSearch(treeView1.SelectedNode.FullPath);
                if (treeView1.SelectedNode.FullPath != "Warehouse" && parent.Type == Folder.FolderType.ProductFolder)
                    throw new ArgumentException("Can't add folder to a product folder");
                CreateFolderForm folform = new CreateFolderForm();
                if (folform.ShowDialog() != DialogResult.OK)
                    return;
                if (treeView1.SelectedNode.Nodes.Cast<TreeNode>().Select(e => e.Text).Contains(folform.FolderName))
                    throw new ArgumentException("No! No repeat!");
                CreateFolder(folform.FolderName, parent);
                saveStructure.SaveWarehouseList(WareHouse);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void changeNameStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Folder target = NodeSearch(this.treeView1.SelectedNode.FullPath);
                RenameForm rnf = new RenameForm();
                rnf.ShowDialog();
                if (target.Parent == null)
                {
                    if (!WareHouse.Select(e => e.Name).Contains(rnf.RenameName))
                        target.Name = rnf.RenameName;
                    else
                        throw new ArgumentException("Cant change task name!");
                }
                else
                {
                    if (!target.Parent.ElementsList.Select(e => e.Name).Contains(rnf.RenameName))
                        target.Name = rnf.RenameName;
                    else
                        throw new ArgumentException("Cant change task name!");
                }
                saveStructure.SaveWarehouseList(WareHouse);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode.Text == "Warehouse")
                    throw new ArgumentException("Can't delete folder! You can't delete warehouse!");
                Folder target = NodeSearch(this.treeView1.SelectedNode.FullPath);
                if (target.Parent == null)
                {
                    if (target.ElementsList.Count == 0)
                        WareHouse.Remove(target);
                    else
                        throw new ArgumentException("Can't delete folder! It is not empty!");
                }
                else
                {
                    if (target.ElementsList.Count == 0)
                        target.Parent.ElementsList.Remove(target);
                    else
                        throw new ArgumentException("Can't delete folder! It is not empty!");
                    if (target.Parent.ElementsList.Count == 0)
                        target.Parent.Type = Folder.FolderType.Default;
                }

                saveStructure.SaveWarehouseList(WareHouse);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newProductStripMenuItem1_Click(object sender, EventArgs e)
        {
            // try
            // {
            string selectednodepath = null;
            if (this.treeView1.SelectedNode != null)
                selectednodepath = this.treeView1.SelectedNode.FullPath;
            if (this.treeView1.SelectedNode.Text == "Warehouse")
                throw new ArgumentException("Can't add product straight to warehouse!!");
            var parent = NodeSearch(treeView1.SelectedNode.FullPath);
            if (parent.Type == Folder.FolderType.FolderFolder)
                throw new ArgumentException("Can't add product to folderfolder!");
            parent.Type = Folder.FolderType.ProductFolder;
            CreateProduct cp = new CreateProduct();
            if (cp.ShowDialog() != DialogResult.OK)
                return;
            Product pr = new Product(cp.Product_Name, cp.Article, cp.Remaining, cp.Price, cp.Description, cp.PathToPic, parent);
            parent.ElementsList.Add(pr);
            saveStructure.SaveWarehouseList(WareHouse);
            RefreshTree();
            RefreshDataGrid(selectednodepath);
            // }
            // catch (Exception er)
            // {
            //     MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // };
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshDataGrid(this.treeView1.SelectedNode.FullPath);
        }
        private void RefreshDataGrid(string selectednodepath)
        {
            this.dataGridView1.DataSource = saveStructure.DefaultDataTable();
            if (this.treeView1.SelectedNode != null)
            {
                var folder = NodeSearch(this.treeView1.SelectedNode.FullPath);
                if (folder != null)
                    if (folder.Type == Folder.FolderType.ProductFolder)
                    {
                        this.dataGridView1.Visible = true;
                        this.dataGridView1.DataSource = saveStructure.ProcessProductFolder(folder);
                    }
                    else
                        this.dataGridView1.Visible = false;
                else
                    this.dataGridView1.Visible = false;

            }
            if (selectednodepath != null)
            {
                var sn = FindTreeNodeByFullPath(this.treeView1.Nodes, selectednodepath);
                if (sn != null)
                    this.treeView1.SelectedNode = sn;
            }
            this.dataGridView1.Refresh();
        }
    }
}
