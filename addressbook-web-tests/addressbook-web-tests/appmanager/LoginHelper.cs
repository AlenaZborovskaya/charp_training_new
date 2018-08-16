using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

// 1. объявляем все классы public
// 2. так как код не компилируется: первое, добавляем конструкции using (испортируем классы из другого пространства имен -селениум)
// второе, исправляем ссылку на driver, создаем для него конструктор
// далее в TestBase используем место перенеенного метода помощник

namespace WebAddressbookTests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) // конструктор для driver, IWebDriver - параметр
        : base(manager)// обращаемся к конструктору base, в качестве параметра driver (когда создали HelperBase)

        {
        }    
        public void Login(AccountData account)
        {
            //проверяем, если логин уже выполнен, то повторно делать не надо
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
            
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        

        
        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }
        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                //делаем вспомогательный метод который извлекает из интерфейса имя пользователя который залогинен
                && GetLoggetUserName() == account.Username;
                
        }

        public string GetLoggetUserName()
        {
           string text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
           return text.Substring(1, text.Length - 2);     //отрезается первый и последний символ
        }
    }
}
