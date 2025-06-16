using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class PaymentPage
    {
        private readonly IPage page;
        private ILocator FirstName => page.Locator("input[name='firstname']");
        private ILocator SecondName => page.Locator("input[name='lastname']");
        private ILocator Address => page.Locator("input[name='street[0]']");
        private ILocator Country => page.Locator("select[name='country_id']");
        private ILocator City => page.Locator("input[name='city']");
        private ILocator PostalCode => page.Locator("input[name='postcode']");
        private ILocator PhoneNo => page.Locator("input[name='telephone']");

        private ILocator PaymentScreen => page.Locator(".base");

        public PaymentPage(IPage page) => this.page = page;

        public async Task EnterFirstNameAsync(string firstName)
        {
            await FirstName.FillAsync(firstName);
        }

        public async Task EnterSecondNameAsync(string secondName)
        {
            await SecondName.FillAsync(secondName);
        }

        public async Task EnterAddressAsync(string address)
        {
            await Address.FillAsync(address);
        }

        public async Task SelectCountryAsync(string country)
        {
            await Country.SelectOptionAsync(country);
        }

        public async Task EnterCityAsync(string city)
        {
            await City.FillAsync(city); 
        }

        public async Task EnterPostalCodeAsync(string postalCode)
        {
            await PostalCode.FillAsync(postalCode);
        }
        public async Task EnterPhoneNoAsync(string phoneNo)
        {
            await PhoneNo.FillAsync(phoneNo);   
        }
        public async Task<bool> IsPaymentScreenLoadedAsync()
        {
            await PaymentScreen.WaitForAsync(new() { Timeout = 10000 });
            return await PaymentScreen.IsVisibleAsync();
        }
    }
}
