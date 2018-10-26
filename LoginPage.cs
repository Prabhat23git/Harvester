using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using Cat.Automation.UI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Cat.Automation.UI.PageCSFile
{
    public class LoginPage
    {
        public static IWebDriver Driver;
        public static ILog LOGGER = LogManager.GetLogger(typeof(LoginPage));
        //[FindsBy(How = How.Id, Using = "submitButton")]
        //public IWebElement BtnLogin { get; set; }



        [FindsBy(How = How.XPath, Using = "//input[@value ='Log In']")]
        public IWebElement BtnLogin { get; set; }

        [FindsBy(How = How.Name, Using = "cwsUID")]
        public IWebElement TxtCWSId { get; set; }

        [FindsBy(How = How.Name, Using = "cwsPwd")]
        public IWebElement TxtCWPswd { get; set; }

        [FindsBy (How= How.XPath,Using = "//div[@id = 'login_status'][@class= 'error-text']")]
        public IWebElement Msg_InvalidLogin{get;set;}

        [FindsBy(How = How.XPath, Using = "//*[@id='providerList']/div/label")]
        public IWebElement Dropdown { get; set; }

        public void Login(String userName, String password)
        {
            //  PropertiesCollection.WebText(TxtCWSId, userName); // by using properties collection item
            // PropertiesCollection.HighLightElement(BrowserFactory.Driver, TxtCWSId); 
           // PropertiesCollection.Wait_ElementToBeClickable(TxtCWSId, 30);
            TxtCWSId.Wait_ElmToBeDisplayed(30);
            Assert.AreEqual(true, TxtCWSId.Displayed, "Field Username is displayed");
            Assert.AreEqual(true, TxtCWPswd.Displayed, "Field Username is displayed");
            Assert.AreEqual(true, BtnLogin.Displayed, "Field Username is displayed");

            TxtCWSId.HighLightElement();
            TxtCWSId.EnterText(userName);
            LOGGER.Info("Username has been Entered");
            TxtCWPswd.HighLightElement();
            TxtCWPswd.EnterText(password);
            LOGGER.Info("Password has been Entered");
            System.Threading.Thread.Sleep(2000);
            BtnLogin.HighLightElement();
            BtnLogin.WebClick();
            LOGGER.Info("CWS login button has been clicked");
           // System.Threading.Thread.Sleep(5000);
            //Dropdown.Wait_ElmToBeDisplayed();
            Dropdown.Wait_ElmToBeDisplayed(30);
        }
       
        public void verify_invalidlogin()
        {
            Msg_InvalidLogin.Wait_ElmToBeDisplayed(30);
            string text = Msg_InvalidLogin.GetText();
            Assert.AreEqual("Invalid User ID or password.", text.Trim(), "Ïnvalid UserName or Paswd");
        }

    }
}
