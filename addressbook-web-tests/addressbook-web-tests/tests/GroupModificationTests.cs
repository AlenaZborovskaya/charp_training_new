using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
   public  class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Navigator.GoToGroupPage();
            app.Groups.CheckGroupExistance();

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData tobeRemoved = oldGroups[0];

            GroupData newData = new GroupData("zzz");
            newData.Footer = null; //если укажем null то с полем не выполняется никаких действий: не очистки ни заполнения
            newData.Header = null;

            app.Groups.Modify(tobeRemoved, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            app.Navigator.ReturnToGroupPage();

           List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
           Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == tobeRemoved.Id) // та группа которую мы модифицировали
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }

        }

    }
}
