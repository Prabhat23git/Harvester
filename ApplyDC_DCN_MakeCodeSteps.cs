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
    [Scope(Feature = "UI_ApplyDC_DCN_MakeCode")]
    [Scope(Feature = "UI_INT_ApplyDC_DCN_MakeCode")]
    
    [Binding]
    public class ApplyDC_DCN_MakeCodeSteps
    {
       
        public void GivenCheckTable()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);

            //string CoulmnName = "Manufacture Desc";
            //string OrderBy = "Desc";
            //string OrderBy = "Asc";
            //Apply_DcDcn.Verify_Sort_Columns(CoulmnName, OrderBy);
            Apply_DcDcn.Export_to_Excel();

        }


        [Given(@"Get the Jsonfor Clamp where FeedId (.*)")]
        public void GivenGetTheJsonforClamp(string FeedId)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Get_Json(FeedId);
        }

        [Then(@"verifytable")]
        public void ThenVerifytable()
        {
            ScenarioContext.Current.Pending();
        }


        [Given(@"Navigate to Page_ApllyDCDCN for (.*) having (.*) (.*)")]
        public void GivenNavigateToPage_ApllyDCDCN(string provider, string username, string password)
        {
            var FeedConfig = new PageCSFile.Feedconfiguration();
            PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
            FeedConfig.Select_ValidConfiguredFeed(provider, username, password);

        }
        [Given(@"Navigate to Page_ApplyMakeCode")]
        public void GivenNavigateToPage_ApplyMakeCode()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Navigate_ApplyMakeCode();
        }



        //[Given(@"Navigate to Page_ApllyDCDCN for (*) having (.*) (.*)")]
        //public void GivenSelectThePreviouslyConfiguredValidFeed(string provider,string username, string password)
        //{
        //    var FeedConfig = new PageCSFile.Feedconfiguration();
        //    PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
        //    FeedConfig.Select_ValidConfiguredFeed(provider, username, password);

        //}

        //[When(@"User click the button (.*)")]
        //public void WhenUserClickTheButtonRegisterAsset(string button)
        //{
        //    var FeedConfig = new PageCSFile.Feedconfiguration();
        //    PageFactory.InitElements(BrowserFactory.Driver, FeedConfig);
        //    FeedConfig.Click_BtnRightPane(button);
        //}

        //[Then(@"Verify that page DC_DCN Loaded successfully")]
        //public void ThenVerifyThatPageDC_DCNLoadedSuccessfully()
        //{
        //    var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
        //    PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
        //    Apply_DcDcn.CheckForEditableRow("Dealer");
        //    Apply_DcDcn.Enter_Dealer("TD00");

        //    Apply_DcDcn.GetEditableSerialNoRowNo_MakeCode("Serial Number");
        //    Apply_DcDcn.Enter_BusinessUnit("BRIAN CARPENTER");
        //    Apply_DcDcn.Enter_MakeCode("PAL");
        //    Apply_DcDcn.Enter_ModelYear("2017");
        //    Apply_DcDcn.Enter_AssetVIN("VIN");
        //    Apply_DcDcn.Verify_DC_DCN_PageDisplayed();
        //}

        [Then(@"Verify that page DC_DCN Loaded successfully")]
        public void ThenVerifyThatPageDC_DCNLoadedSuccessfully()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Verify_DC_DCN_PageDisplayed();
        }

        [Then(@"Verify that page ApplyMakeCode Loaded successfully")]
        public void ThenVerifyThatPageApplyMakeCodeLoadedSuccessfully()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Verify_MakeCode_PageDisplayed();
        }


        [When(@"User search with (.*)")]
        public void WhenUserSearchWith(string serial_no)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.SearchWith_SerialNo(serial_no);
        }

        [Then(@"Verify that searched (.*) is displayed")]
        public void ThenVerifyThatSearchedAIsDisplayed(string serial_no)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.VerifySearchedresult_SerialNo(serial_no);
        }

        [Then(@"Verify that (.*) are sorted in (.*) order")]
        public void ThenVerifyThatColAreSortedInAscOrder(string ColumnName, string OrderBy)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Verify_Sort_Columns(ColumnName, OrderBy);

        }

        [When(@"User Filter (.*) with (.*)")]
        public void WhenUserFilterWith(string ColumnName, string FilterText)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.FilterColumn(ColumnName, FilterText);

        }

        [Then(@"Verify that (.*) filter results are displayed for (.*)")]
        public void ThenVerifyFilterResults(string ColumnName, string FilterText)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.VerifyFilterResult(ColumnName, FilterText);

        }

        [When(@"User Export the csv file")]
        public void WhenUserExportTheCsvFile()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Export_to_Excel();
        }

        [Then(@"Verify the Exported csv file")]
        public void ThenVerifyTheExportedCsvFile()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
           // Apply_DcDcn.VerifyExcel()
        }

        [When(@"Mouse Hover to ToolTip error")]
        public void WhenMouseHoverToToolTipError()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.MouseHover_ToolTipError();
        }

        [Then(@"Verify ToolTip Message")]
        public void ThenVerifyToolTip()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.verify_ErrorMessage_PageDC_DCN();
        }

        [Then(@"Verify ToolTip Message_ApplyMakeCodePage")]
        public void ThenVerifyToolTipMessage_ApplyMakeCodePage()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.verify_Error_message_Page_ApplyMakeCode();
        }

        [Then(@"Verify Error Count for (.*) (.*)")]
        public void ThenVerifyErrorCount(string username, string OrganizationId)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.verifyError_count(username, OrganizationId);
        }

        [When(@"User Apply (.*) and (.*)")]
        public void ApplyDealer_BusinessUnit(string dealer, string BusinessUnit)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.CheckForEditableRow("Dealer"); //check which row is editable in  column Dealer
            Apply_DcDcn.Enter_Dealer(dealer);
            Apply_DcDcn.GetEditableSerialNoRowNo_MakeCode("Serial Number");
            Apply_DcDcn.Enter_BusinessUnit(BusinessUnit);
        }

        [Then(@"Click on Save_next button on Page Apply Dc_Dcn")]
        public void ClickOnSave_NextButton_PageApplyDc_Dcn()
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.Click_SaveNext();
            Apply_DcDcn.Verify_MakeCode_PageDisplayed();
        }

        [Then(@"Apply (.*) (.*) (.*) to same Asset")]
        public void ApplyMakeCode(string Make,string Year, string Vin)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.GetEditableSerialNoRowNo_MakeCode("Serial Number"); //get the row no of editable make code
            Apply_DcDcn.Enter_MakeCode(Make);
            Apply_DcDcn.Enter_ModelYear(Year);
            Apply_DcDcn.Enter_AssetVIN(Vin);
            Apply_DcDcn.Click_finsh_ApplyMake();

        }

        [Then(@"Verify Asset is registered Successfully for (.*) (.*)")]
        public void VerifyAssetIsRegisteredSuccessfully(string username, string OrgId)
        {
            var Apply_DcDcn = new PageCSFile.ApplyDC_DCN();
            PageFactory.InitElements(BrowserFactory.Driver, Apply_DcDcn);
            Apply_DcDcn.VerifyDBdata_Ed_CCds(username, OrgId);
        }


    }
}
