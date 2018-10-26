using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
//using SeleniumExtras.WaitHelpers;
using waithelper = SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Drawing.Imaging;
using System.IO;

namespace Cat.Automation.UI.Utilities
{
    public static class PropertiesCollection
    {
        public static IWebDriver Driver;
        //CustomisedException obj;
        public static ILog LOGGER = LogManager.GetLogger(typeof(PropertiesCollection));

        public static void EnterText(this IWebElement element, String text)
        {
            try
            {
                Actions actions = new Actions(BrowserFactory.Driver);
                actions.MoveToElement(element);
                actions.Click();
                element.Clear();
                element.SendKeys(text);

            }
            catch (Exception e)
            {}
        }

        public static bool Wait_ElmToBeDisplayed(this IWebElement element, int sec)
        {
            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(sec));
            return wait.Until(d =>
            {
                try
                {                   
                    //element.HighLightElement();

                    return element.Displayed;
                }
                catch (ElementNotVisibleException)
                {
                    return false;
                }

            }
            );

        }


        //public static bool Wait_ElmNotDisplayed(this IWebElement element, int sec = 50)
        //{
        //    var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(sec));
        //    return wait.Until(d =>
        //    {
        //        try
        //        {

        //            return element.Displayed;
        //        }
        //        catch (ElementNotVisibleException)
        //        {
        //            return false;
        //        }

        //    }
        //    );

        //}

        public static void Wait_ElementToBeClickable(IWebElement element, int timeout)
        {
            try
            {
                Actions actions = new Actions(BrowserFactory.Driver);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(waithelper.ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception e)
            { }
        }


        //public static IWebElement WaitUntilElementVisible(by element, int timeout)
        //{
        //    try
        //    {
        //        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
        //        return wait.Until(waithelper.ExpectedConditions.ElementIsVisible(elementLocator));
        //    }
        //    catch (NoSuchElementException)
        //    {
        //        Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
        //        throw;
        //    }
        //}

        public static void Wait_ElementToBeExists(IWebElement element, int timeout)
        {
            try
            {
                Actions actions = new Actions(BrowserFactory.Driver);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(waithelper.ExpectedConditions.ElementExists(By.XPath("//*[@id='providerList']/div/label")));


             
            }
            catch (Exception e)
            { }
        }

        public static void fluentWait(String element, int timeout)
        {
            try
            {
             

                DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(Driver);
                fluentWait.Timeout = TimeSpan.FromSeconds(timeout);
                fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.Until(x => x.FindElement(By.XPath(element)).Displayed);
               // IWebElement searchResult = fluentWait.Until(x => x.FindElement(By.XPath(element))).Displayed();
                //string text = Driver.FindElement(By.XPath("//div[contains(text(),'Error']")).GetText();
                
            }
            catch (Exception e)
            {
                bool a;
            }
        }

        public static void WaitForPageLoaded(int timeout)
        {
            //TimeSpan timeout = new TimeSpan(0, 0, 30);
            WebDriverWait wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeout));

            IJavaScriptExecutor javascript = Driver as IJavaScriptExecutor;
            //if (javascript == null)
            //    throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                    "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }

                catch (Exception)
                {
                    return false;
                }

            });
        }
      

        public static void HighLightElement(this IWebElement element)
        {
            IWebDriver driver = BrowserFactory.Driver;
            try
            {
                var javaScriptDriver = (IJavaScriptExecutor)driver;
                string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 3px; border-style: solid; border-color: red"";";
                javaScriptDriver.ExecuteScript(highlightJavascript, new object[] { element });
            }
            catch(Exception e)
            {
                
            }
        }
      

        public static void SelectDropDown(this IList<IWebElement> elements, String data)
        {
            try
            {
                foreach (IWebElement option in elements)
                { 
                    String text = option.Text.ToString();
                    if (text.ToUpper().Trim().Replace(" ", "").Equals(data.ToUpper().Trim().Replace(" ", "")))
                    {
                        option.Click();
                        break;
                    }
                }
              
            }
            catch (Exception e)
            {


            }

        }

     public static void WebClick(this IWebElement element)
		{
			try
			{
				element.Click();
			}
			catch (Exception e)
			{
				javaScriptClick(element);

			}
		}


		public static void javaScriptClick(IWebElement element)
		{
			try
			{
				IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.Driver;
				executor.ExecuteScript("arguments[0].click();", element);

			}
			catch (Exception e)
			{

			}

		}
        public static void takeSnapShot(String pageName)
        {

            try
            {

                ITakesScreenshot screenshotDriver = BrowserFactory.Driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                String fp = @"C:\Screenshots\HarvesterUI" + "_" + DateTime.Now.ToString("dd_MMMM_hh_mm_ss_tt") + ".png";
                String filename = Path.Combine(pageName, fp);
                //String filename = pageName + fp;
                screenshot.SaveAsFile(filename);
                LOGGER.Info("TestCase Failed and Screenshot Captured ");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

        }


        public static void SiteSelectionDropdown()
        {
            try
            {
                IList<IWebElement> list = BrowserFactory.Driver.FindElements(By.XPath("//ul[contains(@class,'dropdown')]//li"));
                
                foreach (IWebElement option in list)
                {
                    String text = option.Text.ToString();

                    if (text.Equals("Volvo"))
                    {
                        option.Click();
                        break;
                    }
                }
            }
            catch (Exception e)
            { }
        }
        public static string  GetText(this IWebElement elemnt)
        {
            string Text = "";
            try {
                Text =  elemnt.Text.ToString(); 
              }
            catch (Exception e)
            { }

            return Text;     
        }

//        public String getTextAt(String cssId)
//        {
//    if (IWebDriver instanceof JavascriptExecutor) {
//        JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
//        return (String) javascriptExecutor.executeScript("return documenet.getElementById('" + cssId + "').value;");
//    }
//    return null;
//}

    }  
}
