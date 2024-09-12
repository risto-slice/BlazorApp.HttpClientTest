using Microsoft.Extensions.Logging;

namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Provides methods for authentication.
/// </summary>
public class AdministrationAuthenticationService : JwtTokenAuthenticationService, IAdministrationAuthenticationService
{
    /// <summary>
    /// Default scheme for administration authentication.
    /// </summary>
    public const string DefaultScheme = "Bearer";

    /// <summary>
    /// Default issuer for authentication.
    /// </summary>
    public const string DefaultIssuer = "urn:testclient";

    /// <summary>
    /// Initializes a new instance of the <see cref="AdministrationAuthenticationService"/> class.
    /// </summary>
    /// <param name="authConfig">The configuration for authentication.</param>
    /// <param name="logger">The logger to use.</param>
    public AdministrationAuthenticationService(
        AdministrationAuthenticationConfiguration authConfig,
        ILogger<AdministrationAuthenticationService> logger) : base(authConfig, logger)
    {
    }
}