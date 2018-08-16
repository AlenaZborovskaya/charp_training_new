using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace addressbook_web_tests1
{
    [TestClass]
    public class Untitled
    {
        [TestMethod]
        public void TestMethodSquare()
        {
            //Задаем объекты
            Square s1 = new Square(5);
            Square s2 = new Square(10);
            Square s3 = s1;
            //Проверяем что размер квадрата равен 5 , 10, 5
            Assert.AreEqual(s1.Size, 5);
            Assert.AreEqual(s2.Size, 10);
            Assert.AreEqual(s3.Size, 5);

            //Для меняющегося размера
            s3.Size = 15;
            //проверяем что у элемента на который ссылается s3 размер тоже15
            Assert.AreEqual(s1.Size, 15);

            s2.Colored = true;
        }

        [TestMethod]
        public void TestMethodCircle()
        {
            //Задаем объекты
            Circle s1 = new Circle(5);
            Circle s2 = new Circle(10);
            Circle s3 = s1;
            //Проверяем что размер квадрата равен 5 , 10, 5
            Assert.AreEqual(s1.Radius, 5);
            Assert.AreEqual(s2.Radius, 10);
            Assert.AreEqual(s3.Radius, 5);

            //Для меняющегося размера
            s3.Radius = 15;
            //проверяем что у элемента на который ссылается s3 размер тоже15
            Assert.AreEqual(s1.Radius, 15);

            s2.Colored = true;
        }
    }
}
   
     
   