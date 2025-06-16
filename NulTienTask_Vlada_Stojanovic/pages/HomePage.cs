using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class HomePage
    {
        private readonly IPage page;
        private ILocator AcceptCookies => page.Locator("#cmpbntyestxt");
        private ILocator UserIcon => page.Locator("//button[@data-toggle='dropdown']");
        private ILocator CreateUserAccount => page.GetByRole(AriaRole.Link, new() { Name = "Kreirajte korisnički nalog" });
        private ILocator MostPopular => page.Locator("a:has-text('Najprodavanije')");
        private ILocator LogIn => page.GetByRole(AriaRole.Link, new() { Name = "Prijava" });
        private ILocator ScrollToCarousel => page.Locator("(//div[contains(@class, 'owl-drag')])[3]");
        private ILocator MostPopularProduct => page.Locator("a[title='JOOP! - Svetloplava muška košulja']").First;
        private ILocator SearchMagnifier => page.Locator(".action-search i[title='Pretraga']");
        private ILocator SearchTextField => page.Locator("input#search");
        private ILocator MensTab => page.Locator("a[href='/rs/muskarci/']");
        private ILocator ClothesTab => page.Locator("a[href='https://xyzfashionstore.com/rs/muskarci/odeca/']");
        private ILocator TShirts => page.Locator("(//a[@href='https://xyzfashionstore.com/rs/muskarci/odeca/majice/'])[1]/preceding::*[1]");
        private ILocator Logo => page.Locator("a[href='https://xyzfashionstore.com/rs/muskarci/odeca/majice/logo/']");

        private ILocator MostPopularTab => page.Locator("//a[@data-toggle='tab' and text()='Najprodavanije']/..");

        private ILocator BossProducts => page.Locator("img[alt='Boss']");

        private ILocator Magnifier => page.Locator("i[title='Pretraga']");


        public HomePage(IPage page) => this.page = page;

        public async Task AcceptCookiesMethod()
        {
            await AcceptCookies.ClickAsync();
        }

        public async Task<RegistrationPage> GoToRegistrationForm()
        {
            await UserIcon.ClickAsync();
            await CreateUserAccount.ClickAsync();
            return new RegistrationPage(page);
        }

        public async Task<LoginPage> OpenLoginPage()
        {
            await UserIcon.ClickAsync();
            await LogIn.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            await LogIn.ClickAsync();
            return new LoginPage(page);
        }

        public async Task OpenMostPopularAsync()
        {
            await MostPopular.ClickAsync();
        }

        public async Task ScrollToCarouselAsync()
        {
            await ScrollToCarousel.ScrollIntoViewIfNeededAsync();
            await page.EvaluateAsync("window.scrollBy(0, 190)");
        }

        public async Task<ProductPage> ClickOnProductAsync()
        {
            await MostPopularProduct.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 20000 });
            await MostPopularProduct.ClickAsync();

            return new ProductPage(page);
        }

        public async Task OpenSearchAsync()
        {
            await SearchMagnifier.ClickAsync();
        }


        public async Task<SearchResults> SearchProductAsync()
        {
            await SearchTextField.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });
            await BossProducts.ClickAsync();

            return new SearchResults(page);
        }

        public async Task<CategoryPage> NavigateToLogoTShirtAsync()
        {
            await MensTab.ClickAsync();
            await ClothesTab.HoverAsync();

            await TShirts.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });

            await TShirts.ClickAsync();
            await Logo.ClickAsync();

            return new CategoryPage(page);
        }

        public async Task<bool> IsMostPopularTabActiveAsync()
        {
            var classAttribute = await MostPopularTab.GetAttributeAsync("class");
            return classAttribute != null && classAttribute.Split(' ').Contains("active");
        }

    }
}
