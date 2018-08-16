using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests1.others
{
    [TestClass]
    public class циклы
    {
        [TestMethod]
        public void old()
        {
            //string s = "test"; - так объявляется 1 переменная в виде строки , если с [] то объявляется массив переменных
           // string[] s = new string[] { "I", "want", "to", "sleep" };
            // в качестве параметра - счетчик. Начинаем с 0 до 10, int++ на каждой итерации значение увел на 1
            //for (int i = 0; i < s.Length; i= i+1)
            {
                //System.Console.Out.Write(s[i] + "\n");
            }
                
        }
       [TestMethod]
        public void newconstruction()
        {
            
            //string[] s = new string[] { "I", "want", "to", "sleep" };
            //для каждого элемента массива выполнить действие
            //foreach (string element in s)
            {
                //System.Console.Out.Write(element + "\n");
            }

        }
        public void newconstruction1()
        {
            //проверяем есть ли на странице нужный элемент или нет
            IWebDriver driver = null;
            //задаем количество попыток
            int attempt = 0;
            
            while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 60)
            {
                System.Threading.Thread.Sleep(1000);
                attempt = attempt + 1;
            }
            //или:
            do
            {
                System.Threading.Thread.Sleep(1000);
                attempt = attempt + 1;
            }
            while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 60);
        }
    }
}

