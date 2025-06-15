using Microsoft.Playwright;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class CategoryPage
    {
        private readonly IPage page;

        public CategoryPage(IPage page)
        {
            this.page = page;
        }

        public async Task<ProductPage> OdaberiProizvod(int redniBroj)
        {
            await page.Locator("//div[contains(@class, 'product-top')]").Nth(redniBroj).ClickAsync();
            return new ProductPage(page);
        }
    }
}

