using NulTienTask_Vlada_Stojanovic.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.testcases
{
    [TestFixture]
    public class BuyProductCategoryTest : BaseTest
    {
        [Test]
        public async Task BuyProductCategory()
        {
            var homePage = new HomePage(Page);
            var productPage = new ProductPage(Page);
            var cartPage = new CartPage(Page);
            var categoryPage = new CategoryPage(Page);
            var paymentPage = new PaymentPage(Page);
            string expectedText = "Peuterey muška logo majica kratkih rukava. Model od pamuka. Bela boja.";
            string expected = "Peuterey - Muška logo majica je dodat u korpu za kupovinu";

            //Navigate to the Logo T-Shirt page and verify that the page has loaded.
            categoryPage = await homePage.NavigateToLogoTShirtAsync();
            Assert.IsTrue(await categoryPage.IsCategoryLoadedAsync(), "Category page did not load correctly.");

            //Select a product and verify that the product description is correct.
            productPage = await categoryPage.SelectProduct(1);
            Assert.IsTrue(await productPage.IsProductDescriptionCorrectAsync(expectedText),
                "Product description is not correct");

            //Select a size, add the product to the cart, and verify that the correct message appears confirming the product was added to the cart
            await productPage.SelectSizeAsync();
            await productPage.AddToCartAsync();
            Assert.IsTrue(await productPage.IsConfirmationMessageTextAsync(expected), "The confirmation message text does not match.");

            //Navigate to the cart and verify that the quantity is correct (in this case, 1)
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
