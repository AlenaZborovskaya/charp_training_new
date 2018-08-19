using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;





namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(50))
                {
                    Lastname = GenerateRandomString(100),
                    Address = GenerateRandomString(100),
                    HomePhone = GenerateRandomString(100),
                    MobilePhone = GenerateRandomString(100),
                    WorkPhone = GenerateRandomString(100)
                });

            }

            return contacts;
            }

       

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {

            return (List<ContactData>) //конструкция приведения типа, так как метод десиолайз возвращает абстрактный объект
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));

        }
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {

            //return JsonConvert.DeserializeObject<List<ContactData>>(
            //    File.ReadAllText(@"contacts.json"));
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory , @"contacts.json")));
        }



        [Test, TestCaseSource("ContactDataFromJsonFile")]

        public void ContactCreationTest(ContactData contact)
        {


            List<ContactData> oldContacts = ContactData.GetAll(); 

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

             [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUi = app.Contacts.GetContactList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));


            start = DateTime.Now;
            List<ContactData> fromDb = ContactData.GetAllContacts();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}
