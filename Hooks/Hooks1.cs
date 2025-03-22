using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using SwagLabTestBDDFramework.Framework.Drivers;
using Microsoft.Extensions.Configuration;
using SwagLabTestBDDFramework.Framework.Credentials;
using System.Reflection;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using PageLayers.Pages;
using SwagLabTestBDDFramework.Framework.Reports;
using SwagLabTestBDDFramework.Framework.Screenshots;
using SwagLabTestBDDFramework.Framework.Logs;
using PageLayers.API;
using RestSharp;


namespace TestLayers.Hooks
{
    [Binding]
    public sealed class Hooks1
    {

        private readonly IObjectContainer _objectContainer;
        private LogInPage _logInPage;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extentReports;
        private ExtentTest _extentTest;
        private static ExtentSparkReporter _sparkReporter;
        private ExtentTest _stepTest;
        private TokenGenerate _tokenGenerate;
        private string _token;
        private RestClient _restclient;
        private RestResponse _response;
        private RestRequest _request;
        private BookingModel _bookingModel;


        public Hooks1(IObjectContainer objectContainer, ScenarioContext scenarioContext) {


            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _extentReports = ReportManager.GetReports();
            LogManaging.InitializeLogging();

        }
      

        [BeforeScenario]
        public void FirstBeforeScenario()
        {
             WebFactory._driver.Value = WebFactory.CreateDriver("chrome");
            _objectContainer.RegisterInstanceAs(WebFactory._driver.Value);
            _extentTest = _extentReports.CreateTest(_scenarioContext.ScenarioInfo.Title);
            LogManaging.GetLogger().Info($"Starting scenario: {_scenarioContext.ScenarioInfo.Title}");


            _tokenGenerate = new TokenGenerate();
            _token = _tokenGenerate.GenerateToken();
            _restclient = new RestClient(CredentialManager.GetEndpoint());

            if (!_objectContainer.IsRegistered<RestClient>())
            {
                _objectContainer.RegisterInstanceAs(_restclient);
            }

            if (!_objectContainer.IsRegistered<Hooks1>())
            {
                _objectContainer.RegisterInstanceAs(this);
            }


        }


    public void LogIntoApplication()
    {
        _logInPage.NavigateTo(CredentialManager.GetBaseUrl());

        var (username, password) = CredentialManager.GetCredentials();
        _logInPage.EnterUsernameAndPass(username, password);
        _logInPage.ClickOnLogIn();

    }
     
        public string GetToken()
        {
            return _token;
        }
       
    
    public IWebDriver GetDriver()
    {
        return WebFactory._driver.Value;
    }

    [AfterScenario]
    public void AfterScenario()
    {


        if (_scenarioContext.TestError != null)
        {
            LogManaging.GetLogger().Error($"Test Failed: {_scenarioContext.ScenarioInfo.Title}");
            LogManaging.GetLogger().Error($"Error Message: {_scenarioContext.TestError.Message}");

            _extentTest.Fail($"Test Failed: {_scenarioContext.ScenarioInfo.Title}");
            _extentTest.Fail($"Error Message: {_scenarioContext.TestError.Message}");
        }
        else
        {
            _extentTest.Pass("Test Passed");
            LogManaging.GetLogger().Info($"Test Passed: {_scenarioContext.ScenarioInfo.Title}");
        }


        var driver = _objectContainer.Resolve<IWebDriver>();

        driver?.Quit();


    }

    [AfterStep]
    public void AfetrStep()
    {
        var stepInfo = _scenarioContext.StepContext.StepInfo.Text;
        var stepStatus = _scenarioContext.TestError == null ? Status.Pass : Status.Fail;
        _stepTest = _extentTest.CreateNode(stepInfo);
        _stepTest.Log(stepStatus, stepInfo);

        if (_scenarioContext.TestError != null)
        {
            _stepTest.Fail($"Error: {_scenarioContext.TestError.Message}");
            _stepTest.AddScreenCaptureFromBase64String(TakeScreenshot.CaptureScreenShot());
        }

    }


    [AfterTestRun]
    public static void AfterTestRun()
    {
        ReportManager.FlushReports();
        LogManaging.GetLogger().Info("Extent Report has been flushed.");
    }


    }
}