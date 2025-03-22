using NUnit.Framework;
using PageLayers.Pages;
using SwagLabTestBDDFramework.Framework.Drivers;
using System;
using TechTalk.SpecFlow;
using TestLayers.Hooks;

namespace TestLayers.StepDefinitions
{
   
    [Binding]
    public class LogInPageFunctionalityStepDefinitions
    {

        private LogInPage _logInPage;

        public LogInPageFunctionalityStepDefinitions(Hooks1 hook)
        {
            WebFactory._driver.Value = hook.GetDriver();
            _logInPage = new LogInPage(WebFactory._driver.Value);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _logInPage.NavigateTo(SwagLabTestBDDFramework.Framework.Credentials.CredentialManager.GetBaseUrl());
        }

        [When(@"I enter ""([^""]*)"" as Username and ""([^""]*)"" as Password")]
        public void WhenIEnterAsUsernameAndAsPassword(string p0, string p1)
        {
            _logInPage.EnterUsernameAndPass(p0, p1);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _logInPage.ClickOnLogIn();
        }

        [Then(@"I should (.*)")]
        public void ThenIShouldSuccessful(string expectedOutcome)
        {
            if (expectedOutcome == "Successful")
            {
                var appLogo = _logInPage.GetAppLogoText();
                Assert.That(appLogo, Is.EqualTo("Swag Labs"));

            }
            else if (expectedOutcome == "Unsuccessful")
            {
                var SignIn = _logInPage.isLoginButtonDisplayed();
                Assert.That(SignIn, Is.True);

            }
            else if (expectedOutcome == "Unsuccessful")
            {
                var errorMsg = _logInPage.GetErrorText();
                Assert.That(errorMsg, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

            }
        }
    }
}
