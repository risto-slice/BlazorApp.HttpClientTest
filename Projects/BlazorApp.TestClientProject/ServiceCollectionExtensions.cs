using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.TestClientProject.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<ITestClient, TestClient>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            });

            return serviceCollection;
        }
    }
}
