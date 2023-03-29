using PuppeteerSharp;

namespace DockerApiTest
{
    public static class IElementHandleExtensions
    {
        public static async Task<string?> GetTextAsync(this IElementHandle elementHandle)
        {
            return (await (await elementHandle.GetPropertyAsync("textContent")).JsonValueAsync()).ToString();
        }
    }
}
