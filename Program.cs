using BlazorApp.HttpClientTest;
using BlazorApp.HttpClientTest.Services;
using BlazorApp.TestClientProject.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IHttpTest, HttpTest>(x =>
{
    x.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});
builder.Services.AddTestClient();

await builder.Build().RunAsync();
