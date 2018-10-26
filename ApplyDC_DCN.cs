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
using Microsoft.Office.Interop.Excel;
//using AutoItX3Lib;
//using OpenQA.Selenium.Interactions;

namespace Cat.Automation.UI.PageCSFile
{
    public class ApplyDC_DCN :DB_Data
    {
        public static IWebDriver Driver;
        //AutoItX3 AutoIt = new AutoItX3();
        DBData_ED_CCDSID DBData_EdCCDsId = new DBData_ED_CCDSID();
        bool Disabled_Cell = false;
        bool FlagDealer = false;
        bool FlagBusinessUnit = false;
        public static string CellXpath = "";
        public static string Serial_Number ="";
        public static string Actual_Dealer = "";
        public static string Actual_BusinessUnit = "";
        public static string Actual_MakeCode = "";
        public static string Actual_Year = "";
        public static string Actual_VIN = "";
        public static int EditableRowNo_ApplyMakeCode = 1;

        public static Microsoft.Office.Interop.Excel.Application _xlApp;
        public static Microsoft.Office.Interop.Excel.Workbook _xlWorkBook;
        public static Microsoft.Office.Interop.Excel.Worksheet _xlWorkSheet;
        public static ILog LOGGER = LogManager.GetLogger(typeof(ApplyDC_DCN));

       

        [FindsBy(How = How.XPath, Using = "//button[text()='CANCEL']")]
        public IWebElement BtnCancel { get; set; }
        [FindsBy(How = How.XPath, Using = "//button[text()='SAVE & NEXT']")]
        public IWebElement BtnSave_Next { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[text()= 'Yes']")]
        public IWebElement BtnYes_Confitmation_DCDCNPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = ' Finish']")]
        public IWebElement BtnFinish { get; set; } 
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Error')]")]
        public IWebElement Msg_ErrorCount { get; set; }
        //[FindsBy(How = How.XPath, Using = "//span[contains(text(),'Error')]/following-sibling::span")]
        //public IWebElement Btn_Download { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Error')]//ancestor::span/span[3]")]
        public IWebElement Btn_Download { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Serial Number')]")]
        public IWebElement Cln_SerialNumber { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Manufacture Desc')]")]
        public IWebElement Cln_ManufactureDesc { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Dealer')]")]
        public IWebElement Cln_Dealer { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Business Unit')]")]
        public IWebElement Cln_BusinessUnit { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'Business Unit')]")]
        public IWebElement BtnFilter_BusinessUnit { get; set; }
        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'Filter by Dealer')]")]
        public IWebElement BtnFIlter_ByDealer { get; set; }
        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'Filter by Descriptions')]")]
        public IWebElement BtnFIlter_ByDescription { get; set; }


        //[FindsBy(How = How.XPath, Using = "//div[@class='ui-multiselect-items-wrapper']/ul/li")]
        //public IList<IWebElement> List_Description { get; set; }

        ////div[@class='ui-multiselect-items-wrapper']/ul/li

        [FindsBy(How = How.XPath, Using = "(//input[@role='textbox'])[1]")]
        public IWebElement SearcTxt_Filter_BusinessUnit { get; set; }
        [FindsBy(How = How.XPath, Using = "(//input[@role='textbox'])[2]")]
        public IWebElement SearcTxt_BtnFIlter_ByDealer { get; set; }
        [FindsBy(How = How.XPath, Using = "(//input[@role='textbox'])[3]")]
        public IWebElement SearcTxt_FIlter_ByDescription { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'myTable ui-datatable')]")]
        public IWebElement HtmlTable_DCN{ get; set;}
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'ui-datatable-scrollable-header')]")]
        public IWebElement HtmlTable_Header_DCN { get; set; }


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

        [FindsBy(How = How.XPath, Using = "//span[@class= 'steps-marker'][text()= '3']")]
        public IWebElement Icn3_ApplyMakeCodePage { get; set; }

        

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'Serial Number']")]
        public IWebElement Txt_SerialNumber { get; set; }

        public void Verify_DC_DCN_PageDisplayed()
        {
           // HtmlTable_DCN.Displayed
            Assert.AreEqual(true, HtmlTable_DCN.Displayed, "DC_DCN page is displayed successfully");

            string pageno = PageNumber.GetText();
            try
            {
                if (pageno == "2")
                {
                    pagedisplayed = true;
                }
                else Console.WriteLine("Apply DC/DCN Page is not displayed");
            }
            catch { Exception e; }
            Assert.AreEqual(true, pagedisplayed, "Apply DC/DCN Page is displayed");
        }

        public void Verify_MakeCode_PageDisplayed()
        {
            // HtmlTable_DCN.Displayed
            Assert.AreEqual(true, HtmlTable_DCN.Displayed, "DC_DCN page is displayed successfully");

            string pageno = PageNumber.GetText();
            try
            {
                if (pageno == "3")
                {
                    pagedisplayed = true;
                }
                else Console.WriteLine("Apply Make Code Page is not displayed");
            }
            catch { Exception e; }
            Assert.AreEqual(true, pagedisplayed, "Apply Make code Page is displayed");
        }
        public void MouseHover_ToolTipError()
        {
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(BrowserFactory.Driver);
            actions.MoveToElement(EleErrorCount).Build().Perform();
        }
        public void Navigate_ApplyMakeCode()
        {
            Icn3_ApplyMakeCodePage.WebClick();
            System.Threading.Thread.Sleep(3000);
        }
        public void verifyError_count(string username, string OrganizationId)
        {
           int  registeredAsset_Count  = 0;
            int registeredAssetCount = RegisteredAssetcount(username, OrganizationId);
            string countmsg = EleErrorCount.GetText();
            IList<IWebElement> rows = HtmlTable_DCN.FindElements(By.TagName("tr"));
            int RowCnt = rows.Count;
            registeredAsset_Count = RowCnt - registeredAssetCount -1;  //Subtract by 1 as header is also counted as 1 row
            Assert.AreEqual(true, countmsg.Contains(registeredAsset_Count.ToString()), "Count Matches for unregistered asset");
        }
        public void VerifyDBdata_Ed_CCds(string username, string orgid)
        {
          DBData_EdCCDsId=  DBdata_Ed_CCDSId(username, Serial_Number, orgid);
            string DBEdid = DBData_EdCCDsId.EdrefId;
            string DBCCdsrefId = DBData_EdCCDsId.CCDSRefid;
            string DbStatus = DBData_EdCCDsId.status;
            string Dbyear = DBData_EdCCDsId.year;
            string DbVin = DBData_EdCCDsId.VIN;
            string DbCatMakeCode = DBData_EdCCDsId.CATMakeCode;

            if (String.IsNullOrEmpty(DBData_EdCCDsId.DealerDescription) || String.IsNullOrEmpty(DBData_EdCCDsId.BusinessUnitDescription))
            {
                Assert.Fail("Dealer/Business Description are null value");
               
            }
            else
            {
                Assert.AreEqual(true, DBData_EdCCDsId.DealerDescription.Contains(Actual_Dealer), "dealer Description stored successfuly in DB");
                Assert.AreEqual(true, DBData_EdCCDsId.BusinessUnitDescription.Contains(Actual_BusinessUnit), "Business Unit stored successfuly in DB");
            }

            if (String.IsNullOrEmpty(DBEdid) || String.IsNullOrEmpty(DBCCdsrefId))
                Assert.Fail("EdiD/CCDSRefID is not associated successfuly");
            Assert.AreEqual(true,DbStatus, "3", "Status updated as 3  for registered Asset" );
            Assert.AreEqual(true,Dbyear, Actual_Year, "Year is updated successfuly in DB");
            Assert.AreEqual(true,DbVin, Actual_VIN, "Vin is updated successfuly in DB");
            Assert.AreEqual(true, DbCatMakeCode, Actual_MakeCode, "Vin is updated successfuly in DB");
        }
        public void verify_ErrorMessage_PageDC_DCN()
        {
            int i = 0;
            string[] ArrErrorsmg = new string[2];

            foreach (var msg in TxtErrorMsg)
            {
                ArrErrorsmg[i] = msg.GetText();
                i++;
            }
            Assert.AreEqual(true, ArrErrorsmg[0] == "Red = Mandatory Field : Data Missing", "Error Message verified successfully");
            Assert.AreEqual(true, ArrErrorsmg[1] == "Green = Mandatory Field : Data Associated", "Error Message verified successfully");
        }

        public void verify_Error_message_Page_ApplyMakeCode()
        {
            int i = 0;        
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(BrowserFactory.Driver);
            actions.MoveToElement(EleErrorCount).Build().Perform();
            string[] ArrErrorsmg = new string[4];

            foreach (var msg in TxtErrorMsg)
            {
                ArrErrorsmg[i] = msg.GetText();
                i++;
            }
            Assert.AreEqual(true, ArrErrorsmg[0] == "Red = Mandatory Field : Data Missing", "Error Message verified successfully");
            Assert.AreEqual(true, ArrErrorsmg[1] == "Green = Mandatory Field : Data Associated", "Error Message verified successfully");
            Assert.AreEqual(true, ArrErrorsmg[2] == "Blue = Optional Field: Data Associated", "Error Message verified successfully");
            Assert.AreEqual(true, ArrErrorsmg[3] == "Yellow = Optional Field: Data Missing", "Error Message verified successfully");
        }
        public void VerifyFilterResult(string ColumnName, string FilterText)
        {
            //GetTable_ColumnData(ColumnName);

            List<string> List_Coldata = GetTable_ColumnData(ColumnName);
            foreach (var data in List_Coldata)
            {
                Assert.AreEqual(true, data.ToLower().Contains(FilterText.ToLower()), "searched serial number is displayed");
            }
        }
        public void FilterColumn(string ColumnName, string FilterText)
        {
            bool FlagFilter = false;
            //get column no from column name -2
            int ColNo = GetTable_ColNo(ColumnName);
            ColumnNo = ColNo - 2; //Subtracting by 2 as first filter column no is 3
            IWebElement Txtbox_Srchfilter = BrowserFactory.Driver.FindElement(By.XPath("(//label[text()= 'Filter']//following::input[@type = 'text'][1])" + "[" + ColumnNo + "]"));
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



        public void SearchWith_SerialNo(string serialno)
        {
            Txt_SerialNumber.EnterText(serialno);

        }
        public void VerifySearchedresult_SerialNo(string serialno)
        {
            GetTable_ColumnData("Serial Number");

            List<string> List_SerialNO = GetTable_ColumnData("Serial Number");
            foreach(var sno in List_SerialNO )
            {
                Assert.AreEqual(true, sno.ToLower().Contains(serialno.ToLower()), "searched serial number is displayed");
            }

        }
        public string[] Flow_GetTableData()
        {
            //bool a = BtnSave_Next.Displayed;
            //bool c = HtmlTable_DCN.Displayed;

            IList<IWebElement> rows = HtmlTable_DCN.FindElements(By.TagName("tr"));
            int RowCnt = rows.Count;
            //IList<IWebElement> Columns = HtmlTable_DCN.FindElements(By.TagName("th"));
           // IList<IWebElement> rows_header = HtmlTable_Header_DCN.FindElements(By.TagName("tr"));
            int i = 0;
            int Counter = 0;
            
            String[] DataArray = new String[RowCnt];
            //int rowindex = 0;
            foreach(var row in rows)
            {
                Counter++;             
                if (Counter == 1)
                {
                    var ColData = row.FindElements(By.TagName("th"));
                    foreach (var colvalue in ColData)
                    {
                        DataArray[i] = DataArray[i] + "^#$@~" + colvalue.GetText().Trim().ToString(); 
                    }
                    i++;
                }
                else
                {
                    var ColData = row.FindElements(By.TagName("td"));
                    foreach (var colvalue in ColData)
                    {
                        DataArray[i] = DataArray[i] + "^#$@~" + colvalue.GetText().Trim().ToString();
                    }
                    i++;
                }
               
            }
            return DataArray;
        }
        public void Export_to_Excel()
        {
            

            string StrFilepath, StrFileName, StrFileFolderpath;
            StrFileFolderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StrFileName = "Report" + DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            StrFilepath = StrFileFolderpath + @"\" + StrFileName + ".csv";
            Btn_Download.WebClick();

            AutoIt.AutoItX.WinActivate("Save As");
            System.Threading.Thread.Sleep(2000);
            AutoIt.AutoItX.Send(StrFilepath);
            System.Threading.Thread.Sleep(2000);
            AutoIt.AutoItX.Send("{ENTER}");

            string[] DataArray = Flow_GetTableData();
            VerifyExcel(StrFilepath, DataArray);
        }

        public void VerifyExcel(string FileName, string[] ArrayData)
        {
            _xlApp = new Microsoft.Office.Interop.Excel.Application();

            _xlWorkBook = _xlApp.Workbooks.Open(FileName, 0, true);//5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            _xlWorkBook = _xlApp.Workbooks.Open(FileName, 0, true);

            _xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(1);

            String strCellValue, strheaderval;
            int i, j, k;
            bool flagheader, flagvalidcol;
           // int intFailCnt = 0;

            int ArraySize = ArrayData.Length; 
            string[] stringSeparators = new string[] { "^#$@~" };

            String[] header = ArrayData[0].Split(stringSeparators, StringSplitOptions.None);
            int intheadercnt = header.Length;


            for (i = 0; i < intheadercnt; i++)
            {
                flagheader = false;
                flagvalidcol = true;
                for (j = 1; j < intheadercnt; j++)
                {
                    strCellValue = _xlWorkSheet.Cells[1, j].Value2.ToString();
                    if(strCellValue!=null)
                    {
                        strCellValue = strCellValue.ToLower();
                        strheaderval = header[i].ToLower();
                        if (strheaderval == "manufacture desc\r\nfilter")
                        {
                            strheaderval = "manufacture desc";
                        }
                        else if (strheaderval == "dealer\r\nfilter")
                        {
                            strheaderval = "dealer";
                        }
                        else if (strheaderval == "business unit\r\nfilter")
                        {
                            strheaderval = "business unit";
                        }

                        if (strheaderval == "make code\r\nfilter")
                        {
                            strheaderval = "make code";
                        }
                        else if (strheaderval == "asset id\r\nfilter")
                        {
                            strheaderval = "asset id";
                        }
                        else if (strheaderval == "asset model\r\nfilter")
                        {
                            strheaderval = "asset model";
                        }
                        if (strheaderval == "model year\r\nfilter")
                        {
                            strheaderval = "model year";
                        }
                        else if (strheaderval == "asset vin\r\nfilter")
                        {
                            strheaderval = "asset vin";
                        }

                        if ((strheaderval == ""))
                        {
                            flagheader = true;
                            flagvalidcol = false;
                            break;
                        }

                        else if (strheaderval.Trim() == strCellValue.Trim())
                        {
                            Console.WriteLine("Column " + header[i] + " found", 0);
                            flagvalidcol = true;
                            flagheader = true;
                            for (k = 1; k < ArrayData.Length; k++)
                            {
                                String[] arrresult = ArrayData[k].Split(stringSeparators, StringSplitOptions.None);
                                var strCellValue2 = _xlWorkSheet.Cells[k + 1, j].Value2;
                                string strArrresult = arrresult[i].ToLower();
                                string Actual_ArrResult = strArrresult;
                                if (strCellValue2 != null)
                                {
                                    strCellValue2 = strCellValue2.ToString().ToLower();
                                    //string strArrresult = arrresult[i].ToLower();


                                    strCellValue2 = string.Join("", strCellValue2.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                                    strArrresult = string.Join("", strArrresult.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                                    Assert.AreEqual(strCellValue2, strArrresult);
                                    Console.WriteLine("Cell value \"" + strArrresult.Trim() + "\" and Excel Cell value \"" + strCellValue2.Trim() + "\" are compared");
                                }
                                else
                                {
                                    if (strArrresult == "select dealer" || strArrresult == "select dealer first" || strArrresult == "-")
                                        strArrresult = null;

                                     Assert.AreEqual(strCellValue2, strArrresult);
                                     Console.WriteLine("Cell value \"" + Actual_ArrResult + "\" and Excel Cell value \"" + strCellValue2 + "\" are compared");
                                }
                            }
                            Console.WriteLine("Values in Column " + header[i] + " verified", 0);
                            break;
                        }
                    }
                }
                    if (flagvalidcol == true)
                    {
                        if (flagheader == true)
                        {
                            Console.WriteLine("Column " + header[i] + " found and verified");
                        }
                        else
                        {
                            Console.WriteLine("Column " + header[i] + "not found");
                            Assert.Fail();
                        }
                    }
                
            }
        }

        public void CheckForEditableRow(string ColumnName)
        {   
            int i;
            IList<IWebElement> rows = HtmlTable_DCN.FindElements(By.TagName("tr"));
            int RowCnt = rows.Count;
            int ColNo = GetTable_ColNo(ColumnName);
            string ColDataxpath = "(//table)[2]/tbody/tr/td" + "[" + ColNo + "]";
            for( i =1;i<=RowCnt;i++)
            {
                CellXpath = "(" +ColDataxpath +")" + "["+i+"]" ;
                IWebElement ColumnInput = BrowserFactory.Driver.FindElement(By.XPath(CellXpath));
                ColumnInput.WebClick();
                
                try
                {
                    IWebElement Row_Disabled = BrowserFactory.Driver.FindElement(By.XPath(CellXpath + "//input[@disabled]"));
                    Disabled_Cell = Row_Disabled.Displayed;    
                }
                catch {
                      Exception e;
                      Disabled_Cell = false;
                      break;                         
                }
            }        

            //Get The SerialNo of the row which is editable

            int SNO_Colno = GetTable_ColNo("Serial Number");
            string Sno_ColDataxpath = "((//table)[2]/tbody/tr/td" + "[" + SNO_Colno + "])" + "["+i+"]" ;
            Serial_Number = BrowserFactory.Driver.FindElement(By.XPath(Sno_ColDataxpath)).GetText();

        }

        public void Enter_Dealer(string Dealer)
        {
            Actual_Dealer = Dealer;
            if (Disabled_Cell == false)
            {              
                IWebElement ColumnInput = BrowserFactory.Driver.FindElement(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]"));
                ColumnInput.EnterText(Dealer);

                IList<IWebElement> ListDealer = BrowserFactory.Driver.FindElements(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]//following::ul/li"));

                foreach (var Ele in ListDealer)
                {
                    string text = Ele.GetText();
                    if (text.Contains(Dealer))
                    {
                        Ele.WebClick();
                        System.Threading.Thread.Sleep(2000);
                        FlagDealer = true;
                        break;
                    }
                }
                if (FlagDealer == false)
                    Assert.Fail("Dealer is not available");
            }
            else
            {
                Assert.Fail("All the asset are registered");
            }
        }
        public void Click_SaveNext()
        {

            BtnSave_Next.WebClick();
            System.Threading.Thread.Sleep(2000);
            BtnYes_Confitmation_DCDCNPage.WebClick();
            System.Threading.Thread.Sleep(3000);

        }
        public void Click_finsh_ApplyMake()
        {
            BtnFinish.WebClick();
            System.Threading.Thread.Sleep(2000);
            BtnYes_Confitmation_DCDCNPage.WebClick();
            System.Threading.Thread.Sleep(3000);

        }
        public void Enter_BusinessUnit(string BusinessUnit)
        {
            Actual_BusinessUnit = BusinessUnit;
              int ColNo = GetTable_ColNo("Business Unit");
              CellXpath = "((//table)[2]/tbody/tr/td[" + ColNo + "])[" + EditableRowNo_ApplyMakeCode + "]";


              BrowserFactory.Driver.FindElement(By.XPath(CellXpath)).WebClick(); //Clicking the Next Column data of same disabled row
                IWebElement ColumnInput = BrowserFactory.Driver.FindElement(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]"));
                ColumnInput.WebClick();
                ColumnInput.EnterText(BusinessUnit);

                IList<IWebElement> ListBusiness = BrowserFactory.Driver.FindElements(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]//following::ul/li"));

                foreach (var Ele in ListBusiness)
                {
                    string text = Ele.GetText();
                    if (text.Contains(BusinessUnit))
                    {
                        Ele.WebClick();
                        FlagDealer = true;
                        break;
                    }
                }
                if (FlagDealer == false)
                    Assert.Fail("Dealer is not available");
            }         

        public int  GetEditableSerialNoRowNo_MakeCode(string ColumnName)
        {
            EditableRowNo_ApplyMakeCode = 1;
            int ColNo = GetTable_ColNo(ColumnName);
            string ColDataxpath = "(//table)[2]/tbody/tr/td" + "[" + ColNo + "]";
            IList<IWebElement> Table_ColData = BrowserFactory.Driver.FindElements(By.XPath(ColDataxpath));
            foreach (var col in Table_ColData)
            {
                string Text = col.GetText().Trim().ToString().ToLower();
                if (Text == Serial_Number.Trim().ToString().ToLower())
                    break;
                else
                    EditableRowNo_ApplyMakeCode++;
            }
            return EditableRowNo_ApplyMakeCode;
        }

        public void Enter_MakeCode(string  MakeCode)
        {
            Actual_MakeCode = MakeCode;
            int ColNo = GetTable_ColNo("Make Code");    
            CellXpath = "((//table)[2]/tbody/tr/td[" + ColNo + "])["+ EditableRowNo_ApplyMakeCode+ "]";
          
                BrowserFactory.Driver.FindElement(By.XPath(CellXpath)).WebClick(); //Clicking the Next Column data of same disabled row
                IWebElement ColumnInput = BrowserFactory.Driver.FindElement(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]"));
                ColumnInput.WebClick();
                ColumnInput.EnterText(MakeCode);

                IList<IWebElement> ListMakeCode = BrowserFactory.Driver.FindElements(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]//following::ul/li"));

                foreach (var Ele in ListMakeCode)
                {
                    string text = Ele.GetText();
                    if (text.Contains(MakeCode))
                    {
                        Ele.WebClick();
                        FlagDealer = true;
                        break;
                    }
                }
                if (FlagDealer == false)
                    Assert.Fail("Make  is not available");           
        }

        public void Enter_ModelYear(string Year)
        {
            Actual_Year = Year;
            int ColNo = GetTable_ColNo("Model Year");
            CellXpath = "((//table)[2]/tbody/tr/td[" + ColNo + "])[" + EditableRowNo_ApplyMakeCode + "]";

            BrowserFactory.Driver.FindElement(By.XPath(CellXpath)).WebClick(); //Clicking the Next Column data of same disabled row
            IWebElement ColumnInput = BrowserFactory.Driver.FindElement(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]"));
            ColumnInput.WebClick();
            ColumnInput.EnterText(Year);
        }

        public void Enter_AssetVIN(string VIN)
        {
            Actual_VIN = VIN;
            int ColNo = GetTable_ColNo("Asset VIN");
            CellXpath = "((//table)[2]/tbody/tr/td[" + ColNo + "])[" + EditableRowNo_ApplyMakeCode + "]";

            BrowserFactory.Driver.FindElement(By.XPath(CellXpath)).WebClick(); //Clicking the Next Column data of same disabled row
            IWebElement ColumnInput = BrowserFactory.Driver.FindElement(By.XPath(CellXpath + "//*[contains(@class, 'ui-inputtext ui-widget')]"));
            ColumnInput.WebClick();
            ColumnInput.EnterText(VIN);
        }

        public int GetTable_ColNo(string  ColumnName)
        {
            bool ColDisplayed = false;
            //if (ColumnName == "Manufacture Desc")
            //    ColumnName = "Manufacture Desc\r\nFilter";

            //if (ColumnName == "Dealer")
            //    ColumnName = "Dealer\r\nFilter";

            //if (ColumnName == "Business Unit")
            //    ColumnName = "Business Unit\r\nFilter";

            //if (ColumnName == "Make Code")
            //    ColumnName = "Make Code\r\nFilter";

            //if (ColumnName == "Asset ID")
            //    ColumnName = "Asset ID\r\nFilter";

            //if (ColumnName == "Asset Model")
            //    ColumnName = "Asset Model\r\nFilter";

            //if (ColumnName == "Model Year")
            //    ColumnName = "Model Year\r\nFilter";

            //if (ColumnName == "Asset VIN")
            //    ColumnName = "Asset VIN\r\nFilter";

            IList<IWebElement> rows_header = HtmlTable_Header_DCN.FindElements(By.TagName("th"));
            int ColIndex = 1;
            foreach (var colvalue in rows_header)
            {
                //if (ColumnName.ToLower() == colvalue.GetText().Trim().ToString().ToLower())
                    if ((colvalue.GetText().Trim().ToString().ToLower()).Contains(ColumnName.ToLower()))
                {
                    ColDisplayed = true;
                    break;
                }
                ColIndex++; 
            }
            if (ColDisplayed == false)
            {
                Assert.Fail("Selected Column is not displayed");
            }
            return ColIndex;
        }

        public List<string> GetTable_ColumnData(string ColumnName)
        {
           int  ColNo = GetTable_ColNo(ColumnName);
           List<string> ColData = new List<string>();
           string ColDataxpath = "(//table)[2]/tbody/tr/td" + "[" + ColNo + "]";
           IList<IWebElement> Table_ColData = BrowserFactory.Driver.FindElements(By.XPath(ColDataxpath));
           foreach (var col in Table_ColData)
            {
                string Text = col.GetText().Trim().ToString().ToLower();
               ColData.Add(Text);
            }
           return ColData;
        }
        public void Verify_Sort_Columns(string ColName, string OrderBy)
        {
            List<string> BeforeSorting = GetTable_ColumnData(ColName);

            if (OrderBy == "Asc")
            {
                //Sort the value in ascending order
                List<string> List_Sort = BeforeSorting.OrderBy(x => x, StringComparer.Ordinal).ToList();


                //Sort the value in ascending order in webtable
                       //string Col_Header_Xpath = "//span[contains(text(),'Serial Number')]";
                       string Col_Header_Xpath = "//span[contains(text(),'" +ColName+ "')]";

                BrowserFactory.Driver.FindElement(By.XPath(Col_Header_Xpath)).WebClick();
                System.Threading.Thread.Sleep(2000);
                //Retrive Sorted Value from Webtable
                List<string> AfterSorting = GetTable_ColumnData(ColName);

                System.Threading.Thread.Sleep(2000);
                //compare if WEbSorted value is same as List Sorted
                Assert.AreEqual(true, List_Sort.SequenceEqual(AfterSorting), "Ascending order Sorting Verified Successfully for Column:" + ColName);
            }
            if (OrderBy == "Desc")
            {
                //Sort the value in descending order
                List<string> List_Sort = BeforeSorting.OrderByDescending(x => x, StringComparer.Ordinal).ToList();

                //Sort the value in descending order in webtable by double click
                string Col_Header_Xpath = "//span[contains(text(),'" + ColName + "')]";

                BrowserFactory.Driver.FindElement(By.XPath(Col_Header_Xpath)).WebClick();
                System.Threading.Thread.Sleep(5000);
                BrowserFactory.Driver.FindElement(By.XPath(Col_Header_Xpath)).WebClick();
                System.Threading.Thread.Sleep(3000);
                //Retrive Sorted Value from Webtable
                List<string> AfterSorting = GetTable_ColumnData(ColName);


                //compare if WEbSorted value is same as List Sorted
                Assert.AreEqual(true, List_Sort.SequenceEqual(AfterSorting), "Descending order Sorting Verified Successfully for Column:" + ColName);
            }

        }

    }
}
