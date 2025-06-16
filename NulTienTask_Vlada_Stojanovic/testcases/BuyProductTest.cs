using NulTienTask_Vlada_Stojanovic.pages;
using NulTienTask_Vlada_Stojanovic.testcases;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.testcases
{
    [TestFixture]
    public class BuyProductTest : BaseTest
    {
        [Test]
        public async Task BuyProduct()
        {
            var homePage = new HomePage(Page);
            var productPage = new ProductPage(Page);
            var cartPage = new CartPage(Page);
            var searchResultsPage = new SearchResults(Page);
            var paymentPage = new PaymentPage(Page);
            string expectedText = "JOOP! Pit-W svetloplava muška košulja od lana i pamuka. Basic model sa kopčanjem na dugmiće.";
            string expected = "JOOP! - Svetloplava muška košulja je dodat u korpu za kupovinu";

            //Select the "Most Popular" tab and verify that it is selected.
            await homePage.OpenMostPopularAsync();
            Assert.IsTrue(await homePage.IsMostPopularTabActiveAsync(), "Most popular tab is not selected");

            //Scroll to the carousel and select a product.
            await homePage.ScrollToCarouselAsync();
            productPage = await homePage.ClickOnProductAsync();

            //Select the size, add the product to the cart, and verify that the correct confirmation message appears
            await productPage.SelectSizeAsync();
            await productPage.AddToCartAsync();
            Assert.IsTrue(await productPage.IsConfirmationMessageTextAsync(expected), "The confirmation message text does not match.");

            //Navigate to the cart and check that the quantity is correct (in this case, 1)
            cartPage = await productPage.GoToCartAsync();
            Assert.AreEqual(1, await cartPage.GetQuantityValueAsync(), "The quantity value is not equal to 1.");

            //Proceed to checkout
            paymentPage = await cartPage.ProceedToCheckoutAsync();
            Assert.IsTrue(await paymentPage.IsPaymentScreenLoadedAsync(), "Payment screen did not load correctly.");

            //Enter the payment details
            await paymentPage.EnterFirstNameAsync("Petar");
            await paymentPage.EnterSecondNameAsync("Peric");
            await paymentPage.EnterAddressAsync("Bulevar Kralja Aleksandra");
            await paymentPage.SelectCountryAsync("Srbija");
            await paymentPage.EnterCityAsync("Beograd");
            await paymentPage.EnterPostalCodeAsync("11000");
            await paymentPage.EnterPhoneNoAsync("060 111 222");
        }
    }
}