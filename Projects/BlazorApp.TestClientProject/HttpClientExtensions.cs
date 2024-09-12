using System.Net.Http.Json;

namespace BlazorApp.TestClientProject.Client;

/// <summary>
/// Provides extension methods for the <see cref="HttpClient"/> type.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Sends a DELETE request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <param name="httpClient">The HttpClient to send the request with.</param>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="value">The value to serialize and include in the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>
    /// The method serializes the specified value and sends it as the body of the DELETE request.
    /// The method is only available in .NET 8.0 or later.
    /// </remarks>
    public static async Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(this HttpClient httpClient, string requestUri, TValue value, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(value),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(requestUri, UriKind.Relative)
        };

        return await httpClient.SendAsync(request, cancellationToken);
    }
}