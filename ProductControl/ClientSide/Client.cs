using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ProductControl.ClientSide;
using ProductControl.ProductLib;
using System.Linq;

namespace ProductControl
{
    [Serializable]

    public class Client

    {
        [XmlElement("IsAdmin")]
        public bool IsAdmin;

        [XmlIgnore]
        public static List<Client> AllClients = new List<Client>();
        [XmlElement("Name")]
        public string Name;
        [XmlElement("PhoneNumber")]

        public string PhoneNumber;
        [XmlElement("Email")]

        public string Email;
        [XmlElement("Password")]

        public string Passoword;
        [XmlArrayItem("Order")]
        public List<Order> SerOrders = new List<Order>();
        [XmlArrayItem("Product")]
        public List<Product> Cart = new List<Product>();
        [XmlIgnore]
        public List<Order> Orders
        {
            get
            {
                foreach (var item in SerOrders)
                {
                    item.OrderClient = this;
                }
                return SerOrders;
            }
        }
        public double AllPriceAmount
        {
            get
            {
                return this.Orders.Select(e => e.OrderCost).Sum();
            }
        }

        public Client()
        {

        }
        public Client(string name, string phoneNumber, string email, string password, bool isadmin)
        {
            Name = name;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Passoword = password;
            this.IsAdmin = isadmin;

        }
        public override bool Equals(object obj)
        {
            var client = (Client)obj;
            return client.Email == this.Email;
        }
        public override int GetHashCode()
        {
            return this.Email.GetHashCode() * 33 + 27;
        }
    }
}
