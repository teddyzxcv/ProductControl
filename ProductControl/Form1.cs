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
using System.IO;
using ProductControl.ClientForm;
using ProductControl.ClientSide;


namespace ProductControl
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// All list.
        /// </summary>
        /// <returns></returns>
        static List<Folder> WareHouse = saveStructure.LoadWarehouseList();

        static bool IsAdmin;
        public static Client CurrentClient = new Client();

        /// <summary>
        /// Create folder.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        static void CreateFolder(string name, Folder parent)
        {
            try
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Refresh all treeview folder.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="fol"></param>
        private void RefreshFolder(TreeNodeCollection nodes, Folder fol)
        {
            try
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Find treenode by full path.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="fullPath"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static TreeNode FindTreeNodeByFullPath(TreeNodeCollection collection, string fullPath, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            try
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new TreeNode();
        }
        /// <summary>
        /// Refresh tree.
        /// </summary>
        private void RefreshTree()
        {
            try
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Search folder by treenode.
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public Folder NodeSearch(string fullpath)
        {
            try
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new Folder();
        }
        /// <summary>
        /// Initialize.
        /// </summary>
        public Form1()
        {
            try
            {
                InitializeComponent();
                WareHouse = saveStructure.LoadWarehouseList();
                RefreshTree();
                this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
                this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                this.dataGridView1.DefaultCellStyle.BackColor = Color.White;
                this.dataGridView1.Rows.Clear();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (treeView1.SelectedNode == null)
                    throw new ArgumentException("Plz, select a folder!");
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
        /// <summary>
        /// Change name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Create product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newProductStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode == null)
                    throw new ArgumentException("Plz, select a folder!");
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
                if (parent.ElementsList.Select(e => e.Name).Contains(cp.Product_Name))
                    throw new ArgumentException("No! No repeat!");
                Product pr = new Product(cp.Product_Name, cp.Article, cp.Remaining, cp.Price, cp.Description, cp.PathToPic, parent, selectednodepath);
                parent.ElementsList.Add(pr);
                saveStructure.SaveWarehouseList(WareHouse);
                RefreshTree();
                RefreshDataGrid(selectednodepath);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }
        /// <summary>
        /// Event when select treenode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                RefreshDataGrid(this.treeView1.SelectedNode.FullPath);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Refresh data grid.
        /// </summary>
        /// <param name="selectednodepath"></param>
        private void RefreshDataGrid(string selectednodepath)
        {
            try
            {
                this.dataGridView1.DataSource = saveStructure.DefaultDataTable();
                if (this.treeView1.SelectedNode != null)
                {
                    var folder = NodeSearch(selectednodepath);
                    if (folder != null)
                        this.dataGridView1.DataSource = saveStructure.ProcessProductFolder(folder);
                    else
                    {
                        this.dataGridView1.DataSource = saveStructure.ProcessAllProductFolder(WareHouse);
                    }
                    this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    // if (folder.Type == Folder.FolderType.ProductFolder)
                    //     {
                    //         this.dataGridView1.Visible = true;
                    //         this.dataGridView1.DataSource = saveStructure.ProcessProductFolder(folder);
                    //     }
                    //     else
                    //     {
                    //         this.dataGridView1.Visible = false;
                    //     }
                    // else
                    //     this.dataGridView1.Visible = false;
                }
                if (selectednodepath != null)
                {
                    var sn = FindTreeNodeByFullPath(this.treeView1.Nodes, selectednodepath);
                    if (sn != null)
                        this.treeView1.SelectedNode = sn;
                }
                this.dataGridView1.Refresh();
                this.dataGridView1.Sort(this.dataGridView1.Columns["Name"], ListSortDirection.Ascending);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Change product name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsAdmin)
                try
                {
                    WareHouse = saveStructure.LoadWarehouseList();
                    if (e.RowIndex == -1) return;
                    DataGridView dgv = sender as DataGridView;
                    if (dgv.CurrentRow.Index > dgv.Rows.Count - 2)
                        return;
                    string selectednodepath = null;
                    if (this.treeView1.SelectedNode != null)
                        selectednodepath = this.treeView1.SelectedNode.FullPath;
                    if (dgv == null)
                        return;
                    Folder fol = NodeSearch(this.treeView1.SelectedNode.FullPath);
                    if (fol == null || fol.Type == Folder.FolderType.FolderFolder)
                        throw new ArgumentException("Plz select a folder to change product directly in this folder");
                    if (dgv.CurrentRow.Selected && fol.Type == Folder.FolderType.ProductFolder)
                    {
                        string name = (string)dgv.CurrentRow.Cells[1].Value;
                        Product pr = (Product)fol.ElementsList.Find(e => e.Name == name);
                        fol.ElementsList.Remove(pr);
                        Productview pv = new Productview(name, pr.Article, pr.Remaining, pr.Price, pr.Description, pr.PathToPic);
                        DialogResult rs = pv.ShowDialog();
                        if (rs == DialogResult.No)
                        {
                            fol.ElementsList.Add(pr);
                            return;
                        }
                        if (rs == DialogResult.Retry)
                        {
                            if (fol.ElementsList.Count == 0)
                                fol.Type = Folder.FolderType.Default;
                            saveStructure.SaveWarehouseList(WareHouse);
                            RefreshTree();
                            RefreshDataGrid(selectednodepath);
                            return;
                        }
                        if (fol.ElementsList.Select(e => e.Name).Contains(pv.Product_Name))
                            throw new ArgumentException("No! No repeat!");
                        pr = new Product(pv.Product_Name, pv.Article, pv.Remaining, pv.Price, pv.Description, pv.PathToPic, fol, selectednodepath);
                        fol.ElementsList.Add(pr);
                        saveStructure.SaveWarehouseList(WareHouse);
                        RefreshTree();
                        RefreshDataGrid(selectednodepath);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
        }
        /// <summary>
        /// TO csv method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToCSVStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                ToCSVControlerForm tccf = new ToCSVControlerForm();
                if (tccf.ShowDialog() == DialogResult.Cancel)
                    return;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                var result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName, false);
                    sw.WriteLine("FullPath;Name;Article;Remaining");
                    for (int i = 0; i < WareHouse.Count; i++)
                    {
                        saveStructure.ToCSV(WareHouse[i], sfd.FileName, tccf.N, sw);
                    }
                    sw.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// New warehouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var ms = MessageBox.Show("Do you want save this warehouse list before create new one?", "Save the warehouse", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ms == DialogResult.Yes)
                    saveStructure.SaveXML();
                WareHouse = new List<Folder>();
                saveStructure.SaveWarehouseList(WareHouse);
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Save warehouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                saveStructure.SaveXML();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Open warehouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML Files (*.xml)|*.xml";
                ofd.ShowDialog();
                if (ofd.FileName == string.Empty)
                    return;
                File.Copy(ofd.FileName, saveStructure.PathToSaving, true);
                WareHouse = saveStructure.LoadWarehouseList();
                RefreshTree();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Random warehouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void randomStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                RandomForm rf = new RandomForm();
                rf.ShowDialog();
                WareHouse = saveStructure.GenerateRandomList(rf.nFolder, rf.nProduct, rf.nLevel);
                RefreshTree();
                saveStructure.SaveWarehouseList(WareHouse);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoginRegistrationForm lrf = new LoginRegistrationForm();
                var result = lrf.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.fileStripMenuItem1.Visible = false;
                    this.randomStripMenuItem1.Visible = false;
                    this.deleteStripMenuItem1.Visible = false;
                    this.changeNameStripMenuItem1.Visible = false;
                    this.newFolderStripMenuItem1.Visible = false;
                    this.newProductStripMenuItem1.Visible = false;
                    this.ToCSVStripMenuItem1.Visible = false;
                    this.clientsStripMenuItem1.Visible = false;
                    IsAdmin = false;
                    this.dataGridView1.ContextMenuStrip = this.contextMenuStrip2;
                    this.Text = "User: " + CurrentClient.Name;
                }
                else if (result == DialogResult.Yes)
                {
                    IsAdmin = true;
                    this.cartStripMenuItem1.Visible = false;
                    this.Text = "Admin control";
                }
                else
                {
                    this.Close();
                    return;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void addStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                WareHouse = saveStructure.LoadWarehouseList();
                if (this.dataGridView1.SelectedRows.Count == 0)
                    return;

                string path = this.dataGridView1.SelectedRows[0].Cells["Path"].Value.ToString();
                string name = this.dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                path = path.Substring(path.IndexOf('\\'));
                Folder fol = NodeSearch(path);
                CurrentClient.Cart.Add((Product)fol.ElementsList.Find(e => e.Name == name));
                XmlSerialzation.ClientSerialzation();
            }
            catch
            {
                return;
            }
        }

        private void cartStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                CartForm cf = new CartForm(CurrentClient);
                cf.ShowDialog();
                XmlSerialzation.ClientSerialzation();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ordersStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OrderForm of = new OrderForm(IsAdmin, CurrentClient);
                of.ShowDialog();
                XmlSerialzation.ClientSerialzation();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clientsStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AllClientsForm acf = new AllClientsForm();
                acf.ShowDialog();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
