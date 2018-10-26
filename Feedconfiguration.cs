using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
//using SeleniumExtras.WaitHelpers;
using waithelper = SeleniumExtras.WaitHelpers;
using Cat.Automation.UI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Interactions;

namespace Cat.Automation.UI.PageCSFile
{
    public  class Feedconfiguration : DB_Data
    {
        CommonPage CommonMethod = new CommonPage();
        //static Feedconfiguration()
        //{
        //   // var FeedConfig = new PageCSFile.Feedconfiguration();
        //    PageFactory.InitElements(BrowserFactory.Driver, this);
        //}
       
        List<ServiceProvider> DBData_ListFeed = new List<ServiceProvider>();
        public static IWebDriver Driver;
        public static ILog LOGGER = LogManager.GetLogger(typeof(Feedconfiguration));  
        [FindsBy(How = How.Id, Using = "setProvider")]
        public IWebElement BtnSetProvider { get; set; }
        [FindsBy(How = How.XPath, Using = "//*[@id='providerList']/div/label")]
        public IWebElement Dropdown { get; set; }
        [FindsBy(How = How.XPath, Using = "//ul[contains(@class,'dropdown')]//li")]
        public IList<IWebElement> Dropdown_List { get; set; }
        [FindsBy(How = How.XPath, Using = "//label[contains(@class,'ui-radiobutton-label')]")]
        public IWebElement Feed_ProviderSchema { get; set;}

        [FindsBy(How = How.XPath, Using = "//label[contains(@class,'ui-radiobutton-label')]")]
        public IList<IWebElement> Feed_ProviderSchema_All { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'URL')]//parent::div//div//label")]
        public IWebElement Feed_URL { get; set; }

        //label[contains(text(),'URL')]//parent::div//div//label
        [FindsBy(How = How.Id, Using = "userName")]
        public IWebElement UserName {get;set;}
        [FindsBy(How= How.Id, Using = "password")]
        public IWebElement Password {get;set;}
        [FindsBy(How = How.Id, Using = "url")]
        public IWebElement URL_Otherfeed { get; set; }
        [FindsBy(How = How.Id, Using = "feedName")]
        public IWebElement OptionalName {get;set;}
        [FindsBy(How =How.XPath, Using = "//button[text()= 'CANCEL']")]
        public IWebElement BtnCancel{get;set;}
        [FindsBy(How =How.XPath, Using = "//button[text()= 'SAVE']")]
        public IWebElement BtnSave{get;set;}


        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Success')]")]
        public IWebElement Success_Msg{get;set;}

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Info']")]
        public IWebElement AddedFeed_Msg { get; set; }

        //[FindsBy(How = How.XPath, Using = "//div[contains(text(),'Error']")]
        //public IWebElement DuplicateFeed_Msg { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[contains(@class,'toast-message')]")]
        public IWebElement DuplicateFeed_Msg { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[contains(@class,'toast-message')]")]
        public IWebElement URLErrorMsg { get; set; }

        //[FindsBy(How = How.XPath, Using = "//div[contains(text(),'Exceeding a maximum of 255 characters')]")]
        //public IWebElement URLErrorMsg { get; set; }

        DB_Data data = new DB_Data();
        DBFeed_URLSchema DataFeed_URLSchema = new DBFeed_URLSchema();
        FeedConfig_DBModel FeedConfig_SavedDBData = new FeedConfig_DBModel();
        public static string Entered_Username = "";
        public static string Entered_Password = "";
        public static string Entered_OPtionalName = "";
        public static string Entered_Url = "";
        public static string provider = "";
        


        // Right Pane Elements:----------------------------------------------------------------------
        public static string App_URL = "";
        public static string TxtConfigured_Username = "";
        public static string TxtConfigured_Url = "";
        public static string TxtConfigured_Paswd = "";
        public static string TxtConfigured_Provider_OptionalName = "";
        public static string TxtConfigured_OptionalName = "";

        public static string Username_BeforeDiscard = "";
        public static string Password_BeforeDiscard = "";
        public static string URL_BeforeDiscard = "";
        public static string Updated_Username = "";
        public static string Updated_Password = "";
        public static string URL_BeforeUpdate = "";
        public static int ID_ConfiguredUsername;
        public static bool UpdateConnection_Flag = true;
        public static bool TestConnection_Flag = false;
        public static bool VerifyTestConnection_status = false;
        public static bool Error_RegisterAsset_Flag = false;
        public static string DuplicateMsg_ServiceProviderDataId = "";
        public static int count = 0;

        public static string ServiceproviderName = "";
        [FindsBy(How = How.XPath, Using = "//span[@class= 'ng-star-inserted']")]
        public IWebElement Txt_OptionalName { get; set; }

         
        //[FindsBy(How = How.XPath, Using = "//span[@class= 'contain-title']")]
        [FindsBy(How = How.XPath, Using = "(//span[@class='hEllipsed'])[1]")]
        public IWebElement Txt_ContainTitle { get; set; }

        [FindsBy(How = How.XPath, Using = "(.//*[contains(@class,'btn hbtn-yellow')])[1]")]
        public IWebElement BtnStatus_Update { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='hEllipsed']")]
        public IList<IWebElement> ALLAvailableProvider_Txt_ContainTitle { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[contains(@class,'btn hbtn-yellow')]")]
        public IList<IWebElement> ALLAvailableProvider_BtnStatus_Update { get; set; }

       
        [FindsBy(How = How.XPath, Using = "//button[text()=' Test Connection']")]
        public IWebElement BtnTest_Connection { get; set; }
        [FindsBy(How = How.XPath, Using = "//button[text()='Update Connection']")]
        public IWebElement BtnUpdate_Connection { get; set; }
        [FindsBy(How = How.XPath, Using = "//button[text()='Discard Changes']")]
        public IWebElement BtnDiscard_Changes { get; set; }
        [FindsBy(How = How.XPath, Using = "//button[text()='Register Asset']")]
        public IWebElement BtnRegister_Asset { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[text()='URL: ']//following::label[1]")]
        public IWebElement Configured_URL { get; set; }
        [FindsBy(How = How.XPath, Using = "//label[text()='Username: ']//following::input[1]")]
        public IWebElement Configured_UserName { get; set; }
        [FindsBy(How = How.XPath, Using = "//label[text()='Password: ']//following::input[1]")]
        public IWebElement Configured_Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Test failed')]")]
        public IWebElement Msg_FailedTestconnection { get; set; }
        
        [FindsBy(How = How.XPath, Using = "(//*[contains(@class,'icon-close_red')])[1]")]
        public IWebElement Icn_FailTestconnection { get; set; }
        //[FindsBy(How = How.XPath, Using = "(//*[contains(@class,'icon-tick_green')])[3]")]
        //public IWebElement Icn_PassTestconnection { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class,'icon-tick_green')]")]
        public IWebElement Icn_PassTestconnection { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Error downloading assets')]")]
        public IWebElement MsgErrorDownloadingassets { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'feed details already exists')]")]
        public IWebElement MsgFeedDetails_AlreadyExists { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'cannot be null')]")]
        public IWebElement Msg_UNamePaswd_CannotbeNull { get; set; }

        //Added Temporary for 2nd Page

        [FindsBy(How = How.XPath, Using = "(//li[@routerlinkactive ='active'][@class='active'])[2] ")]
        public IWebElement PageNumber { get; set; }
        bool pagedisplayed = false;

       public static int ColumnNo = 0;
       public static string ColumnName = "";

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Manufacture Desc')]//following::label[contains(text(),'Filter')][1]")]
        public IWebElement EleFilter_ManufactureDesc { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Dealer')]//following::label[contains(text(),'Filter')][1]")]
        public IWebElement EleFilter_Dealer { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Business Unit')]//following::label[contains(text(),'Filter')]")]
        public IWebElement EleFilter_BusinessUnit { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Make Code')]//following::label[contains(text(),'Filter')][1]")]
        public IWebElement EleFilter_MakeCode { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Asset ID')]//following::label[contains(text(),'Filter')][1]")]
        public IWebElement EleFilter_AssetID { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Asset Model')]//following::label[contains(text(),'Filter')][1]")]
        public IWebElement EleFilter_AssetModel { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Model Year')]//following::label[contains(text(),'Filter')][1]")]
        public IWebElement EleFilter_ModelYear { get; set; }

         [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Asset VIN')]//following::label[contains(text(),'Filter')]")]
        public IWebElement EleFilter_AssetVIN { get; set; }

         [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Error')]")]
         public IWebElement EleErrorCount { get; set; }

         [FindsBy(How = How.XPath, Using = "//div[@class  = 'ui-overlaypanel-content']/span")]
         public IList<IWebElement> TxtErrorMsg { get; set; }
         
         
        public void Verify_feedConfigrationPage_Loaded()
        {
           //Dropdown.Wait_ElmToBeDisplayed();
           Assert.AreEqual(true, Dropdown.Displayed, "Set Provider Page is displayed");

            //enable below two line
            System.Threading.Thread.Sleep(10000);
           Dropdown.WebClick();
            string pageno = PageNumber.GetText();
            try { 
                if(pageno == "2")
                {
                     pagedisplayed = true;
                }
                else Console.WriteLine("Apply DC/DCN Page is not displayed");
            }
            catch { Exception e; }
            Assert.AreEqual(true,pagedisplayed,"Apply DC/DCN Page is displayed");
            
          }

        public void verify_ErrorCount_message()
        {
            int i=0;
            string c = "4";
            string countmsg = EleErrorCount.GetText();
            Assert.AreEqual(true, countmsg.Contains(c), "Count Matches");
            Actions actions = new Actions(BrowserFactory.Driver);
            actions.MoveToElement(EleErrorCount).Build().Perform();
            string[] ArrErrorsmg = new string[2];

            foreach(var msg in TxtErrorMsg )
            {
                 ArrErrorsmg[i] = msg.GetText();
                 i++;
            }
            Assert.AreEqual(true, ArrErrorsmg[1] == "Red = Mandatory Field : Data Missing", "Error Message verified successfully");
            Assert.AreEqual(true, ArrErrorsmg[2] == "Green = Mandatory Field : Data Associated", "Error Message verified successfully");
        }
        //public enum columnnnumber
        //{
        //    Manufacture_Desc = 1,
        //    Dealer =2,
        //    Business_Unit = 3,
        //    Make_Code = 2,
        //    Asset_ID = 2,
        //    Asset_Model = 3,
        //    Model_Year =4,
        //    Asset_VIN =5

        //}
        //public int Columnno(string ColumnName)
        //{
        //   // int ColumnNo = (int)columnnnumber.Manufacture_Desc;
        //    int ColumnNo = (int)columnnnumber.Asset_ID;
        //    return ColumnNo;
        //}
        public void FilterAndVerify2ndPage(string ColumnName, string FilterText)

        {
            bool FlagFilter = false;
            ColumnName = "Make Code";
            FilterText  = "TOY";
            //get column no from column name -2
            ColumnNo = 2;
          IWebElement Txtbox_Srchfilter=  BrowserFactory.Driver.FindElement(By.XPath("(//label[text()= 'Filter']//following::input[@type = 'text'][1])" + "[" + ColumnNo + "]"));
          IList<IWebElement> ListFIlter = BrowserFactory.Driver.FindElements(By.XPath("(//div[contains(@class,'ui-multiselect-items')])[" + ColumnNo + "]/ul/li"));
          IWebElement BtnClosefilter = BrowserFactory.Driver.FindElement(By.XPath("(//a[contains(@class,'ui-multiselect-close')])" + "[" + ColumnNo + "]"));
            switch (ColumnName)
            {
                case "Manufacture Desc":
                    EleFilter_ManufactureDesc.WebClick();                  
                    break;

                case "Dealer":
                    EleFilter_Dealer.WebClick();
                    break;

                case "Business Unit":
                    EleFilter_BusinessUnit.WebClick();
                    break;

                case "Make Code":
                    EleFilter_MakeCode.WebClick();
                    break;

                case "Asset ID":
                    EleFilter_AssetID.WebClick();
                    break;

                case "Asset Model":
                    EleFilter_AssetModel.WebClick();
                    break;

                case "Model Year":
                    EleFilter_ModelYear.WebClick();
                    break;

                case "Asset VIN":
                    EleFilter_AssetVIN.WebClick();
                    break;
                default:
                    break;                    
            }
            Txtbox_Srchfilter.EnterText(FilterText);
            System.Threading.Thread.Sleep(2000);
            foreach (var Ele in ListFIlter)
            {
                string text = Ele.GetText();
                if (text.Contains(FilterText))
                {
                    Ele.WebClick();
                    FlagFilter = true;
                    break;
                }              
            }
            if (FlagFilter == false)
                Assert.Fail("Filter Text is not available");
            System.Threading.Thread.Sleep(1000);
            BtnClosefilter.WebClick();
        }
        //public void FilterAndVerify3rdPage(string ColumnName, string FilterText)
        //{

        //    IWebElement Txtbox_Srchfilter = BrowserFactory.Driver.FindElement(By.XPath("(//label[text()= 'Filter']//following::input[@type = 'text'][1])" + "[" + ColumnNo + "]"));
        //    switch (ColumnName)
        //    {
        //        case "Manufacture Desc":
        //            EleFilter_ManufactureDesc.WebClick();
        //            break;

        //        case "Make Code":
        //            EleFilter_MakeCode.WebClick();
        //            break;

        //        case "Asset ID":
        //            EleFilter_AssetID.WebClick();
        //            break;

        //        case "Asset Model":
        //            EleFilter_AssetModel.WebClick();
        //            break;

        //        case "Model Year":
        //            EleFilter_ModelYear.WebClick();
        //            break;

        //        case "Asset VIN":
        //            EleFilter_AssetVIN.WebClick();
        //            break;
        //        default:
        //            break;
        //    }
        //}


        private  Random random = new Random();
        public  string RandomString(int length)
        {
            const string chars = "https://otptionalurl";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());       
        }
       
        public void click_Dropdown()
        {
           // System.Threading.Thread.Sleep(10000);
            Dropdown.Wait_ElmToBeDisplayed(30);
            Dropdown.HighLightElement();
            Dropdown.WebClick();
        }
        public void Select_Dropdown(string Feed_Provider)
        {
            provider = Feed_Provider;
            Dropdown_List.SelectDropDown(provider);
            //System.Threading.Thread.Sleep(5000);
            Feed_ProviderSchema.Wait_ElmToBeDisplayed(30);
        }
        public void Verify_ProviderList()
        {
            List<string> feed = new List<string>();
         //   System.Threading.Thread.Sleep(10000);            
            IList<IWebElement> list = BrowserFactory.Driver.FindElements(By.XPath("//ul[contains(@class,'dropdown')]//li"));           
            DBData_ListFeed =data.List_Provider();
            feed = data.List_Provider().Select(x=>x.Feed).ToList();
            var result = list.Where(x => !feed.Contains(x.Text)).ToList();
            //var result2 = list.Where(x => !DBData_ListFeed.All(y => y.Feed == x.Text)).ToList();
            //var result3 = list.Where(x => DBData_ListFeed.Any(y => y.Feed == x.Text)).ToList();

            if (result.Count == 0)               

                LOGGER.Info("List Of Feed Provider matches");
            else
                LOGGER.Info("List Of Feed Provider does not matches");

            Assert.AreEqual(result.Count, 0, "List Of Feed Provider matches");
            
        }
        public void UseLAter()
        {
            data.FetchDBData("volvo");
        }

        public void Feed_Configuration (string UName, string Paswd, string optionalName,string Action, string URL)
        {
            Entered_Username = UName;
            Entered_Password = Paswd;
            Entered_OPtionalName = optionalName;
            Entered_Url = URL;
            if (provider != "others")
            App_URL = Feed_URL.GetText();

            Assert.AreEqual(true,UserName.Displayed, "Field Username is displayed");
            Assert.AreEqual(true,Password.Displayed, "Field Username is displayed");
            Assert.AreEqual(true,OptionalName.Displayed, "Field Username is displayed");
            Assert.AreEqual(true,BtnSave.Displayed, "Field Username is displayed");
            Assert.AreEqual(true,BtnCancel.Displayed, "Field Username is displayed");

            UserName.EnterText(UName);
            Password.EnterText(Paswd);
            OptionalName.EnterText(optionalName);
            if (provider == "others")
                URL_Otherfeed.EnterText(URL);

            if (Action == "Save" && UName != "" && Paswd != "")
            {
                BtnSave.WebClick();
               // bool a = Driver.FindElement(By.XPath("//div[contains(text(),'Success: Provider configuration saved'")).Displayed;
                //bool b = Driver.FindElement(By.XPath("//div[contains(text(),'Info: Added to previosly configured feeds'")).Displayed;            
               // System.Threading.Thread.Sleep(5000);
            }

            if (Action == "Cancel")
                BtnCancel.WebClick();
           // System.Threading.Thread.Sleep(5000);

            WebDriverWait wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(15));
            wait.Until(waithelper.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(".//*[contains(@class,'toast-message')]")));
        }

        public void Feed_Configuration_duplicate(string UName, string Paswd, string optionalName, string Action, string URL)
        {
            Entered_Username = UName;
            Entered_Password = Paswd;
            Entered_OPtionalName = optionalName;
            Entered_Url = URL;

            UserName.EnterText(UName);
            Password.EnterText(Paswd);
            OptionalName.EnterText(optionalName);
            if (provider == "others")
                URL_Otherfeed.EnterText(URL);
            if (Action == "Save" && UName != "" && Paswd != "")
            {
                BtnSave.WebClick();
                DuplicateFeed_Msg.Wait_ElmToBeDisplayed(30);
                DuplicateMsg_ServiceProviderDataId = DuplicateFeed_Msg.GetText();
               //bool a = DuplicateFeed_Msg.Displayed;
              // DuplicateFeed_Msg

              // PropertiesCollection.fluentWait("//div[contains(text(),'Error']", 20);

            }

            if (Action == "Cancel")
                BtnCancel.WebClick();
           // System.Threading.Thread.Sleep(5000);
        }
        public void Verify_DuplicateMSg_AndCountDBUsername()
        {
            int UserNameCount = Verify_UserName_Count(Entered_Username);
            Assert.AreEqual(true, UserNameCount == 1, "No duplicate entry was created");
        }

        public void Verify_ExistingServiceProviderDataId_Displayed()
        {
            string ServiceProviderData_id = ServiceProviderDataId_UserName(Entered_Username).ToString();
            Assert.AreEqual(true, DuplicateMsg_ServiceProviderDataId.Contains(ServiceProviderData_id), "Duplicate ServiceProviderDataID is displayed");

        }
       public void VerifyURLErrorMsgDisplayed()
        {
            //WebDriverWait wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(10));
            //wait.Until(waithelper.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(".//*[contains(@class,'toast-message')]")));


            //string text1 = BrowserFactory.Driver.FindElement(By.XPath("//div[contains(text(),'Exceeding a maximum of 255 characters')]")).GetText();
           // PropertiesCollection.Wait_ElmToBeDisplayed(URLErrorMsg);
           //URLErrorMsg.Wait_ElmToBeDisplayed();
            PropertiesCollection.Wait_ElementToBeClickable(URLErrorMsg, 30);
           string text = URLErrorMsg.GetText();
           // Console.Write(text);
            Assert.AreEqual(true, "Error: Url is Null/Empty/Exceeding a maximum of 255 characters" == URLErrorMsg.GetText(),"ÜRL error message verified successfully");
       
        }

        public void Verify_User_FeddConfig_DBData(string username)
        {
            int Uname_maxLength = Math.Min(username.Length, 50);
            int Paswd_maxLength = Math.Min(Entered_Password.Length, 50);
            int OptionalName_maxLength = Math.Min(Entered_OPtionalName.Length, 50);
            int URL_Length = Math.Min(Entered_Url.Length, 50);
            Entered_Username = username.Substring(0, Uname_maxLength);
            Entered_Password = Entered_Password.Substring(0, Paswd_maxLength);
            Entered_OPtionalName = Entered_OPtionalName.Substring(0, OptionalName_maxLength);
            Entered_Url = Entered_Url.Substring(0, URL_Length);

            FeedConfig_SavedDBData = Verify_FeddConfig_DBData(Entered_Username); // Get user feed config data from DB         
           
            Assert.AreEqual(true, FeedConfig_SavedDBData.UserName == Entered_Username, "Successful entry created for username");       
             
            string DB_Password = CommonMethod.DecryptRijndael(FeedConfig_SavedDBData.Password); //Retrive decrypted password
            Assert.AreEqual(true, DB_Password == Entered_Password, "Successful entry created for Password");

                if (Entered_OPtionalName =="")
                Assert.AreEqual(true, FeedConfig_SavedDBData.OptionalName == "", "Successful entry created for optionalName");
                else
                Assert.AreEqual(true, FeedConfig_SavedDBData.OptionalName == Entered_OPtionalName, "Successful entry created for optionalName");

            if(provider == "others")
            {
                Assert.AreEqual(true, FeedConfig_SavedDBData.Url == Entered_Url, "Successful entry created for URL");
            }
        }

        public void Verify_Feed_Schema(string Provider)
        {
            DataFeed_URLSchema = DB_Feed_URLSchema(Provider);
            string App_Schema = Feed_ProviderSchema.GetText();

            if (DataFeed_URLSchema.FeedSchema_id == 1 && Provider != "others")
            {
                Assert.AreEqual(true, App_Schema.Equals("Aemp 1.2"), "feed_schema AEMP1.2 is displayed for selected provider" + Provider);
                Assert.AreEqual(true, Feed_ProviderSchema.Enabled, "feed_schema  AEMP1.2 is enabled for selected provider:" + Provider);
                
            }
            if (DataFeed_URLSchema.FeedSchema_id == 1 &&Provider == "others")
            {
                Assert.AreEqual(true, Feed_ProviderSchema_All[0].Text.Equals("Aemp 1.2"), "feed_schema AEMP1.2 is displayed for selected provider" + Provider);
                Assert.AreEqual(true, Feed_ProviderSchema_All[1].Text.Equals("Aemp 2.0"), "feed_schema AEMP2.0 is displayed for selected provider" + Provider);
                Assert.AreEqual(true, Feed_ProviderSchema_All[0].Selected, "feed_schema  AEMP1.2 is enabled for selected provider:" + Provider);
                Assert.AreEqual(false, Feed_ProviderSchema_All[1].Selected, "feed_schema  AEMP2.0 is disabled for selected provider:" + Provider);
               
                //foreach (IWebElement option in Feed_ProviderSchema_All)
                //{
                //    String text = option.Text.ToString();
                //}

            }
            if (DataFeed_URLSchema.FeedSchema_id != 1)
            {
                Assert.AreEqual(true, App_Schema.Equals("Others"), "feed_schema AEMP1.2 is displayed for selected provider" + Provider);
                Assert.AreEqual(true, Feed_ProviderSchema.Enabled, "feed_schema  AEMP1.2 is enabled for selected provider:" + Provider);
            }
        }
       
           public void Verify_Feed_URL(string Provider, string URL)
        {
           DataFeed_URLSchema = DB_Feed_URLSchema(Provider);
            App_URL = Feed_URL.GetText();
            Assert.AreEqual(true,App_URL ==URL,"feed_URL is displayed selected provider:" + Provider);
            Assert.AreEqual(true, App_URL == DataFeed_URLSchema.Url, "feed_URL is displayed selected provider:" + Provider);
          
        }
        public void VerifyBtnSave_Disabled()
        {
            bool Status = BtnSave.Enabled;
            Assert.AreEqual(false,Status, "Save button is disabled");
        }
        public void VerifyBtnSave_Enabled()
        {
            bool Status = BtnSave.Enabled;
            Assert.AreEqual(true, Status, "Save button is Enabled");
        }

        public void VerifyUnamePaswd_Truncated50Char()
        {
           // Entered_Username, Entered_Password
            int Uname_legth = Entered_Username.Length;
            int Paswd_legth = Entered_Password.Length;
            Assert.AreEqual(true, Uname_legth == 50, "UserName is trucated to 56 char");
            Assert.AreEqual(true, Paswd_legth == 50, "Password is trucated to 56 char");


        }
        public void select_configuredFeed()
        {
            BtnStatus_Update.WebClick();
            ServiceproviderName = Txt_ContainTitle.Text.Split('(')[0];
        }

        //public string select_configuredFeed_ReturnProvider()
        //{
        //    BtnStatus_Update.WebClick();
        //    string ServiceproviderName = Txt_ContainTitle.Text.Split('(')[0];
        //    return 
        //}

        public string Existingusername()
        {
           string Uname=  Get_DuplicateUser(ServiceproviderName);
           return Uname;
        }
        public void select_configuredFeed_SelectProvider(string provider)
        {
            foreach (IWebElement option in ALLAvailableProvider_Txt_ContainTitle)
            {
                count++;
                String text = option.Text.ToString();
                if (text.Split('(')[0].Equals(provider))
                {
                    string BtnStatusUpdatexpath = "(.//*[contains(@class,'btn hbtn-yellow')])" + "[" + count + "]";
                    BrowserFactory.Driver.FindElement(By.XPath(BtnStatusUpdatexpath)).WebClick();
                    TestConnection_Flag = true;
                    break;
                }      
              
            }
            if (TestConnection_Flag != true)
            {
                LOGGER.Info("Previousaly configured feed is not available for selected" + provider);
                Assert.Fail("Previousaly configured feed is not available for selected" + provider);
                System.Threading.Thread.CurrentThread.Abort();
                //System.Threading.Thread.EndThreadAffinity();
                //BrowserFactory.Driver.Quit();
            }
           
        }
       
        public void Select_ValidConfiguredFeed(string provider,string username, string password)
        {
           // int i = 0;
            foreach (IWebElement option in ALLAvailableProvider_Txt_ContainTitle)
            {
                count++;
                
                String text = option.Text.ToString();
                if (text.Split('(')[0].Equals(provider))
                {                    
                    string BtnStatusUpdatexpath = "(.//*[contains(@class,'btn hbtn-yellow')])" + "[" + count + "]";
                    BrowserFactory.Driver.FindElement(By.XPath(BtnStatusUpdatexpath)).WebClick();

                    System.Threading.Thread.Sleep(2000);
                  //  int countTestconnection = count - i;
                    string TestConnectionXpath = "(//button[text()=' Test Connection'])" + "[" + count + "]";
                    BrowserFactory.Driver.FindElement(By.XPath(TestConnectionXpath)).WebClick();

                    System.Threading.Thread.Sleep(3000);
                    TestConnection(username, password);
                  //  i++;
                    if (VerifyTestConnection_status ==true)               
                    break;
                    string BtnDiscardChangesxpath = "(//button[text()='Discard Changes'])" + "[" + count + "]";
                    BrowserFactory.Driver.FindElement(By.XPath(BtnDiscardChangesxpath)).WebClick();
                }

            }
            if (VerifyTestConnection_status == false)
            {
                Assert.Fail("no Valid configured feeds are available for selected" + provider);
            }
            else
            {
                string RegisterAssetXpath = "(//button[text()='Register Asset'])" + "[" + count + "]";
                BrowserFactory.Driver.FindElement(By.XPath(RegisterAssetXpath)).WebClick();
                System.Threading.Thread.Sleep(10000);
            }
        }

        public void Click_BtnRightPane(string button)
        {
           // bool Error_Flag = false;
            switch (button)
            {
                case "DiscardChanges":
                    BtnDiscard_Changes.WebClick();
                 break;

                case "Testconncetion":

                 if (count == 0)
                     BtnTest_Connection.WebClick();
                 string TestConnectionXpath = "(//button[text()=' Test Connection'])" + "[" + count + "]";
                 BrowserFactory.Driver.FindElement(By.XPath(TestConnectionXpath)).WebClick();
                 
                  System.Threading.Thread.Sleep(10000);
                 break;

                case "Updateconncetion":
                 BtnUpdate_Connection.WebClick();             
                 System.Threading.Thread.Sleep(10000);
                 break;

                case "RegisterAsset":
                 Retrived_RightPane_ConfiguredValue();
                 if (count == 0)
                  BtnRegister_Asset.WebClick(); 

                string RegisterAssetXpath = "(//button[text()='Register Asset'])" + "[" + count + "]";
                BrowserFactory.Driver.FindElement(By.XPath(RegisterAssetXpath)).WebClick();

                try
                {
                    MsgErrorDownloadingassets.Wait_ElmToBeDisplayed(20);
                    Error_RegisterAsset_Flag = MsgErrorDownloadingassets.Displayed;
                }
                catch (Exception e)
                {

                }            
                    break;

                default:
                    Console.WriteLine(button+"not available");
                    break;
            }
        }      
        public void Retrived_RightPane_ConfiguredValue()
        {
            if (count == 0)
            {
                //Configured_UserName.HighLightElement();
                TxtConfigured_Username = Configured_UserName.GetAttribute("value");
                TxtConfigured_Paswd = Configured_Password.GetAttribute("value");
                TxtConfigured_Url = Configured_URL.GetText();

                TxtConfigured_OptionalName = Txt_OptionalName.GetText();
                TxtConfigured_Provider_OptionalName = Txt_ContainTitle.GetText();
            }
            else
            {
                string Uname_Xpath = "(//label[text()='Username: ']//following::input[1])" + "[" + count + "]";
                string Paswd_Xpath = "(//label[text()='Password: ']//following::input[1])" + "[" + count + "]";
                string Url_Xpath = "(//label[text()='URL: ']//following::label[1])" + "[" + count + "]";

                 TxtConfigured_Username = BrowserFactory.Driver.FindElement(By.XPath(Uname_Xpath)).GetAttribute("value");
                 TxtConfigured_Paswd = BrowserFactory.Driver.FindElement(By.XPath(Paswd_Xpath)).GetAttribute("value");
                 TxtConfigured_Url = BrowserFactory.Driver.FindElement(By.XPath(Url_Xpath)).GetText();
            }
        }
       public void ModifyUserNamePassword_PreviouslyConfiguredFeed(string ModifyUsername,string ModifyPassword)
        {
            Retrived_RightPane_ConfiguredValue();
            Username_BeforeDiscard = TxtConfigured_Username;
            Password_BeforeDiscard = TxtConfigured_Paswd;
            URL_BeforeDiscard = TxtConfigured_Url;
           // Enter New modified username and  Password
            Configured_UserName.EnterText(ModifyUsername);
            Configured_Password.EnterText(ModifyPassword);

            Assert.AreEqual(true, ModifyUsername == Configured_UserName.GetAttribute("value"), "Username modified");
            Assert.AreEqual(true, ModifyPassword == Configured_Password.GetAttribute("value"), "Password modified");

        }
      
       public void UpdateUserNamePassword_PreviouslyConfiguredFeed(string UpdateUsername, string UpdatePassword, string WhatToupdate)
       {

           if (UpdateUsername == "" || UpdatePassword =="")
               UpdateConnection_Flag = false;

           Updated_Username = UpdateUsername;
           Updated_Password = UpdatePassword;
           TxtConfigured_Username = Configured_UserName.GetAttribute("value");
           TxtConfigured_Paswd = Configured_Password.GetAttribute("value");
           //Retrive the ID of UserName from DB
           ID_ConfiguredUsername= ServiceProviderDataId_UserName(TxtConfigured_Username);
           // Enter updated username and Password
           if (WhatToupdate == "username" || WhatToupdate == "any")
           {
               Configured_UserName.EnterText(UpdateUsername);
               Assert.AreEqual(true, UpdateUsername == Configured_UserName.GetAttribute("value"), "Username updated");
               if(WhatToupdate!="any")
               Updated_Password = TxtConfigured_Paswd;
             
           }
           if (WhatToupdate == "password" || WhatToupdate == "any")
           {
               Configured_Password.EnterText(UpdatePassword);
               Assert.AreEqual(true, UpdatePassword == Configured_Password.GetAttribute("value"), "Password updated");
               string a = Configured_Password.GetAttribute("value");
               if (WhatToupdate != "any")
              Updated_Username = TxtConfigured_Username;
            
           }
           

       }
       public void VerifyUpdatedUserName_Password()
       {
           if (UpdateConnection_Flag == false)
           {
               bool msg_Status = Msg_UNamePaswd_CannotbeNull.Displayed;
               Assert.AreEqual(true, msg_Status, "UserName/Password cannot be null/empty message is displayed");
           }
           else
           {
               FeedConfig_SavedDBData = Verify_UpadtedFeddConfig_DBData(ID_ConfiguredUsername);
               string DBUpdated_Username = FeedConfig_SavedDBData.UserName;
               string DBUpdated_Password = CommonMethod.DecryptRijndael(FeedConfig_SavedDBData.Password);
               string DBUpdated_Url = FeedConfig_SavedDBData.Url;

               Assert.AreEqual(true, Updated_Username == DBUpdated_Username, "UserName updated successfully in DB");
               Assert.AreEqual(true, Updated_Password == DBUpdated_Password, "UserName updated successfully in DB");
           }
       }
        public void VerifyPreviouslyConfiguredValueRetained()
       {
           Retrived_RightPane_ConfiguredValue();

           Assert.AreEqual(true, Username_BeforeDiscard == TxtConfigured_Username, "Discard button reatined previousaly configured username");
           Assert.AreEqual(true, Password_BeforeDiscard == TxtConfigured_Paswd, "Discard button reatined previousaly configured Password");
           Assert.AreEqual(true, URL_BeforeDiscard == TxtConfigured_Url, "Discard button reatined previousaly configured URL");

       }
        public void Verify_FeedConfig_RightPane(string provider)
        {
            select_configuredFeed();
            Retrived_RightPane_ConfiguredValue();
            string RightPane_FeedTitle = provider + "(" + Entered_OPtionalName + ")";
            Assert.AreEqual(true, TxtConfigured_Username == Entered_Username, "UserName configured correctly");
            Assert.AreEqual(true, TxtConfigured_Paswd == Entered_Password, "Password configured correctly");
            if(provider=="others")           
            Assert.AreEqual(true, TxtConfigured_Url == Entered_Url, "URL configured correctly for service provider other");
            else
            Assert.AreEqual(true, TxtConfigured_Url == App_URL, "URL configured correctly");
            
        }
       public void Verify_DuplicateMSg_FeedDetails()
        {
            bool status = MsgFeedDetails_AlreadyExists.Displayed;
            Assert.AreEqual(true, status, "DuplicateMSg: This feed details already exists displayed");
        }
       public void RegisterAssetStatus(string username, string password)
       {
          // Retrived_RightPane_ConfiguredValue();
           string Actual_Username = TxtConfigured_Username;
           string Actual_Password = TxtConfigured_Paswd;

           string Expected_Username = username;
           string Expected_Password = password;

           if (Actual_Username == Expected_Username && Actual_Password == Expected_Password)
           {
               Assert.AreEqual(false, Error_RegisterAsset_Flag, "Register Asset Successfull for Provider:" + provider);
               var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
               PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
               Apply_DcDcn.Verify_DC_DCN_PageDisplayed();
           }
           else
           {
               Assert.AreEqual(true, Error_RegisterAsset_Flag, "Register Asset Failed for Provider:" + provider);
           }
       }
        public void VerifyTestConnectionStatus(string username, string password)
        {          
            Retrived_RightPane_ConfiguredValue();
           // string provider = TxtConfigured_Provider_OptionalName.Split('(')[0];
            string Actual_Username = TxtConfigured_Username;
            string Actual_Password = TxtConfigured_Paswd;

            string Expected_Username = username;
            string Expected_Password = password;

            if (Actual_Username == Expected_Username && Actual_Password == Expected_Password)
            {
                bool Test_status = Icn_PassTestconnection.Displayed;
                Assert.AreEqual(true, Test_status, "Test connection is successful for valid credentials for" + provider);                
            }
            else
            {
                bool Test_status = Icn_FailTestconnection.Displayed;
                Assert.AreEqual(true, Test_status, "Test connection failed for Invalid credentials for" + provider);
                Assert.AreEqual(true,Msg_FailedTestconnection.Displayed, "Test failed msg is displayed");
            }
        }

        public void TestConnection(string username, string password)
        {
            Retrived_RightPane_ConfiguredValue();
            // string provider = TxtConfigured_Provider_OptionalName.Split('(')[0];
            string Actual_Username = TxtConfigured_Username;
            string Actual_Password = TxtConfigured_Paswd;

            string Expected_Username = username;
            string Expected_Password = password;

            if (Actual_Username == Expected_Username && Actual_Password == Expected_Password)
               VerifyTestConnection_status = Icn_PassTestconnection.Displayed;

          
        }



  }
}
