using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class CartPage
    {
        private readonly IPage page;

        private ILocator EmptyTheCartBtn => page.Locator("button[title='Isprazni korpu za kupovinu']");
        private ILocator ProceedToCheckoutBtn => page.GetByText("Nastavite na plaćanje");
        private ILocator Amount => page.Locator(".amount[data-th='Ukupno']");
        private ILocator Quantity => page.Locator("input[title='Količina']");

        public CartPage(IPage page) => this.page = page;

        public async Task<PaymentPage> ProceedToCheckoutAsync()
        {
            await Amount.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 20000 });
            await ProceedToCheckoutBtn.ClickAsync();
            return new PaymentPage(page);
        }

        public async Task EmptyTheCartAsync() {
            await EmptyTheCartBtn.ClickAsync();
        }
        public async Task<int> GetQuantityValueAsync()
        {
            var valueString = await Quantity.InputValueAsync();
            return int.Parse(valueString);
        }
    }
}
