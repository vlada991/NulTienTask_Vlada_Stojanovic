using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NulTienTask_Vlada_Stojanovic.pages
{
    public class LoginPage
    {
        private readonly IPage page;

        public LoginPage(IPage page) => this.page = page;

        public async Task PrijaviSeAsync(string email, string lozinka)
        {
            await page.Locator("#email").FillAsync(email);
                      await Task.Delay(5000);
            await page.Locator("#pass").FillAsync(lozinka);
            await page.Locator("#send2").First.ClickAsync();
        }
    }
}
