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
        private ILocator FirstNameReg => page.Locator("#firstname");
        private ILocator SecondNameReg => page.Locator("#lastname");
        private ILocator EmailAddress => page.Locator("#email_address");
        private ILocator PassReg => page.Locator("#password");
        private ILocator PassRegConfirmation => page.Locator("#password-confirmation");
        private ILocator PrivacyCheckbox => page.Locator("//input[@title='Privacy Checkbox']");
        private ILocator AgreeCheckbox => page.Locator("//input[@title='Agree']");
        private ILocator CreateAccBtn => page.Locator("//button[@title='Kreirajte korisnički nalog']");


        public RegistrationPage(IPage page)
        {
            this.page = page;
        }

        public async Task RegisterUserAsync(string firstName, string lastName, string email, string password)
        {
            await FirstNameReg.FillAsync(firstName);
            await SecondNameReg.FillAsync(lastName);
            await EmailAddress.FillAsync(email);
            await PassReg.FillAsync(password);
            await PassRegConfirmation.FillAsync(password);
            await PrivacyCheckbox.CheckAsync();
            await AgreeCheckbox.CheckAsync();
            await CreateAccBtn.ClickAsync();
        }
    }
}
