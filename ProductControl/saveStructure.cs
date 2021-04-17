using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Xml;
using ProductControl.ProductLib;

namespace ProductControl
{ /// <summary>
  /// Class for check tree view.<see langword="abstract"/>
  /// </summary>
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Save all tree expansion states.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<string> GetExpansionState(this TreeNodeCollection nodes)
        {
            return nodes.Descendants()
                        .Where(n => n.IsExpanded)
                        .Select(n => n.FullPath)
                        .ToList();
        }
        /// <summary>
        /// Set tree expansion state.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="savedExpansionState"></param>
        public static void SetExpansionState(this TreeNodeCollection nodes, List<string> savedExpansionState)
        {
            foreach (var node in nodes.Descendants()
                                      .Where(n => savedExpansionState.Contains(n.FullPath)))
            {
                node.Expand();
            }
        }

        public static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                yield return node;

                foreach (var child in node.Nodes.Descendants())
                {
                    yield return child;
                }
            }
        }
    }
    class saveStructure
    {
        /// <summary>
        /// Path to save file xml.
        /// </summary>
        public static string PathToSaving = "StructureSave.xml";
        public static string PathToSavePic = "Pictures";
        public static List<Folder> LoadWarehouseList()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSaving);
            XmlNodeList WarehouseList = doc.SelectNodes("//ProductControl/Warehouse/Folder");
            List<Folder> Output = new List<Folder>();
            if (WarehouseList != null)
            {
                foreach (XmlNode item in WarehouseList)
                {
                    Folder folder = new Folder(item.Attributes["Name"].Value, null, (Folder.FolderType)Enum.Parse(typeof(Folder.FolderType), item.Attributes["Type"].Value));
                    if (folder.Type == Folder.FolderType.FolderFolder)
                        folder.ElementsList = LoadFolderList(item.SelectNodes("Folder").Cast<XmlNode>().ToList(), folder);
                    else if (folder.Type == Folder.FolderType.ProductFolder)
                        folder.ElementsList = LoadProductList(item.SelectNodes("Product").Cast<XmlNode>().ToList(), folder);
                    Output.Add(folder);
                }
            }
            return Output;
        }

        public static List<Elements> LoadFolderList(List<XmlNode> nodes, Folder parent)
        {
            List<Elements> Output = new List<Elements>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Folder fol = new Folder(nodes[i].Attributes["Name"].Value, parent, (Folder.FolderType)Enum.Parse(typeof(Folder.FolderType), nodes[i].Attributes["Type"].Value));
                if (fol.Type == Folder.FolderType.FolderFolder)
                    fol.ElementsList = LoadFolderList(nodes[i].SelectNodes("Folder").Cast<XmlNode>().ToList(), fol);
                else if (fol.Type == Folder.FolderType.ProductFolder)
                    fol.ElementsList = LoadProductList(nodes[i].SelectNodes("Product").Cast<XmlNode>().ToList(), fol);
                Output.Add(fol);
            }
            return Output;
        }
        public static List<Elements> LoadProductList(List<XmlNode> nodes, Folder parent)
        {
            List<Elements> Output = new List<Elements>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Product pro = new Product(nodes[i].Attributes["Name"].Value, nodes[i].Attributes["Article"].Value, int.Parse(nodes[i].Attributes["Remain"].Value)
                    , int.Parse(nodes[i].Attributes["Price"].Value), nodes[i].Attributes["Description"].Value, nodes[i].Attributes["PathToPic"].Value, parent, nodes[i].Attributes["FullPath"].Value);
                Output.Add(pro);
            }
            return Output;
        }

        public static void SaveWarehouseList(List<Folder> folList)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSaving);
            XmlNode root = doc.SelectSingleNode("ProductControl");
            root.SelectSingleNode("Warehouse").RemoveAll();
            XmlNode prjnodelist = root.SelectSingleNode("Warehouse");
            for (int i = 0; i < folList.Count; i++)
            {
                XmlElement newFolder = doc.CreateElement("Folder");
                XmlAttribute FolderName = doc.CreateAttribute("Name");
                XmlAttribute FolderType = doc.CreateAttribute("Type");
                FolderType.Value = folList[i].Type.ToString();
                FolderName.Value = folList[i].Name;
                newFolder.SetAttributeNode(FolderName);
                newFolder.SetAttributeNode(FolderType);
                if (folList[i].ElementsList.Count != 0)
                {
                    if (folList[i].Type == Folder.FolderType.FolderFolder || folList[i].Type == Folder.FolderType.Default)
                        SaveFolder(ref newFolder, folList[i], doc);
                    if (folList[i].Type == Folder.FolderType.ProductFolder)
                        SaveProduct(newFolder, folList[i], doc);
                }
                prjnodelist.AppendChild(newFolder);
            }
            doc.Save(PathToSaving);
        }
        /// <summary>
        /// Save Folder.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="follist"></param>
        /// <param name="doc"></param>
        private static void SaveFolder(ref XmlElement node, Folder fol, XmlDocument doc)
        {
            for (int i = 0; i < fol.ElementsList.Count; i++)
            {
                XmlElement newSubList = doc.CreateElement("Folder");
                XmlAttribute taskName = doc.CreateAttribute("Name");
                XmlAttribute FolderType = doc.CreateAttribute("Type");
                FolderType.Value = ((Folder)(fol.ElementsList[i])).Type.ToString();
                taskName.Value = fol.ElementsList[i].Name;
                newSubList.SetAttributeNode(taskName);
                newSubList.SetAttributeNode(FolderType);
                if (((Folder)(fol.ElementsList[i])).ElementsList.Count != 0)
                {
                    if (((Folder)(fol.ElementsList[i])).Type == Folder.FolderType.FolderFolder || ((Folder)(fol.ElementsList[i])).Type == Folder.FolderType.Default)
                        SaveFolder(ref newSubList, (Folder)(fol.ElementsList[i]), doc);
                    else
                    if (((Folder)(fol.ElementsList[i])).Type == Folder.FolderType.ProductFolder)
                        SaveProduct(newSubList, (Folder)(fol.ElementsList[i]), doc);

                }
                node.AppendChild(newSubList);
            }
        }

        public static DataTable DefaultDataTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Path");
            result.Columns.Add("Name");
            result.Columns.Add("Article");
            result.Columns.Add("Remaining");
            result.Columns.Add("Price");
            return result;
        }
        private static void SaveProduct(XmlElement node, Folder folder, XmlDocument doc)
        {
            for (int i = 0; i < folder.ElementsList.Count; i++)
            {
                XmlElement newProduct = doc.CreateElement("Product");
                XmlAttribute prductName = doc.CreateAttribute("Name");
                XmlAttribute prductArticle = doc.CreateAttribute("Article");
                XmlAttribute prductPrice = doc.CreateAttribute("Price");
                XmlAttribute prductRemain = doc.CreateAttribute("Remain");
                XmlAttribute prductPathToPic = doc.CreateAttribute("PathToPic");
                XmlAttribute prductDescp = doc.CreateAttribute("Description");
                XmlAttribute prductFullPath = doc.CreateAttribute("FullPath");
                prductName.Value = ((Product)folder.ElementsList[i]).Name;
                prductArticle.Value = ((Product)folder.ElementsList[i]).Article;
                prductPrice.Value = ((Product)folder.ElementsList[i]).Price.ToString();
                prductRemain.Value = ((Product)folder.ElementsList[i]).Remaining.ToString();
                prductPathToPic.Value = ((Product)folder.ElementsList[i]).PathToPic;
                prductDescp.Value = ((Product)folder.ElementsList[i]).Description;
                prductFullPath.Value = ((Product)folder.ElementsList[i]).FullPath;
                newProduct.SetAttributeNode(prductName);
                newProduct.SetAttributeNode(prductArticle);
                newProduct.SetAttributeNode(prductPrice);
                newProduct.SetAttributeNode(prductRemain);
                newProduct.SetAttributeNode(prductPathToPic);
                newProduct.SetAttributeNode(prductDescp);
                newProduct.SetAttributeNode(prductFullPath);
                node.AppendChild(newProduct);
            }
        }
        public static DataTable ProcessProductFolder(Folder fol)
        {
            DataTable result = DefaultDataTable();
            for (int i = 0; i < fol.AllUnderList.Count; i++)
            {
                var row = result.NewRow();
                var product = fol.AllUnderList[i];
                row["Path"] = product.FullPath;
                row["Name"] = product.Name;
                row["Article"] = product.Article;
                row["Remaining"] = product.Remaining;
                row["Price"] = product.Price;
                result.Rows.Add(row);
            }
            return result;
        }
        public static DataTable ProcessAllProductFolder(List<Folder> fols)
        {
            DataTable result = DefaultDataTable();
            for (int j = 0; j < fols.Count; j++)
            {
                for (int i = 0; i < fols[j].AllUnderList.Count; i++)
                {
                    var row = result.NewRow();
                    var product = fols[j].AllUnderList[i];
                    row["Path"] = product.FullPath;
                    row["Name"] = product.Name;
                    row["Article"] = product.Article;
                    row["Remaining"] = product.Remaining;
                    row["Price"] = product.Price;
                    result.Rows.Add(row);
                }
            }

            return result;
        }
        public static void ToCSV(Folder fol, string strFilePath, int remain, StreamWriter sw)
        {
            if (fol.Type == Folder.FolderType.ProductFolder)
            {
                foreach (Product dr in fol.ElementsList)
                {
                    if (dr.Remaining <= remain)
                        sw.WriteLine($"{dr.FullPath},{dr.Article},{dr.Name},{dr.Remaining}");
                }
            }
            else if (fol.Type == Folder.FolderType.FolderFolder)
            {
                for (int i = 0; i < fol.ElementsList.Count; i++)
                {
                    ToCSV((Folder)fol.ElementsList[i], strFilePath, remain, sw);
                }
            }
        }
        public static List<Folder> GenerateRandomList(int nFolder, int nProduct, int level)
        {
            string alp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<Folder> warehouse = new List<Folder>();
            var rand = new Random();
            for (int j = 0; j < nFolder; j++)
            {
                warehouse.Add(new Folder(alp[j].ToString(), null, Folder.FolderType.Default));
            }
            var currentlist = warehouse;
            var defaultlist = new List<Elements>();
            for (int k = 0; k < level - 1; k++)
            {
                var nextlist = new List<Folder>();
                int remain = nFolder;
                for (int i = 0; i < currentlist.Count; i++)
                {
                    int folderinlevel = rand.Next(1, remain);
                    currentlist[i].Type = Folder.FolderType.FolderFolder;
                    for (int j = 0; j < folderinlevel; j++)
                    {
                        var fol = new Folder(alp[j].ToString(), currentlist[i], Folder.FolderType.Default);
                        currentlist[i].ElementsList.Add(fol);
                        nextlist.Add(fol);
                    }
                    remain -= folderinlevel;
                    if (remain == 0)
                    {
                        break;
                    }
                }
                defaultlist.AddRange(currentlist.Where(e => ((Folder)e).Type == Folder.FolderType.Default));
                currentlist = nextlist;
            }
            defaultlist.AddRange(currentlist.Where(e => ((Folder)e).Type == Folder.FolderType.Default));

            for (int i = 0; i < defaultlist.Count; i++)
            {
                Folder fol = (Folder)defaultlist[i];
                fol.Type = Folder.FolderType.ProductFolder;
                for (int j = 0; j < nProduct; j++)
                {
                    fol.ElementsList.Add(new Product(alp[i].ToString(), j.ToString(), rand.Next(0, 100), rand.Next(1, 10000), alp[i].ToString(), "default - image.png", fol, FolderToPath(fol)));
                }
            }
            return warehouse;
        }

        public static string FolderToPath(Folder fol)
        {
            List<string> path = new List<string>();
            Folder parent;
            do
            {
                parent = fol.Parent;
                path.Add(fol.Name);
                fol = fol.Parent;
            } while (parent != null);
            path.Add("Warehouse");
            path.Reverse();
            return string.Join('\\', path);
        }
        public static void SaveXML()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files (*.xml)|*.xml";
            sfd.ShowDialog();
            if (sfd.FileName != string.Empty)
                File.Copy(PathToSaving, sfd.FileName);
        }

    }
}
