using Microsoft.Playwright;
using NulTienTask_Vlada_Stojanovic;
using NulTienTask_Vlada_Stojanovic.pages;

public class BaseTest
{
    protected IPage Page => GlobalSetup.Page;
    protected CartPage cartPage;

    [SetUp]
    public async Task SetUpAsync()
    {
        // Svaki test kreće od početne stranice
        await Page.GotoAsync("https://rs.shop.xyz.fashion/");
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await Page.GotoAsync("https://xyzfashionstore.com/rs/checkout/cart/");
        cartPage = new CartPage(Page);
        await cartPage.EmptyTheCartAsync();
    }
}

