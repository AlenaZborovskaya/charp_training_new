using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactInformationTest : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            app.Contacts.CheckContactExistance();
            
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestContactInformationfromOtherForm()
        {
            app.Contacts.CheckContactExistance();
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            ContactData fromPersonForm = app.Contacts.GetContactInformationFromPersonForm(0);

            
            Assert.AreEqual(fromForm.AllContactInformation, fromPersonForm.AllContactInformation);
        }
    }
}
