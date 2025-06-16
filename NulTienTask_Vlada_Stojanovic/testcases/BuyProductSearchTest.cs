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
            // Inicijalizacija Page Object klasa sa IPage iz BaseTest
            var homePage = new HomePage(Page);
            var productPage = new ProductPage(Page);
            var cartPage = new CartPage(Page);
            var searchResultsPage = new SearchResults(Page);
            var paymentPage = new PaymentPage(Page);
            string expectedText = "BOSS Janet_Pump kožne bež salonke sa diskretnim logoom. Tanka potpetica.";
            string expected = "BOSS - Kožne bež salonke je dodat u korpu za kupovinu";

            //Klikni na magnifier
            await homePage.OpenSearchAsync();

            //Trazi proizvod
            searchResultsPage = await homePage.SearchProductAsync();

            //Selektuj proizvod i proveri da li je opis proizvoda tacan
            await searchResultsPage.OdaberiProizvod(0);
            Assert.IsTrue(await productPage.IsProductDescriptionCorrectAsync(expectedText),
    "Opis proizvoda nije tačan.");

            //Selektuj velicinu i dodaj u korpu i proveri da li je ispravna poruka o dodatom proizvodu u korpu
            await productPage.SelectSizeAsync();
            await productPage.AddToCartAsync();
            Assert.IsTrue(await productPage.IsConfirmationMessageTextAsync(expected), "The confirmation message text does not match.");

            //Navigiraj do korpe i proveri da li je kolicina ispravna (u ovom slucaju 1)
            cartPage = await productPage.GoToCartAsync();
            Assert.AreEqual(1, await cartPage.GetQuantityValueAsync(), "The quantity value is not equal to 1.");

            //Produzi na placanje
            paymentPage = await cartPage.ProceedToCheckoutAsync();
            Assert.IsTrue(await paymentPage.IsPaymentScreenLoadedAsync(), "Payment screen did not load correctly.");

            //Unesi podatke za placanje
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