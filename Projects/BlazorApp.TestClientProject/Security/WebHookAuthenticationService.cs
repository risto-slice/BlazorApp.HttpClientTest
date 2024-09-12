using Microsoft.Extensions.Logging;

namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Provides methods for authentication.
/// </summary>
public class WebHookAuthenticationService : SharedKeyAuthenticationService, IWebHookAuthenticationService
{
    /// <summary>
    /// Default scheme for authentication.
    /// </summary>
    public const string DefaultScheme = "X-BlazorTest-WebHook";

    /// <summary>
    /// Default web hook signature name for authentication.
    /// </summary>
    public const string DefaultWebHookSignatureName = "X-BlazorTest-Signature";

    /// <summary>
    /// Initializes a new instance of the <see cref="WebHookAuthenticationService"/> class.
    /// </summary>
    /// <param name="authConfig">The configuration for authentication.</param>
    /// <param name="logger">The logger to use.</param>
    public WebHookAuthenticationService(
        WebHookAuthenticationConfiguration authConfig,
        ILogger<WebHookAuthenticationService> logger) : base(authConfig, logger)
    {
    }
}