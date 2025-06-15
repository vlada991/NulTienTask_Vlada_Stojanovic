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

        public HomePage(IPage page) => this.page = page;

        public async Task PrihvatiKolaciceAsync()
        {
            await page.Locator("#cmpbntyestxt").ClickAsync();
        }

        public async Task<RegistrationPage> IdiNaFormuZaRegistraciju()
        {
            await page.Locator("//button[@data-toggle='dropdown']").ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Kreirajte korisnički nalog" }).ClickAsync();
            return new RegistrationPage(page);
        }

        public async Task<LoginPage> OtvoriPrijavuAsync()
        {
            await page.Locator("//button[@data-toggle='dropdown']").ClickAsync();
            var prijava = page.GetByRole(AriaRole.Link, new() { Name = "Prijava" });
            await prijava.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            await prijava.ClickAsync();
            return new LoginPage(page);
        }

        public async Task OtvoriNajprodavanijeAsync()
        {
            await page.Locator("a:has-text('Najprodavanije')").ClickAsync();
        }

        public async Task SkrolujDoCarouselaAsync()
        {
            var strelica = page.Locator("(//div[contains(@class, 'owl-drag')])[3]");
            await strelica.ScrollIntoViewIfNeededAsync();
            await page.EvaluateAsync("window.scrollBy(0, 190)");
        }

        public async Task<ProductPage> KlikniNaProizvodAsync()
        {
            var proizvod = page.Locator("a[title='JOOP! - Svetloplava muška košulja']").First;
            await proizvod.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 20000 });
            await proizvod.ClickAsync();

            return new ProductPage(page);
        }

        public async Task OtvoriPretraguAsync()
        {
            await page.Locator(".action-search i[title='Pretraga']").ClickAsync();
            await Task.Delay(2000); // možeš zameniti boljim čekanjem ako želiš
        }


        public async Task<SearchResults> PretraziProizvodAsync(string naziv)
        {
            await page.Locator("input#search").FillAsync(naziv);
            await page.Keyboard.PressAsync("Enter");
            await Task.Delay(3000);

            return new SearchResults(page);
        }

        public async Task<CategoryPage> NavigirajDoLogoMajicaAsync()
        {
            await page.Locator("a[href='/rs/muskarci/']").ClickAsync();
            await page.Locator("a[href='https://xyzfashionstore.com/rs/muskarci/odeca/']").HoverAsync();
            var majice = page.Locator("(//a[@href='https://xyzfashionstore.com/rs/muskarci/odeca/majice/'])[1]/preceding::*[1]");

            await majice.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });

            await majice.ClickAsync();
            await page.Locator("a[href='https://xyzfashionstore.com/rs/muskarci/odeca/majice/logo/']").ClickAsync();

            return new CategoryPage(page);
        }

    }
}
