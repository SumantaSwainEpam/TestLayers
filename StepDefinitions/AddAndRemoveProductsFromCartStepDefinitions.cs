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
    public class AddAndRemoveProductsFromCartStepDefinitions
    {
        private ProductPage _productPage;
        private LogInPage _logInPage;

        public AddAndRemoveProductsFromCartStepDefinitions(Hooks1 _hook)
        {
            WebFactory._driver.Value = _hook.GetDriver();
            _logInPage = new LogInPage(WebFactory._driver.Value);
            _productPage = new ProductPage(WebFactory._driver.Value);

        }

        [When(@"I add the backpack and bike light to the cart")]
        public void WhenIAddTheBackpackAndBikeLightToTheCart()
        {

            _logInPage.NavigateTo(SwagLabTestBDDFramework.Framework.Credentials.CredentialManager.GetBaseUrl());
            var (username, password) = CredentialManager.GetCredentials();
            _logInPage.EnterUsernameAndPass(username, password);
            _logInPage.ClickOnLogIn();
            _productPage.AddTheProduct();
        }

        [Then(@"the cart should contain the backpack and bike light")]
        public void ThenTheCartShouldContainTheBackpackAndBikeLight()
        {
            _productPage.NavigateToCart();
            var cartCount = _productPage.GetCartCount();
            Assert.That(cartCount, Is.EqualTo("2"), "Cart count did not match expected value.");  //2
        }

        [When(@"I remove the backpack and bike light from the cart")]
        public void WhenIRemoveTheBackpackAndBikeLightFromTheCart()
        {

            _productPage.AddTheProduct();
            //Navigate to Cart
            _productPage.NavigateToCart();
            //Count the product that add to cart
            var countAfterAdd = _productPage.GetCartCount();
            //Remove From Cart
            _productPage.RemoveProducts();
        }

        [Then(@"the cart should be empty")]
        public void ThenTheCartShouldBeEmpty()
        {
            var countAfterRemove = _productPage.GetCartCount();
            Assert.That(countAfterRemove, Is.EqualTo(""), "Product count in cart should be 0 after removing products."); //""
        }
    }
}
