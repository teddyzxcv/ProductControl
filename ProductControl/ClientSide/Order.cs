using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ProductControl.ProductLib;
using System.Linq;

namespace ProductControl
{
    [Serializable]
    public class Order
    {
        [XmlIgnore]
        public Client OrderClient;
        [XmlIgnore]
        public static List<Order> AllOrder
        {
            get
            {
                List<Order> orders = new List<Order>();
                var allclients = Client.AllClients;
                foreach (var item in allclients)
                {
                    orders.AddRange(item.Orders);
                }
                return orders;
            }
        }
        [XmlElement("Product")]
        public List<Product> Products;
        [XmlElement("Index")]

        public double OrderCost
        {
            get
            {
                return this.Products.Select(e => e.Price).Sum();
            }

        }
        public int Index;
        [XmlElement("CreateTime")]
        public DateTime CreateTime;

        [XmlElement("Status")]

        public OrderStatus Status;
        [Flags]
        public enum OrderStatus
        {
            None = 1 << 3,
            Processed = 3 << 1,
            Payed = 1 << 0,
            Shipped = 1 << 1,
            Executed = 2 << 1
        }
        public Order()
        {
            CreateTime = DateTime.Now;
            Status = OrderStatus.None;
        }

    }
}
