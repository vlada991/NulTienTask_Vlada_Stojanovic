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
            // Inicijalizacija Page Object klasa sa IPage iz BaseTest
            var homePage = new HomePage(Page);
            var loginPage = new LoginPage(Page);
            var productPage = new ProductPage(Page);
            var cartPage = new CartPage(Page);
            var categoryPage = new CategoryPage(Page);

            categoryPage = await homePage.NavigirajDoLogoMajicaAsync();
            productPage = await categoryPage.OdaberiProizvod(1);

            await productPage.OdaberiVelicinuAsync();
            await productPage.DodajUKorpuAsync();

            cartPage = await productPage.OtvoriKorpuAsync();
            await cartPage.NastaviNaPlacanjeAsync();
        }
    }
}
