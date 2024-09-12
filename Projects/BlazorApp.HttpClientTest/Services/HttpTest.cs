
namespace BlazorApp.HttpClientTest.Services
{
    public class HttpTest : IHttpTest
    {
        private HttpClient _httpClient;

        public HttpTest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TestAsync()
        {
            var response = await _httpClient.GetAsync("todos");

            return "something";
        }
    }
}
