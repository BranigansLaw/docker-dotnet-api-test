using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;

namespace DockerApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CromiumController : Controller
    {
        [HttpGet(Name = "GetCromiumTest")]
        public async Task<IActionResult> GetCromiumTestAsync()
        {
            float value;
            using (IBrowser browser = await CreatePuppeteerBrowserAsync())
            {
                using (IPage page = await browser.NewPageAsync())
                {
                    await page.GoToAsync("http://www.worldgovernmentbonds.com/country/canada/");

                    IElementHandle marketPriceResult = await page.WaitForXPathAsync($"//table//tbody//tr//td[position()=3]//b");
                    string pulled = await marketPriceResult.GetTextAsync() ?? "";
                    value = float.Parse(pulled.Substring(0, pulled.Length - 1));
                }
            }

            return Ok(value);
        }

        /// <summary>
        /// Creates a puppeteer browser
        /// </summary>
        /// <returns>The created <see cref="Browser"/></returns>
        private async Task<IBrowser> CreatePuppeteerBrowserAsync()
        {
            return await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] {
                  "--disable-gpu",
                  "--disable-dev-shm-usage",
                  "--disable-setuid-sandbox",
                  "--no-sandbox"
                }
            });
        }
    }
}
