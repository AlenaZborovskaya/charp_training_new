using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace testdatageneratorcontacts
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]); // передаем количество данных, которые мы хотим сгенерировать
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(10))
                {
                    Lastname = TestBase.GenerateRandomString(100),
                    Address = TestBase.GenerateRandomString(100),
                    HomePhone = TestBase.GenerateRandomString(100),
                    MobilePhone = TestBase.GenerateRandomString(100),
                    WorkPhone = TestBase.GenerateRandomString(100)
                });
            }

            if (format == "xml")
            {
                writeContactsToXmlFile(contacts, writer);
            }
            else if (format == "json")
            {
                writeContactsToJsonFile(contacts, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognised format" + format);
            }

            writer.Close();
        }
       
        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
