using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


// 1.пространство имен делаем общим,  объявляем все классы public
// 2. так как код не компилируется: первое, добавляем конструкции using (испортируем классы из другого пространства имен -селениум)
// второе, исправляем ссылку на driver, создаем для него конструктор
// далее в TestBase используем место перенеcенных методов помощники

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) // конструктор для driver, IWebDriver - параметр
          : base(manager)// обращаемся к конструктору base, в качестве параметра driver (когда создали HelperBase)
        {
            this.baseURL = baseURL;
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }

        public void OpenHomePage()
        {
            if (driver.Url == baseURL + "addressbook/")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
        }
        public void GoToGroupPage()
        {
            
                driver.FindElement(By.LinkText("groups")).Click();
        }

        public void ReturnToGroupPage()
        {
            if (driver.Url == baseURL + "addressbook/group.php"
                   && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("group page")).Click();
        }
        public
            void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "addressbook/group.php"
                    && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();

        }
    }
}