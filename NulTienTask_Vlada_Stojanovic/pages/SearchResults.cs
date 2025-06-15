using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class SearchResults
    {
        private readonly IPage page;

        public SearchResults(IPage page) => this.page = page;

        public async Task<ProductPage> OdaberiProizvod(int index)
        {
            await page.Locator("//div[contains(@class, 'product-top')]").Nth(index).ClickAsync();

            return new ProductPage(page);
        }
    }
}
