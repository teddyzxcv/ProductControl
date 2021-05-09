using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ProductControl.ClientSide
{
    public class XmlSerialzation
    {
        public static void ClientSerialzation()
        {
            FileStream fs = new FileStream("ClientSave.xml", FileMode.Create, FileAccess.ReadWrite);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Client>));
            serializer.Serialize(fs, Client.AllClients);
            fs.Close();
        }
        public static List<Client> DeserializationClient()
        {
            FileStream fs = new FileStream("ClientSave.xml", FileMode.Open, FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Client>));
            var output = (List<Client>)serializer.Deserialize(fs);
            fs.Close();
            return output;
        }
    }
}
