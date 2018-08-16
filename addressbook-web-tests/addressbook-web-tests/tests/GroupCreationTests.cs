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
    public class GroupCreationTests : AuthTestBase

    { 
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i =0; i<5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100), Footer = GenerateRandomString(100)
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
           
            return (List < GroupData >) //конструкция приведения типа, так как метод десиолайз возвращает абстрактный объект
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
           
        }
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {

            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));

        }


        [Test, TestCaseSource("GroupDataFromJsonFile")]

        public void GroupCreationTest(GroupData newData)
        {
            

           
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            

            app.Groups.Create(newData);

            //метод который быстро возвращает количество групп перед сравнением списков
            
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //метод для проверки того, что добавились группы. List<GroupData> - список объектов типа GroupData
            List<GroupData> newGroups = app.Groups.GetGroupList();
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
    }
}

