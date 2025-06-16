using NulTienTask_Vlada_Stojanovic.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.testcases
{
    [TestFixture]
    public class BuyProductSearchTest : BaseTest
    {
        [Test]
        public async Task BuyProductSearch()
        {
            var homePage = new HomePage(Page);
            var productPage = new ProductPage(Page);
            var cartPage = new CartPage(Page);
            var searchResultsPage = new SearchResults(Page);
            var paymentPage = new PaymentPage(Page);
            string expectedText = "BOSS Janet_Pump kožne bež salonke sa diskretnim logoom. Tanka potpetica.";
            string expected = "BOSS - Kožne bež salonke je dodat u korpu za kupovinu";

            //Click on magnifier and select boss brand
            await homePage.OpenSearchAsync();
            searchResultsPage = await homePage.SearchProductAsync();

            //Select product and check product desctiprion
            await searchResultsPage.SelectProduct(0);
            Assert.IsTrue(await productPage.IsProductDescriptionCorrectAsync(expectedText),
    "Product description is not correct");

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