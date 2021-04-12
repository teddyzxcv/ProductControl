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
        static string PathToSaving = "StructureSave.xml";

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
                    Folder folder = new Folder(item.Attributes["Name"].Value, null);
                    folder.ElementsList = LoadFolderList(item.SelectNodes("Folder").Cast<XmlNode>().ToList(), folder);
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
                Folder fol = new Folder(nodes[i].Attributes["Name"].Value, parent);
                fol.ElementsList = LoadFolderList(nodes[i].SelectNodes("Folder").Cast<XmlNode>().ToList(), parent);
                Output.Add(fol);
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
                FolderName.Value = folList[i].Name;
                newFolder.SetAttributeNode(FolderName);
                if (folList[i].ElementsList.Count != 0)
                {
                    XmlElement newTaskList = doc.CreateElement("Folder");
                    SaveFolder(ref newTaskList, folList[i].ElementsList, doc);
                    newFolder.AppendChild(newTaskList);
                }
                prjnodelist.AppendChild(newFolder);
            }
            doc.Save(PathToSaving);
        }
        /// <summary>
        /// Save task.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="follist"></param>
        /// <param name="doc"></param>
        private static void SaveFolder(ref XmlElement node, List<Elements> follist, XmlDocument doc)
        {
            for (int i = 0; i < follist.Count; i++)
            {
                XmlAttribute taskName = doc.CreateAttribute("Name");
                taskName.Value = follist[i].Name;
                node.SetAttributeNode(taskName);
                if (((Folder)(follist[i])).ElementsList.Count != 0)
                {
                    XmlElement newSubList = doc.CreateElement("Folder");
                    SaveFolder(ref newSubList, ((Folder)(follist[i])).ElementsList, doc);
                    node.AppendChild(newSubList);
                }
            }
        }

    }
}
