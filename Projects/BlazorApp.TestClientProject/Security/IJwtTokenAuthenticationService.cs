using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Defines the methods for the JWT token authentication service.
/// </summary>
public interface IJwtTokenAuthenticationService
{
    /// <summary>
    /// Authenticates an JWT token request.
    /// </summary>
    /// <param name="request">The HTTP request to authenticate.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the authentication result.</returns>
    Task<AuthenticateResult> AuthenticateAsync(HttpRequest request, CancellationToken cancellationToken = default);
}