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

        public ProductPage(IPage page) => this.page = page;

        public async Task OdaberiVelicinuAsync()
        {
            var options = page.Locator("div.swatch-option.text");

            await options.First.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5000 });
            int count = await options.CountAsync();

            if (count > 0)
            {
                await options.Nth(count - 3).ClickAsync();
            }
            else
            {
                Console.WriteLine("Nema veličina!");
            }
        }

        public async Task DodajUKorpuAsync()
        {
            await page.Locator("//button[@title='Dodajte u korpu']").ClickAsync();
        }

        public async Task<CartPage> OtvoriKorpuAsync()
        {
            await page.Locator(".action.showcart").ClickAsync();
            await page.Locator("a:has-text('Idite na korpu')").ClickAsync();

            return new CartPage(page);
        }
    }
}
