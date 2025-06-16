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

            //Open the page and wait for it to load
            await Page.GotoAsync("https://rs.shop.xyz.fashion/");
            await Page.Locator(".dark-logo").First.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            //Accept cookies
            var homePage = new HomePage(Page);
            await homePage.AcceptCookiesMethod();

            //User registration
            var registrationPage = await homePage.GoToRegistrationForm();
            string randomEmail = $"nultientest_{DateTime.Now.Ticks}@mailinator.com";
            await registrationPage.RegisterUserAsync(
                "Pera",
                "Peric",
                 randomEmail,
                "Test12345"
            );

            //Navigate to mailinator, open e-mail and click the link from the e-mail to verify account
            await Page.GotoAsync("https://www.mailinator.com/");
            await Page.Locator("#search").FillAsync(randomEmail);
            await Page.Locator("//button[@value=\"Search for public inbox for free\"]").ClickAsync();
            await Page.GetByText("Potvrdite Vaš nalog na XYZ - Srbija").ClickAsync();
            var frame = Page.Frame("html_msg_body");
            var popupTask = Page.Context.WaitForPageAsync();
            await frame.Locator("xpath=//a[text()='Potvrdite Vaš nalog']").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync();

            //Immediately close the new tab that opens after clicking the link from the verification email
            await newPage.CloseAsync();
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
