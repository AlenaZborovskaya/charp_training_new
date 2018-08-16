using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests1
{
    class Figure
    {
        private bool colored = false;

        //делаем свойства
        public bool Colored
        {
            get
            {
                return colored;
            }
            set
            {
                colored = value; //заменяем size на value, это и есть неявная переменная через которую передаются значения. Убираем this так как конфликтов с size больше не возникает
            }
        }
    }
}
