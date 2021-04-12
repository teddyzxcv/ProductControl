using System;
using System.Collections.Generic;
using System.Text;

namespace ProductControl.ProductLib
{
    public class Folder : Elements
    {
        public Folder Parent;
        public List<Elements> ElementsList = new List<Elements>();

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

