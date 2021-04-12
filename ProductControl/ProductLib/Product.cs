using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProductControl.ProductLib
{
    public class Product : Elements
    {
        public string FullPath;

        public string Article;

        public int Remaining;
        public int Price;
        public string Description;
        public string PathToPic;

        public Folder Parent;

        public Product(string name, string article, int remaining, int price, string description, string pathtopic, Folder parent, string fullpath)
        {
            Name = name;
            Article = article;
            Remaining = remaining;
            Price = price;
            Description = description;
            PathToPic = pathtopic;
            Parent = parent;
            FullPath = fullpath;
        }

    }
}
