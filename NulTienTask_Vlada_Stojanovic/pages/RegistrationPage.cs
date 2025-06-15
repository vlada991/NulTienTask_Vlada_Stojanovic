using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class RegistrationPage
    {
        private readonly IPage page;


        public RegistrationPage(IPage page)
        {
            this.page = page;
        }

        public async Task RegistrujNovogKorisnikaAsync(string firstName, string lastName, string email, string password)
        {
            await page.Locator("#firstname").FillAsync(firstName);
            await page.Locator("#lastname").FillAsync(lastName);
            await page.Locator("#email_address").FillAsync(email);
            await page.Locator("#password").FillAsync(password);
            await page.Locator("#password-confirmation").FillAsync(password);
            await page.Locator("//input[@title='Privacy Checkbox']").CheckAsync();
            await page.Locator("//input[@title='Agree']").CheckAsync();
            await page.Locator("//button[@title='Kreirajte korisnički nalog']").ClickAsync();
        }
    }
}
