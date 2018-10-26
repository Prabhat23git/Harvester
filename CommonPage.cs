using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using Cat.Automation.UI.PageCSFile;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace Cat.Automation.UI.Utilities
{
    [Binding]
    public static class TestRunSingleBrowserHooks
    {
        public static ExtentReports extent;
        public static ExtentTest featureName;
        public static ExtentTest scenario;
        public static ExtentHtmlReporter htmlReporter;
        public static string UserDir;        

       // [BeforeTestRun]
        [BeforeScenario] 
        public static void RegisterPages()
        {
            BrowserFactory.InitBrowser(ConfigurationManager.AppSettings["Browser"]);
            Console.WriteLine("Browser has been launched");
            BrowserFactory.LoadApplication(ConfigurationManager.AppSettings["HarvesterUI_Url"]);
            Console.WriteLine("URL has been launched");

            var login = new PageCSFile.LoginPage();
            PageFactory.InitElements(BrowserFactory.Driver, login);
            login.Login("kumarp22", "ANVI2@gungun");
           
            // PropertiesCollection.WaitForPageLoaded(30);
            Console.WriteLine("Entered the login1 : AuthroizedRepeated User details");

        }
        [AfterScenario]
       // [AfterTestRun]
        public static void AfterTestRun()
        {
            
            BrowserFactory.CloseBrowser();
            Feedconfiguration.count = 0;
            Feedconfiguration.Error_RegisterAsset_Flag = false;
            
        }

        [BeforeTestRun]
        public static void Generate_Report()
        {
            UserDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            htmlReporter = new ExtentHtmlReporter(UserDir + "\\Reports\\Automation_Report_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".html");

            htmlReporter.LoadConfig(UserDir + "\\extent-config.xml");

            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

        }

        [AfterTestRun]
        public static void After_TestRun()
        {
            extent.Flush();
        }
        [AfterStep]
        public static void Configure_Report()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }

            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                   // scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

            }
            
        }
        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        } 
       
    }
    




    public class CommonPage
    {
        private static Configuration appConfig;
        public static string salt;
      //  string pwd = DecryptRijndael("Password", salt);
        static CommonPage()
        {
            appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            salt = appConfig.AppSettings.Settings["RjnSalt"].Value;
        }


        public string DecryptRijndael(string cipherText)
        {
            //if (string.IsNullOrEmpty(cipherText))
            //    throw new ArgumentNullException(nameof(cipherText));

            if (!IsBase64String(cipherText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var aesAlg = NewRijndaelManaged();

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            var cipher = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }

            return text;
        }
        private static RijndaelManaged NewRijndaelManaged()
        {
            var inputkey = ConfigurationManager.AppSettings["RjnKey"];

            //if (salt == null)
            //    throw new ArgumentNullException(nameof(salt));  
            var saltBytes = Encoding.ASCII.GetBytes(salt);

            var key = new Rfc2898DeriveBytes(inputkey, saltBytes);

            var aesAlg = new RijndaelManaged();

            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }
        private static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();

            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        } 
    }
}
