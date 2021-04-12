using System;
using System.Collections.Generic;
using System.Text;

namespace ProductControl.ProductLib
{
    public class Folder : Elements
    {
        public Folder Parent;
        public List<Elements> ElementsList = new List<Elements>();
        public bool IsFolderfolder;

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
            IsFolderfolder = false;
            Type = FolderType.Default;
        }
    }
}
