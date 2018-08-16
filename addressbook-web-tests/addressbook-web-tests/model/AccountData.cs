using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
   public class AccountData
    {
        private string username;
        private string password;

        //добавляем конструктор, чтобы реконструировать код в одну строчку
        public AccountData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        //делаем свойства
        public string Username
        {
            get  //возвращает имя
            {
                return username;
            }
            set // присваивает имя
            {
                username = value;
            }
        }
        public string Password
        {
            get  //возвращает имя
            {
                return password;
            }
            set // присваивает имя
            {
                password = value;
            }
        }
    }
}
