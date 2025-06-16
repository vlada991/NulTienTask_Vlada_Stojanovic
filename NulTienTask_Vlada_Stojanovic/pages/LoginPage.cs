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
        private ILocator EMail => page.Locator("#email");
        private ILocator Pass => page.Locator("#pass");
        private ILocator Send => page.Locator("#send2");
        public LoginPage(IPage page) => this.page = page;

        public async Task LogInAsync(string email, string lozinka)
        {
            await EMail.FillAsync(email);
            await Pass.FillAsync(lozinka);
            await Send.First.ClickAsync();
        }
    }
}
