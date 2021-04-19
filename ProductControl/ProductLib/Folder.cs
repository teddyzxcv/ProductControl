using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProductControl.ProductLib
{
    public class Folder : Elements
    {
        public Folder Parent;
        public List<Elements> ElementsList = new List<Elements>();

        public List<Product> AllUnderList
        {
            get
            {
                return OpenFolder(this);
            }
        }
        /// <summary>
        /// Open folder.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private List<Product> OpenFolder(Folder folder)
        {
            List<Product> output = new List<Product>();
            if (folder.Type == FolderType.ProductFolder)
                return folder.ElementsList.Select(e => (Product)e).ToList();
            else
            {
                for (int i = 0; i < folder.ElementsList.Count; i++)
                {
                    output.AddRange(OpenFolder((Folder)(folder.ElementsList[i])));
                }
            }
            return output;

        }
        public enum FolderType
        {
            Default,
            FolderFolder,
            ProductFolder
        }
        public FolderType Type;

        public Folder(string name, Folder parent)
        {
            Name = name;
            this.Parent = parent;
            Type = FolderType.Default;
        }
        public Folder(string name, Folder parent, FolderType type)
        {
            Name = name;
            this.Parent = parent;
            Type = type;
        }
    }
}

