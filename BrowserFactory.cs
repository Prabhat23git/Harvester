using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
namespace Cat.Automation.UI.Utilities
{
   public class BrowserFactory
    {
        public static IWebDriver Driver;
        public static object Drivers { get; private set; }


        public static void InitBrowser(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                    Driver = new FirefoxDriver();
                 break;

                case "ie":

                  // Driver = new InternetExplorerDriver(@"C:\Users\kr4\source\repos\LD\packages\Selenium.InternetExplorer.WebDriver.3.8\driver");
                    Driver = new InternetExplorerDriver();
                 break;

                case "chrome":
                    var options = new ChromeOptions();
                    options.AddUserProfilePreference("download.prompt_for_download", true);

                    Driver = new ChromeDriver(options);
                    //Driver = new ChromeDriver(@"C:\Users\kr4\source\repos\LD\packages\Selenium.Chrome.WebDriver.2.31\driver");

//                    var chromeOptions = new ChromeOptions();
// chromeOptions.AddUserProfilePreference("download.default_directory", "Your_Path");
// chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
// chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
//var driver = new ChromeDriver("Driver_Path", chromeOptions);


                    break;
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Driver.Manage().Window.Maximize();
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }
        
       public static void CloseBrowser()
        {
            Driver.Close();
        }

    }
}
