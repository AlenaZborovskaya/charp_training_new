using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase

    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });

            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {

            return (List<GroupData>) //конструкция приведения типа, так как метод десиолайз возвращает абстрактный объект
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));

        }
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {

            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));

        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {

            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(TestContext.CurrentContext.TestDirectory, @"groups.xlsx"));
            //Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;

        }

      

        [Test, TestCaseSource("GroupDataFromExcelFile")]

        public void GroupCreationTest(GroupData newData)
        {

           

            List<GroupData> oldGroups = GroupData.GetAll();


            app.Groups.Create(newData);

            //метод который быстро возвращает количество групп перед сравнением списков

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //метод для проверки того, что добавились группы. List<GroupData> - список объектов типа GroupData
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(newData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }



        [Test]
        public void BadNameGroupCreationTest()//проверка создания группы с пустыми именами
        {
            GroupData newData = new GroupData("a'a");
            newData.Footer = "";
            newData.Header = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(newData);
            //app.Navigator.ReturnToGroupPage();

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(newData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
        [Test]
        public void TestDBConnectivity()
        {
            foreach (ContactData contact in ContactData.GetAll())
            {
                System.Console.Out.WriteLine(contact.Deprecated);
            }
        }
    }
}
