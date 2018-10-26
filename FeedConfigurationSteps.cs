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
    [Scope(Feature = "UI_FeedConfiguration")]
    [Scope(Feature = "UI_INT_FeedConfiguration")]
   
    public class FeedConfigurationSteps
    {
        public static string user_name = "";
        public static string Duplicate_User = "";


        [Given(@"User Clicked On The Select Provider Dropdown")]
        public void GivenUserClickedOnTheSelectProviderDropdown()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.click_Dropdown();
        }
        
        [Then(@"Validate the List of provider displayed")]
        public void ThenValidateTheListOfProviderDisplayed()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();            
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_ProviderList();
        }

        [Given(@"User selected (.*) from provider dropdown list")]
        public void GivenUserSelectedProvider_ProviderDropdownList(string provider)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);               
            FeedConfig.click_Dropdown();
            FeedConfig.Select_Dropdown(provider);
        }

        [Then(@"Verify the Feed_Schema is displayed for selected ServiceProvider (.*)")]
        public void ThenVerifyTheFeed_SchemaIsDisplayedForSelectedServiceProvider(string provider)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_Feed_Schema(provider);
        }
        [Then(@"Verify the Feed_Url is displayed for selected ServiceProvider (.*),(.*)")]
        public void ThenVeriftTheFeed_UrlIsDisplayedForSelectedServiceProvider(string provider,string url)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_Feed_URL(provider,url);
        }

        [When(@"User Enterd the mandatory field (.*), (.*), (.*) and (.*) and (.*)")]
        public void WhenUserEnterdTheMandatoryField(string Username, string Password, string OPtionalName, string Action, string url)
        {
            user_name = Username;
            if(user_name != "")
            user_name = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + Username;

            Duplicate_User = user_name;

            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Feed_Configuration(user_name, Password, OPtionalName, Action, url);
        }

        [Then(@"Verify that Db_Entry created for new feed_configuration")]
        public void ThenVerifyThatDb_EntryCreatedForNewFeed_Configuration()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_User_FeddConfig_DBData(user_name);
     
        }
        [Then(@"Verify that Save button is disabled")]
        public void ThenVerifyThatSaveButtonIsDisabled()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.VerifyBtnSave_Disabled();
        }

        [Then(@"Verify That Input is truncated to 50 char and Saved in DB")]
        public void ThenVerifyThatInputIsTruncatedTo50_CharandsavedinDB()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_User_FeddConfig_DBData(user_name);
        }

        [When(@"User Enterd the mandatoryfield (.*), (.*), (.*) and (.*) and (.*) and (.*)")]
        public void WhenUserEnterdTheMandatoryfield(string Username, string Password, string OPtionalName, string Action, string url, int url_length)
        {
            user_name = Username;           
            if (user_name != "")
                user_name = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + Username;

            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            string URL_255 = FeedConfig.RandomString(url_length);
            FeedConfig.Feed_Configuration(user_name, Password, OPtionalName, Action, URL_255);
        }
        [Then(@"Verify that error message is diplayed as URL char is more than 255 char")]
        public void ThenVerifyThatErrorMessageIsDiplayed()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.VerifyURLErrorMsgDisplayed();
        }

        [Then(@"User selected (.*) from provider dropdown list")]
        public void ThenUserSelected_ProviderDropdownList(string provider)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.click_Dropdown();
            FeedConfig.Select_Dropdown(provider);
        }


        [Then(@"User Entered the duplicate feed (.*), (.*), (.*) and (.*) and (.*)")]
        public void ThenUserEnteredTheDuplicate(string Username, string Password, string OPtionalName, string Action, string url)
        {
            user_name = Duplicate_User;
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Feed_Configuration_duplicate(user_name, Password, OPtionalName, Action, url);
        }

        [Then(@"Verify that duplicate entry was not created")]
        public void ThenVerifyThatDuplicateEntryWasNotCreated()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_DuplicateMSg_AndCountDBUsername();

        }
   
        [Then(@"Verify that Existing ServiceproviderData_id is returned")]
        public void Verify_ExistingServiceproviderData_IdIsReturnedForDuplicateEntry()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_ExistingServiceProviderDataId_Displayed();
        }



//-------------------------------Right Pane Feature File steps------------------------------------------

        [Then(@"Verify that new feed_configuration is displayed in right pane for (.*)")]
        public void ThenVerifyThatNewFeed_ConfigurationIsDisplayedInRightPane(string provider)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_FeedConfig_RightPane(provider);
        }

        [Given(@"Select the Previously Configured Feed")]
        public void GivenSelectThePreviouslyConfiguredFeed()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig); 
            FeedConfig.select_configuredFeed();
        }
        [Given(@"Select the Previously Configured Feed (.*)")]
        public void GivenSelectPreviouslyConfiguredFeed(string provider)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.select_configuredFeed_SelectProvider(provider);
        }

        [Then(@"Verify that test connection performed successfully (.*),(.*)")]
        public void ThenVerifyThatTestConnectionPerformedSuccessfully(string username, string password)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.VerifyTestConnectionStatus(username, password);
        }
        [Then(@"Verify that Register Asset performed successfully (.*),(.*)")]
        public void ThenVerifyThatRegisterAssetPerformedSuccessfully(string username,string password)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.RegisterAssetStatus(username, password);

        }


        [When(@"User modify the input value of (.*), (.*)")]
        public void WhenUserModifyTheInputValueOfModifyUsername(string Modify_Uname,string Modify_Paswd)
        {
            //Modify_Uname = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + Modify_Uname;
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.ModifyUserNamePassword_PreviouslyConfiguredFeed(Modify_Uname, Modify_Paswd);
        }

        [When(@"User click the button (.*)")]
        public void WhenUserClickTheButton(string button)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Click_BtnRightPane(button);
        }
        [Then(@"User click the button (.*)")]
        public void UserClickTheButton(string button)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Click_BtnRightPane(button);
        }

        [Then(@"Verify that previously configured orignal value are retained")]
        public void ThenVerifyThatPreviouslyConfiguredOrignalValueAreRetained()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.VerifyPreviouslyConfiguredValueRetained();
        }
        [When(@"User update the input value of (.*), (.*), (.*)")]
        public void WhenUserUpdateTheInputValue(string Update_Username, string Update_Paswd, string WhatToupdate)
        {
            string Update_Uname = "";
            if (Update_Username != "")
            Update_Uname = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + Update_Username;
            Duplicate_User = Update_Uname;
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            if (Update_Username == "DuplicateUserName")
                Update_Uname = FeedConfig.Existingusername();
            FeedConfig.UpdateUserNamePassword_PreviouslyConfiguredFeed(Update_Uname, Update_Paswd, WhatToupdate);
        }

        [Then(@"Verify that username and password successfully updated")]
        public void ThenVerifyThatUsernameAndPasswordSuccessfullyUpdated()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.VerifyUpdatedUserName_Password();
        }
        [Then(@"User Entered the duplicate feed for update connection (.*), (.*), (.*)")]
        public void ThenUserEnteredTheDuplicateFeed(string Uname, string Paswd,string WhatToupdate)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
           // Duplicate_User = FeedConfig.Get_DuplicateUser();
            Uname = Duplicate_User;
            FeedConfig.UpdateUserNamePassword_PreviouslyConfiguredFeed(Uname, Paswd, WhatToupdate);
        }

        [Then(@"Verify that duplicate entry was not created through Update connection")]
        public void ThenVerifyThatDuplicateEntryWasNotCreatedThroughUpdateConnection()
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Verify_DuplicateMSg_FeedDetails();
        }

    }
}
