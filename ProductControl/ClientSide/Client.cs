using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ProductControl.ClientSide;
using ProductControl.ProductLib;

namespace ProductControl
{
    [Serializable]

    public class Client

    {
        public static List<Client> AllClients = new List<Client>();
        [XmlElement("Name")]
        public string Name;
        [XmlElement("PhoneNumber")]

        public string PhoneNumber;
        [XmlElement("Email")]

        public string Email;
        [XmlElement("Password")]

        public string Passoword;
        [XmlElement("Orders")]
        public List<Order> Orders = new List<Order>();
        [XmlArrayItem("Product")]
        public List<Product> Cart = new List<Product>();

        public Client()
        {

        }
        public Client(string name, string phoneNumber, string email, string password)
        {
            Name = name;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Passoword = password;
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
