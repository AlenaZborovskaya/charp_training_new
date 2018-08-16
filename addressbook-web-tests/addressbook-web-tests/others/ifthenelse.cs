using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace addressbook_web_tests1.tests
{
    [TestClass]
    public class ifthenelse
    {
        [TestMethod]
        public void TestMethod1()
            //правила по которым вычисляется скидка, если купил товаров больше 1000 р то скидка 10%
        {
            double total = 999; //объявляем переменную тотал
            bool vipClient = true; 
            //второе условие, что вип клиент
           if (total > 1000 || vipClient) // && это и то и то, || или то или то
            {
                total = total * 0.9; // считаем скидку
                System.Console.Out.Write("скидка 10% общая сумма " + total);//добавляем вывод на консоль
            }
            else
            {
               System.Console.Out.Write("скидки нет общая сумма " + total);//добавляем вывод на консоль

            }
        }
    }
}
