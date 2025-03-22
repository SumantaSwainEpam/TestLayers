using NUnit.Framework;
using PageLayers.Pages;
using SwagLabTestBDDFramework.Framework.Credentials;
using SwagLabTestBDDFramework.Framework.Drivers;
using System;
using TechTalk.SpecFlow;
using TestLayers.Hooks;

namespace TestLayers.StepDefinitions
{
    
    [Binding]
    public class OrderPlaceFunctionalityStepDefinitions
    {

        private OrderPlace _orderPlace;
        private LogInPage _logInPage;
        public OrderPlaceFunctionalityStepDefinitions(Hooks1 hook)
        {
            WebFactory._driver.Value = hook.GetDriver();
            _logInPage = new LogInPage(WebFactory._driver.Value);
            _orderPlace = new OrderPlace(WebFactory._driver.Value);

        }

        [Given(@"I am on the productpage")]
        public void GivenIAmOnTheProductpage()
        {
            _logInPage.NavigateTo(SwagLabTestBDDFramework.Framework.Credentials.CredentialManager.GetBaseUrl());
            var (username, password) = CredentialManager.GetCredentials();
            _logInPage.EnterUsernameAndPass(username, password);
            _logInPage.ClickOnLogIn();
        }

        [Given(@"I have added products to the cart")]
        public void GivenIHaveAddedProductsToTheCart()
        {
            _orderPlace.AddProductsToCart();
            _orderPlace.NavigateToCart();
        }

        [When(@"I enter my checkout information")]
        public void WhenIEnterMyCheckoutInformation()
        {
            _orderPlace.EnterCheckoutInformation();
        }

        [When(@"I complete the order")]
        public void WhenICompleteTheOrder()
        {
            _orderPlace.PlaceOrder();
        }

        [Then(@"I would see the order confirmation page ""([^""]*)""")]
        public void ThenIWouldSeeTheOrderConfirmationPage(string p0)
        {
            if (p0 == "Thank you for your order!")
            {
                var pageTitle = _orderPlace.GetProductPageTitle();
                Assert.That(pageTitle, Is.EqualTo("Thank you for your order!"));
            }
            else
            {
                Assert.Fail();

            }
        }
    }
}
