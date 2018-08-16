using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests1
{
    class Circle : Figure // : Figura означает что класс Circle наследует класс Figure, т.е она обладает всеми свойствами класса фигура и владеет собственными свойствами
    {
        private int radius;
        //проверяем закрашен ли наш объект или нет
        
        //создаем конструктор (название как у класса) для указания точного размера
        public Circle(int radius)
        {
            this.radius = radius;
        }
        //определяем проперти (для) сокращения конструкции get/set)
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value; //заменяем size на value, это и есть неявная переменная через которую передаются значения. Убираем this так как конфликтов с size больше не возникает
            }
        }
        
    }
}
