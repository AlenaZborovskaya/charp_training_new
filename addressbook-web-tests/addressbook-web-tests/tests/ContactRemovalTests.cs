using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class ContactRemovaltests : AuthTestBase
    {
        [Test]
        public void ContactRemovaltest()
        {
            app.Navigator.OpenHomePage();
            app.Contacts.CheckContactExistance();

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.RemoveContact(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());
            List<ContactData> newContacts = app.Contacts.GetContactList();

            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
                Assert.AreNotEqual(contact.Firstname, toBeRemoved.Firstname);
                Assert.AreNotEqual(contact.Lastname, toBeRemoved.Lastname);
            }
            
        }
    }
}
      
