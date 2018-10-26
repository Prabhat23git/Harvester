//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;
//using System.IO;
//using AventStack.ExtentReports.Reporter;
//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Gherkin.Model;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Cat.Automation.UI.PageCSFile;

//namespace Cat.Automation.UI.Utilities
//{
//    [Binding]
//  public static class Hooks
//    {
//      public static ExtentReports extent;
//       public static ExtentTest featureName;
//       public static ExtentTest scenario;
//       public static ExtentHtmlReporter htmlReporter;
//       public static string UserDir;
 


//[BeforeTestRun]
//      public static void Generate_Report()
//{
//        UserDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

//            htmlReporter = new ExtentHtmlReporter(UserDir + "\\Reports\\Automation_Report_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".html");
 
//            htmlReporter.LoadConfig(UserDir+"\\extent-config.xml");
 
//            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
//            extent = new ExtentReports();
//            extent.AttachReporter(htmlReporter); 
 
//}

//[AfterTestRun]
//    public static void Configure_Report()
//{
//    var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

//    if (ScenarioContext.Current.TestError == null)
//    {
//        if (stepType == "Given")
//            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
//        else if (stepType == "When")
//            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
//        else if (stepType == "Then")
//            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
//        else if (stepType == "And")
//            scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
//    }

//    else if (ScenarioContext.Current.TestError != null)
//    {
//        if (stepType == "Given")
//            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
//        else if (stepType == "When")
//            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
//        else if (stepType == "Then")
//            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

//    }
//    extent.Flush();
//}
    
//    }
//}
