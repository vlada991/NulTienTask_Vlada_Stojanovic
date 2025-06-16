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
        private ILocator Logo => Page.Locator(".dark-logo");

        [OneTimeSetUp]
        public static async Task GlobalOneTimeSetUp()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new() { Headless = false });
            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                ScreenSize = new ScreenSize { Width = 1920, Height = 1080 }
            });
            Page = await Context.NewPageAsync();

            //Otvori stranicu i sacekaj da se ucita
            await Page.GotoAsync("https://rs.shop.xyz.fashion/");
            await Page.Locator(".dark-logo").First.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            //Prihvati kolacicee
            var homePage = new HomePage(Page);
            await homePage.AcceptCookiesMethod();

            // Registracija korisnika
            var registrationPage = await homePage.GoToRegistrationForm();
            string randomEmail = $"nultientest_{DateTime.Now.Ticks}@mailinator.com";
            await registrationPage.RegisterUserAsync(
                "Pera",
                "Peric",
                 randomEmail,
                "Test12345"
            );

            //Verifikuj email adresu
            await Page.GotoAsync("https://www.mailinator.com/");
            await Page.Locator("#search").FillAsync(randomEmail);
            await Page.Locator("//button[@value=\"Search for public inbox for free\"]").ClickAsync();
            await Page.GetByText("Potvrdite Vaš nalog na XYZ - Srbija").ClickAsync();
            var frame = Page.Frame("html_msg_body");
            var popupTask = Page.Context.WaitForPageAsync();
            await frame.Locator("xpath=//a[text()='Potvrdite Vaš nalog']").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync();

            // Odmah zatvori novi tab koji se otvara nakon sto se klikne na link iz verifikacionom mejla
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
