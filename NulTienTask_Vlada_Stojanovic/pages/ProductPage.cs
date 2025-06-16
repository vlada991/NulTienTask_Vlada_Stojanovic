using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class ProductPage
    {
        private readonly IPage page;
        private ILocator Size => page.Locator("div.swatch-option.text");
        private ILocator AddToCartBtn => page.Locator("//button[@title='Dodajte u korpu']");
        private ILocator CartIcon => page.Locator(".action.showcart");
        private ILocator GoToCart => page.Locator("a:has-text('Idite na korpu')");

        private ILocator SelectedProduct =>  page.Locator("#attributedescription");

        private ILocator ConfirmationMessage => page.Locator("//div[@data-bind='html: message.text']");
        public ProductPage(IPage page) => this.page = page;

        public async Task SelectSizeAsync()
        {
            await Size.First.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5000 });
            int count = await Size.CountAsync();

            if (count > 0)
            {
                await Size.Nth(count - 3).ClickAsync();
            }
            else
            {
                Console.WriteLine("Nema veličina!");
            }
        }

        public async Task AddToCartAsync()
        {
            await AddToCartBtn.ClickAsync();
        }

        public async Task<CartPage> GoToCartAsync()
        {
            await CartIcon.ClickAsync();
            await GoToCart.ClickAsync();

            return new CartPage(page);
        }

        public async Task<bool> IsProductDescriptionCorrectAsync(string expectedDescription)
        {
            await SelectedProduct.WaitForAsync(new() { Timeout = 10000 });
            string actualText = await SelectedProduct.InnerTextAsync();
            return actualText.Trim() == expectedDescription.Trim();
        }

        public async Task<bool> IsConfirmationMessageTextAsync(string expectedText)
        {
            await ConfirmationMessage.WaitForAsync(new() { Timeout = 10000 });
            string actualText = await ConfirmationMessage.InnerTextAsync();
            return actualText.Trim() == expectedText.Trim();
        }
    }
}
