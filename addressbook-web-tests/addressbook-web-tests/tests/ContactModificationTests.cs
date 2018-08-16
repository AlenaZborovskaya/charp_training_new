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

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldData = oldContacts[0];

            app.Contacts.Modifycontact(0, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = newContact.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id) // контакт, который мы модифицировали
                {
                    Assert.AreEqual(newContact.Firstname, contact.Firstname);
                }
            }
        }
    }
}
