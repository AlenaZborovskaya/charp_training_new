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

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData tobeRemoved = oldContacts[0];

            app.Contacts.RemoveContact(tobeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, tobeRemoved.Id);
                Assert.AreNotEqual(contact.Firstname, tobeRemoved.Firstname);
                Assert.AreNotEqual(contact.Lastname, tobeRemoved.Lastname);
            }
            
        }
    }
}
      
