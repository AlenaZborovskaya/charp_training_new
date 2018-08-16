using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {



        public GroupHelper(ApplicationManager manager) // конструктор для driver, IWebDriver - параметр
         : base(manager)// обращаемся к конструктору base, в качестве параметра driver (когда создали HelperBase)

        {
        }

        public GroupHelper CheckGroupExistance()
        {
            if (IsElementPresent())
            {
                
            }
            else
            {
                GroupData newData = new GroupData("новая");
                newData.Footer = "новая"; 
                newData.Header = "новая";

                Create(newData);
                manager.Navigator.ReturnToGroupPage();

            }

            return this;
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        private List<GroupData> groupCashe = null; // здесь будет хранится заполненный и сохраненный список групп. При первом обращении в Group лист мы будем использовать пустой список, потом уже заполненный


        public List<GroupData> GetGroupList()
        {
            if (groupCashe == null) //если кэш еще не заполнен то заполняем его
            {
                groupCashe = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCashe.Add(new GroupData(element.Text) //получает текст для каждого отдельного элемента
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<GroupData>(groupCashe); //возвращаем копию кэша так как снаружи его может кто нибудь изменить
        }

        public GroupHelper Create(GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            InitNewGroup(); 
            FillGroupForm(newData);
            SubmitGroupCreation();
            manager.Navigator.ReturnToGroupPage();
            return this;
        }


        public GroupHelper Modify(int p, GroupData newData)
        {
            SelectGroup(p);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            manager.Navigator.ReturnToGroupPage();
            return this;
        }
        public GroupHelper Remove(int p)
        {
            SelectGroup(p);
            DeleteGroup();
            manager.Navigator.ReturnToGroupPage();
            return this;
        }


        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCashe = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {

            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public GroupHelper DeleteGroup()
        {
            driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
            groupCashe = null;
            return this;
        }



        public GroupHelper SelectGroup(int p)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (p+1) + "]")).Click();
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCashe = null;
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            

            Type(By.Name("group_name"), group.Name); //поле которое определяется локатором group_name, и передает значение groupname
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }


        public GroupHelper InitNewGroup() //заменили void на GroupHelper, добавили retutn this
        {

            driver.FindElement(By.XPath("(//input[@name='new'])[2]")).Click();
            return this;
        }
        public bool IsElementPresent()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

    }
}


