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
                parent.ElementsList.Add(folder);
        }
        private void RefreshFolder(TreeNodeCollection nodes, List<Elements> folderlist)
        {
            for (int i = 0; i < folderlist.Count; i++)
            {
                TreeNode node = new TreeNode(folderlist[i].Name);
                if (((Folder)(folderlist[i])).ElementsList.Count != 0)
                    RefreshFolder(node.Nodes, ((Folder)(folderlist[i])).ElementsList);
                nodes.Add(node);
            }
        }

        private void RefreshTree()
        {
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.Add("Warehouse");
            for (int i = 0; i < WareHouse.Count; i++)
            {
                TreeNode node = new TreeNode(WareHouse[i].Name);
                if (WareHouse[i].ElementsList.Count != 0)
                    RefreshFolder(node.Nodes, WareHouse[i].ElementsList);
                this.treeView1.Nodes[0].Nodes.Add(node);
            }
        }
        public Folder NodeSearch(string fullpath)
        {
            List<string> path = fullpath.Split('\\').ToList();
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
                Console.WriteLine("hello");
                CreateFolderForm folform = new CreateFolderForm();
                if (folform.ShowDialog() != DialogResult.OK)
                    return;
                if (treeView1.SelectedNode.Nodes.Cast<TreeNode>().Select(e => e.Text).Contains(folform.FolderName))
                    throw new ArgumentException("No! No repeat!");
                CreateFolder(folform.FolderName, NodeSearch(treeView1.SelectedNode.FullPath));
                saveStructure.SaveWarehouseList(WareHouse);
                WareHouse = saveStructure.LoadWarehouseList();
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
                    if (WareHouse.Select(e => e.Name).Contains(rnf.RenameName))
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
                Folder target = NodeSearch(this.treeView1.SelectedNode.FullPath);
                if (target.Parent == null)
                {
                    if (target.ElementsList.Count == 0)
                        WareHouse.Remove(target);
                    else
                        throw new ArgumentException("Cant delete folder! It is not empty!");
                }
                else
                {
                    if (target.ElementsList.Count == 0)
                        target.Parent.ElementsList.Remove(target);
                    else
                        throw new ArgumentException("Cant delete folder! It is not empty!");
                }

                saveStructure.SaveWarehouseList(WareHouse);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
