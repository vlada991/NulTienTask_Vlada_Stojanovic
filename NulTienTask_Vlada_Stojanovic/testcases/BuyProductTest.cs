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
            // Inicijalizacija Page Object klasa sa IPage iz BaseTest
            var homePage = new HomePage(Page);
            var loginPage = new LoginPage(Page);
            var productPage = new ProductPage(Page);
            var cartPage = new CartPage(Page);

            await homePage.OtvoriNajprodavanijeAsync();
            await homePage.SkrolujDoCarouselaAsync();
            productPage = await homePage.KlikniNaProizvodAsync();

            await productPage.OdaberiVelicinuAsync();
            await productPage.DodajUKorpuAsync();

            cartPage = await productPage.OtvoriKorpuAsync();
            await cartPage.NastaviNaPlacanjeAsync();
        }
    }
}
