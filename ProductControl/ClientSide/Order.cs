using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ProductControl.ProductLib;

namespace ProductControl
{
    [Serializable]
    public class Order
    {
        [XmlElement("Product")]
        public List<Product> Products;
        [XmlElement("Index")]

        public int Index;
        [XmlElement("CreateTime")]
        public DateTime CreateTime;

        [Flags]
        public enum Status
        {
            Processed,
            Payed,
            Shipped,
            Executed
        }

    }
}
