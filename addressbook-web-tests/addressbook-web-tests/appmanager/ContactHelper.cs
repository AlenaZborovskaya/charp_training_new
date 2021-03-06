﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        //private bool acceptNextAlert;

        public ContactHelper(ApplicationManager manager) // конструктор для driver, IWebDriver - параметр
 : base(manager)// обращаемся к конструктору base, в качестве параметра driver (когда создали HelperBase)

        {
        }



        public ContactHelper CheckContactExistance()
        {
            if (IsElementPresent())
            {

            }
            else
            {
                ContactData contact = new ContactData("Маша");
                contact.Lastname = "Иванова";
                contact.Address = "Ватутина 13";
                contact.HomePhone = "293-47-62";
                contact.MobilePhone = "8(910) 132 86 35";
                contact.WorkPhone = "8(831) 2934444";

                Create(contact);

            }
            return this;
        }



        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//table/tbody/tr[contains(@name, 'entry')]")).Count;
        }

        private List<ContactData> contactsCashe = null;




        public List<ContactData> GetContactList()
        {
            if (contactsCashe == null) //если кэш еще не заполнен то заполняем его
            {
                contactsCashe = new List<ContactData>();
                var rows = driver.FindElements(By.Name("entry"));
                foreach (var element in rows)
                {
                    var cells = element.FindElements(By.TagName("td"));
                    string lastName = cells[1].Text;
                    string firstName = cells[2].Text;
                    string id = cells[0].FindElement(By.TagName("input")).GetAttribute("value");

                    contactsCashe.Add(new ContactData(firstName)
                    {
                        Lastname = lastName,
                        Id = id
                    });

                }
            }
            return new List<ContactData>(contactsCashe);

        }

        public ContactHelper Create(ContactData contact)
        {
            CreationContact();
            InputContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modifycontact(int p, ContactData newContact)
        {
            SelectContact(p);
            InitContactModification(p);
            InputContactForm(newContact);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }
        public ContactHelper Modify(ContactData contact, ContactData newContact)
        {
            SelectContact(contact.Id);
            InitContactModify(contact.Id);
            InputContactForm(newContact);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactsCashe = null;
            return this;
        }

        public ContactHelper InitContactModification(int p)
        {

            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (p + 1) + "]")).Click();
            return this;
        }
        public ContactHelper InitContactModify(string id)
        {
            driver.FindElement(By.Id(id)).FindElement(By.XPath("(//img[@alt='Edit'])")).Click();
            return this;
        }



        public ContactHelper RemoveContact(int p)
        {
            SelectContact(p);
            DeleteContact();
            CloseAlert();
            return this;
        }

        public ContactHelper RemoveContact(ContactData contact)
        {
            SelectContact(contact.Id);
            DeleteContact();
            CloseAlert();
            return this;
        }


        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactsCashe = null;
            return this;
        }
        public ContactHelper SelectContact(int p)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (p + 1) + "]")).Click();
            return this;
        }


        //public ContactHelper SelectContact(string id)
        // {
        //     driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
        //     return this;
        // }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//*[@id='content']//*[@name='submit'][2]")).Click();
            contactsCashe = null;
            return this;

        }
        public ContactHelper InputContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            return this;
        }
        public ContactHelper CreationContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper CloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        public bool IsElementPresent()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmail = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName)
            {
                Lastname = lastName,
                Address = address,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int p)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName)
            {
                Lastname = lastName,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3

            };
        }

        public ContactData GetContactInformationFromPersonForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitWatchContactInformation(0);

            string allContactInformation = driver.FindElement(By.Id("content")).Text;

            return new ContactData(allContactInformation)
            {
                AllContactInformation = allContactInformation
            };
        }

        public void InitWatchContactInformation(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            //из текста вычленяем число
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value); //после получения текста преобразовываем в число
        }

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
              .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
        public ContactHelper RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            DoGroupFilter(group);
            SelectContact(contact.Id);
            CommitRemoveContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).
                   Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;

        }

        private void CommitRemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void DoGroupFilter(GroupData group)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(group.Name);
        }

    }
}



