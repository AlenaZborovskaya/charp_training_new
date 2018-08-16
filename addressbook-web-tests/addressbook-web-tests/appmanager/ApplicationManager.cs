using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;



namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        //перенесли также из TestBase, так как в тестах не используется, драйвер только запускает и выключает браузер
        protected IWebDriver driver;
        protected string baseURL;

        //скопировали ссылки на помощников из TestBase
        protected LoginHelper loginHelper; //первое, делаем LoginHelper, первая l маленькая
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;

        

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();// это объект который будет устанавливать соответствие между текущим потоком и объектом типа ApplicationManager


        //иницилизируем помощников в конструкторе
        private ApplicationManager() // private означает что за пределами класса никто не сможет сконструировать объекты    
        {
        FirefoxOptions options = new FirefoxOptions();
        options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
        options.UseLegacyImplementation = true;
        driver = new FirefoxDriver(options);

        baseURL = "http://localhost/";
       


        loginHelper = new LoginHelper(this); //добавляем код который будет создавать помощников, в качестве параметра - driver, первая l маленькая
        navigationHelper = new NavigationHelper(this, baseURL);
        groupHelper = new GroupHelper(this);
        contactHelper = new ContactHelper(this);
        }

        //Делаем деструктор, чтобы заменить stop application manager: ~название класса
         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance() //static означает что метод также глобальный и может быть вызван не только в каком либо объекте, по имени ApplicationManager.GetInstance
        {
            if (! app.IsValueCreated ) //  
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.OpenHomePage();
                app.Value = newInstance;
            }

            return app.Value;
        }

        //Создаем метод для стопа драйвера (который мы перенесем из TestBase) чтобы остановить все в менеджере

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }


        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public NavigationHelper Navigator
        {
            get
            {
                return navigationHelper;
            }
        }
        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }
      
    }
}
