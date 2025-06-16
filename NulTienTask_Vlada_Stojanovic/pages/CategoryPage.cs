using Microsoft.Playwright;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class CategoryPage
    {
        private readonly IPage page;
        private ILocator Product => page.Locator("//div[contains(@class, 'product-top')]");
        private ILocator TShirtsLogo => page.Locator(".base");

        public CategoryPage(IPage page)
        {
            this.page = page;
        }

        public async Task<ProductPage> SelectProduct(int index)
        {
            await Product.Nth(index).ClickAsync();
            return new ProductPage(page);
        }

        public async Task<bool> IsCategoryLoadedAsync()
        {
            return await TShirtsLogo.IsVisibleAsync();
        }
    }
}

