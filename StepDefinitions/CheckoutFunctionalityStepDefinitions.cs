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
    public class CheckoutFunctionalityStepDefinitions
    {

        private CheckOutProduct _checkOutProduct;
        private LogInPage _logInPage;

        public CheckoutFunctionalityStepDefinitions(Hooks1 hook)
        {
            WebFactory._driver.Value = hook.GetDriver();
            _checkOutProduct = new CheckOutProduct(WebFactory._driver.Value);
            _logInPage = new LogInPage(WebFactory._driver.Value);

        }

        [Given(@"am on the products page")]
        public void GivenAmOnTheProductsPage()
        {
            _logInPage.NavigateTo(SwagLabTestBDDFramework.Framework.Credentials.CredentialManager.GetBaseUrl());
            var (username, password) = CredentialManager.GetCredentials();
            _logInPage.EnterUsernameAndPass(username, password);
            _logInPage.ClickOnLogIn();
        }

        [When(@"I add the Bolt T-Shirt and Backpack to the cart")]
        public void WhenIAddTheBoltT_ShirtAndBackpackToTheCart()
        {
            _checkOutProduct.AddToCheckOut();

        }

        [When(@"I navigate to the cart and proceed to checkout")]
        public void WhenINavigateToTheCartAndProceedToCheckout()
        {
            _checkOutProduct.NavigateToCart();
            _checkOutProduct.Checkout();
        }

        [Then(@"I sucessfully completed the checkout  with the title ""([^""]*)""")]
        public void ThenISucessfullyCompletedTheCheckoutWithTheTitle(string p0)
        {
            if (p0 == "Checkout: Your Information")
            {
                var element = _checkOutProduct.GetCheckoutVerifyElement();
                Assert.That(element.Text, Is.EqualTo("Checkout: Your Information"));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
