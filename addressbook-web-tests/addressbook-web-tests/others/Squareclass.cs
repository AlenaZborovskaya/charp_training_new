using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests1
{
    class Square : Figure
    {
        private int size;
       
        //создаем конструктор (название как у класса) для указания точного размера
        public Square(int size)
        {
            this.size = size;
        }
        //определяем проперти (для) сокращения конструкции get/set)
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value; //заменяем size на value, это и есть неявная переменная через которую передаются значения. Убираем this так как конфликтов с size больше не возникает
            }
        }
      
    }
 }