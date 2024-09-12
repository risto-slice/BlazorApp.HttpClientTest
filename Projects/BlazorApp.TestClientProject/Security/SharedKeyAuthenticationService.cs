using System.Security.Claims;
using BlazorApp.TestClientProject.Client.Properties;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Provides methods for shared key authentication.
/// </summary>
public class SharedKeyAuthenticationService : ISharedKeyAuthenticationService
{
    private readonly SharedKeyAuthenticationConfiguration _authConfig;
    private readonly ILogger<SharedKeyAuthenticationService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SharedKeyAuthenticationService"/> class.
    /// </summary>
    /// <param name="authConfig">The configuration for shared key authentication.</param>
    /// <param name="logger">The logger to use.</param>
    public SharedKeyAuthenticationService(
        SharedKeyAuthenticationConfiguration authConfig,
        ILogger<SharedKeyAuthenticationService> logger)
    {
        _authConfig = authConfig;
        _logger = logger;
    }

    /// <inheritdoc />
    public virtual async Task<AuthenticateResult> AuthenticateAsync(
        HttpRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!request.Headers.ContainsKey(_authConfig.WebHookSignatureName))
        {
            return AuthenticateResult.Fail(Resources.AuthenticationUnauthorized);
        }

        try
        {
            var hash = "request.ExtractHeaderValues(_authConfig.WebHookSignatureName).First()";
            var body = "await request.GetRequestBodyAsync(cancellationToken: cancellationToken)";

            return HasValidWebHookSignature(request, hash, body)
                ? AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>()), _authConfig.Scheme))
                : AuthenticateResult.Fail(Resources.AuthenticationAccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Authentication Error");
            return AuthenticateResult.Fail(Resources.AuthenticationAccessDenied);
        }
    }

    /// <summary>
    /// Checks if a request has a valid web hook signature.
    /// </summary>
    /// <param name="request">The HTTP request to check.</param>
    /// <param name="hash">The hash to check against.</param>
    /// <param name="body">The body of the request.</param>
    /// <returns>True if the request has a valid web hook signature; otherwise, false.</returns>
    protected virtual bool HasValidWebHookSignature(HttpRequest request, string hash, string body)
    {
        return _authConfig.WebHookKeys.Any(key =>
        {
            var webHookSignature = new SharedKeySecurityTokenHandler(key);
            return webHookSignature.IsValid(hash, body, request.Path);
        });
    }

    protected virtual bool HasValidWebHookSignature(HttpRequestData request, string hash, string body)
    {
        return _authConfig.WebHookKeys.Any(key =>
        {
            var webHookSignature = new SharedKeySecurityTokenHandler(key);
            return webHookSignature.IsValid(hash, body, PathString.FromUriComponent(request.Url));
        });
    }
}