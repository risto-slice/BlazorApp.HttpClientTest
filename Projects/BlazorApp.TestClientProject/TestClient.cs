namespace BlazorApp.TestClientProject.Client
{
    public class TestClient : ITestClient
    {
        private HttpClient _httpClient;

        public TestClient(HttpClient httpClient)
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
