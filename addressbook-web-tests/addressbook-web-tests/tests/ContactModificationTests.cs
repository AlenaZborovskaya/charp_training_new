using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Navigator.OpenHomePage();
            app.Contacts.CheckContactExistance();

            ContactData newContact = new ContactData("измененный");
            newContact.Lastname = null;
            newContact.Address = null;
            newContact.HomePhone = null;
            newContact.MobilePhone = null;
            newContact.WorkPhone = null;
            newContact.Fax = null;
            newContact.Email = null;
            newContact.Email2 = null;
            newContact.Email3 = null;

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData tobeModified = oldContacts[0];

            app.Contacts.Modify(tobeModified, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            tobeModified.Firstname = newContact.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == tobeModified.Id) // контакт, который мы модифицировали
                {
                    Assert.AreEqual(newContact.Firstname, contact.Firstname);
                }
            }
        }
    }
}
