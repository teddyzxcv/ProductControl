using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace ProductControl.ProductLib
{
    [Serializable]
    public class Product : Elements
    {
        public string FullPath;
        [XmlElement("Article")]
        public string Article;
        [XmlIgnore]
        public int Remaining;
        [XmlElement("Price")]
        public int Price;
        [XmlIgnore]
        public string Description;
        [XmlIgnore]
        public string PathToPic;
        [XmlIgnore]
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
        public Product()
        {

        }
        public override bool Equals(object obj)
        {
            var pr = (Product)obj;
            return pr.Article == this.Article && pr.FullPath == this.FullPath;
        }
        public override int GetHashCode()
        {
            return this.Article.GetHashCode() * 33 + 27 * this.FullPath.GetHashCode();
        }

    }
}
