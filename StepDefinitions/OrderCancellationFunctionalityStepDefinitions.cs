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
    public class OrderCancellationFunctionalityStepDefinitions
    {

        private CancelOrder _cancelOrder;
        private LogInPage _logInPage;

        public OrderCancellationFunctionalityStepDefinitions(Hooks1 hook)
        {
            WebFactory._driver.Value = hook.GetDriver();
            _cancelOrder = new CancelOrder(WebFactory._driver.Value);
            _logInPage=new LogInPage(WebFactory._driver.Value);

        }


        [Given(@"I am on the products page")]
        public void GivenIAmOnTheProductsPage()
        {
            _logInPage.NavigateTo(SwagLabTestBDDFramework.Framework.Credentials.CredentialManager.GetBaseUrl());
            var (username, password) = CredentialManager.GetCredentials();
            _logInPage.EnterUsernameAndPass(username, password);
            _logInPage.ClickOnLogIn();
        }

        [When(@"I add a backpack and a bike light to the cart")]
        public void WhenIAddABackpackAndABikeLightToTheCart()
        {
            _cancelOrder.AddProductsToCart();
        }

        [When(@"I navigate to the cart")]
        public void WhenINavigateToTheCart()
        {
           
            _cancelOrder.NavigateToCart();
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            _cancelOrder.ProceedToCheckout();
        }

        [When(@"I enter checkout information with first name ""([^""]*)"", last name ""([^""]*)"", and zip code ""([^""]*)""")]
        public void WhenIEnterCheckoutInformationWithFirstNameLastNameAndZipCode(string sumanta, string swain, string p2)
        {
            _cancelOrder.EnterCheckoutInformation(sumanta, swain, p2);
        }

        [When(@"I cancel the order")]
        public void WhenICancelTheOrder()
        {
            _cancelOrder.cancelOrder();
        }

        [Then(@"I would be on the product page")]
        public void ThenIWouldBeOnTheProductPage()
        {
           
            var pageTitle = _cancelOrder.GetProductPageTitle();
            Assert.That(pageTitle, Is.EqualTo("Products"), "The page title should be 'Products' after cancelling the order.");
        }
    }
}
