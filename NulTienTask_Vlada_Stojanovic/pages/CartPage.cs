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

        public CartPage(IPage page) => this.page = page;

        public async Task NastaviNaPlacanjeAsync()
        {
            await page.GetByText("Nastavite na plaćanje").ClickAsync();
        }

        public async Task IsprazniKorpuAsync() {
            await page.Locator("button[title='Isprazni korpu za kupovinu']").ClickAsync();
        }
    }
}
