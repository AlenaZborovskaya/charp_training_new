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
    public class HelperBase
    {
        protected ApplicationManager manager;
        protected IWebDriver driver; //перенесли ссылку на драйвер, сделали ее protected

        public HelperBase(ApplicationManager manager) //делаем конструктор 
        {
            this.manager = manager;
            this.driver = manager.Driver; //на вход принимает ссылку на браузер которым мы управляем драйвером и присваивает ее поле

        }
        public void Type(By locator, string text) // перенесли из group helper так как не только он заполняет  форму
        {
            //если текст не null то заполняем поле, иначе ничего делать не нужно
            if (text != null) // == оператор который позволяет сравнивать, != не равно
            {
                driver.FindElement(locator).Clear();// заполнить поле тем значением, которое переданно в качестве параметра
                driver.FindElement(locator).SendKeys(text);
            }
            else
            {
                // то ничего не делать, else вообще можно убрать
            }
        }
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
    }
}
     

