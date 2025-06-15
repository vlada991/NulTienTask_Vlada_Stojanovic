using Microsoft.Playwright;
using NUnit.Framework;
using NulTienTask_Vlada_Stojanovic.pages;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic
{
    [SetUpFixture]
    public class GlobalSetup
    {
        public static IPlaywright Playwright;
        public static IBrowser Browser;
        public static IBrowserContext Context;
        public static IPage Page;

        [OneTimeSetUp]
        public static async Task GlobalOneTimeSetUp()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new() { Headless = false });
            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();

            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync("https://rs.shop.xyz.fashion/");


            var homePage = new HomePage(Page);
            await homePage.PrihvatiKolaciceAsync();

            // Registracija korisnika
            var registrationPage = await homePage.IdiNaFormuZaRegistraciju();
            string randomEmail = $"nultientest_{DateTime.Now.Ticks}@mailinator.com";
            await registrationPage.RegistrujNovogKorisnikaAsync(
                "Pera",
                "Petic",
                 randomEmail,
                "NulTienTest123"
            );

            await Page.GotoAsync("https://www.mailinator.com/");
            await Page.Locator("#search").FillAsync(randomEmail);
            await Page.Locator("//button[@value=\"Search for public inbox for free\"]").ClickAsync();
            await Page.GetByText("Potvrdite Vaš nalog na XYZ - Srbija").ClickAsync();
            var frame = Page.Frame("html_msg_body");
            var popupTask = Page.Context.WaitForPageAsync();
            await frame.Locator("xpath=//a[text()='Potvrdite Vaš nalog']").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync();

            // Odmah zatvori novi tab
            await newPage.CloseAsync();


            // Prihvatanje kolačića i login
            /* await Page.GotoAsync("https://rs.shop.xyz.fashion/");
             var loginPage = await homePage.OtvoriPrijavuAsync();
             await loginPage.PrijaviSeAsync(randomEmail, "Test12345");*/
        }

        [OneTimeTearDown]
        public static async Task GlobalOneTimeTearDown()
        {
            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}
