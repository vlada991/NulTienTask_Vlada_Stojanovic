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
        private ILocator SelectedProduct => page.Locator("//div[contains(@class, 'product-top')]");
        private ILocator ResultsSearch => page.Locator(".base");
        public SearchResults(IPage page) => this.page = page;

        public async Task<ProductPage> SelectProduct(int index)
        {
            await SelectedProduct.Nth(index).ClickAsync();

            return new ProductPage(page);
        }

        public async Task<string> GetResultsSearchTextAsync()
        {
            return await ResultsSearch.InnerTextAsync();
        }
    }
}
