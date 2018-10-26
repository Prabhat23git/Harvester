using System;
using TechTalk.SpecFlow;
using Cat.Automation.UI.Utilities;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
//using OpenQA.Selenium.Support.PageObjects;
using Cat.Automation.UI.PageCSFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SeleniumExtras.PageObjects;

namespace Cat.Automation.UI.StepDefination
{
    [Binding]
    [Scope(Feature = "UI_Verify_LoginScreen")]
    [Scope(Feature = "UI_INT_Verify_LoginScreen")]

    public class LoginPageStep
    {
        private static Configuration appConfig;
        public static string HarvesterUI_Url;
        public static string Browser;
        static LoginPageStep()
        {
            appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            HarvesterUI_Url = appConfig.AppSettings.Settings["HarvesterUI_Url"].Value;
            Browser = appConfig.AppSettings.Settings["Browser"].Value;
        }
 
        [Given(@"User launched the HarvesterUI URL")]
        public void GivenUserLaunchedTheHarvesterUIURL()
        {
            try
            {                
                BrowserFactory.InitBrowser(ConfigurationManager.AppSettings["Browser"]);
                Console.WriteLine("Browser has been launched");
                BrowserFactory.LoadApplication(ConfigurationManager.AppSettings["HarvesterUI_Url"]);
                Console.WriteLine("URL has been launched");

            }

            catch (Exception e)
            {
                if (CustomisedException.getErrorMessage() != null)
                {
                    PropertiesCollection.takeSnapShot("LoginpageLaunchURL");
                    Assert.Fail(CustomisedException.GetFieldValue() + " :" + CustomisedException.getErrorMessage());
                }
                else
                {

                    PropertiesCollection.takeSnapShot("LoginpageLaunchURL");
                    Assert.Fail(e.ToString());


                }

            }
        }
        
        [When(@"User Entered the Login Details for Authroized User (.*) and (.*)")]
        public void WhenUserEnteredTheLoginDetailsForAuthroizedUser(string username, string Password)
        {        
            try
            {
                var login = new PageCSFile.LoginPage();
                PageFactory.InitElements(BrowserFactory.Driver, login);
                login.Login(username, Password);               
               // PropertiesCollection.WaitForPageLoaded(30);
                Console.WriteLine("Entered the login1 : AuthroizedRepeated User details");


            }
            catch (Exception e)
            {
                if (CustomisedException.getErrorMessage() != null)
                {
                    PropertiesCollection.takeSnapShot("LoginpageAuthroizedRepeated User");
                    Assert.Fail(CustomisedException.GetFieldValue() + " :" + CustomisedException.getErrorMessage());
                }
                else
                {

                    PropertiesCollection.takeSnapShot("LoginpageAuthroizedRepeated User");
                    Assert.Fail(e.ToString());


                }

            }
        }
        
        [Then(@"HarvesterUI HomePage screen should be displayed")]
        public void ThenHarvesterUIHomePageScreenShouldBeDisplayed()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();          
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            //FeedConfig.Verify_ProviderList();
            FeedConfig.verify_ErrorCount_message();
            FeedConfig.Verify_feedConfigrationPage_Loaded();
            FeedConfig.FilterAndVerify2ndPage("Manufacture Desc","PRA");
            
        }

        [When(@"User Entered the Login Details for UnAuthenticated User (.*) and (.*)")]
        public void WhenUserEnteredTheLoginDetailsForUnAuthenticatedUser(string username, string Password)
        {
            try
            {
                var login = new PageCSFile.LoginPage();
                PageFactory.InitElements(BrowserFactory.Driver, login);
                login.Login(username, Password);
                Console.WriteLine("Entered the login1 : UNAuthroized User details");


            }
            catch (Exception e)
            {
                if (CustomisedException.getErrorMessage() != null)
                {
                    PropertiesCollection.takeSnapShot("LoginpageAuthroizedRepeated User");
                    Assert.Fail(CustomisedException.GetFieldValue() + " :" + CustomisedException.getErrorMessage());
                }
                else
                {

                    PropertiesCollection.takeSnapShot("LoginpageAuthroizedRepeated User");
                    Assert.Fail(e.ToString());


                }

            }
        }

        [Then(@"HarvesterUI HomePage screen should not displayed")]
        public void ThenHarvesterUIHomePageScreenShouldNotDisplayed()
        {
            var Login = new PageCSFile.LoginPage();
            PageFactory.InitElements(BrowserFactory.Driver, Login);
            Login.verify_invalidlogin();
        }
    }
}
