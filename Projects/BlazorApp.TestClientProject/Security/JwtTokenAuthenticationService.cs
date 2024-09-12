using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BlazorApp.TestClientProject.Client.Properties;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Provides methods for JWT token authentication.
/// </summary>
public class JwtTokenAuthenticationService : IJwtTokenAuthenticationService
{
    private readonly JwtTokenAuthenticationConfiguration _authConfig;
    private readonly ILogger<JwtTokenAuthenticationService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenAuthenticationService"/> class.
    /// </summary>
    /// <param name="authConfig">The configuration for JWT token authentication.</param>
    /// <param name="logger">The logger to use.</param>
    public JwtTokenAuthenticationService(
        JwtTokenAuthenticationConfiguration authConfig,
        ILogger<JwtTokenAuthenticationService> logger)
    {
        _authConfig = authConfig;
        _logger = logger;
    }

    /// <inheritdoc />
    public virtual Task<AuthenticateResult> AuthenticateAsync(
        HttpRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return Task.FromResult(AuthenticateResult.Fail(Resources.AuthenticationUnauthorized));
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var bearerToken = "request.ExtractBearerTokenFromHeader(_authConfig.Scheme)";
            if (!tokenHandler.CanReadToken(bearerToken))
            {
                _logger.LogError("Cannot read token");
                return Task.FromResult(AuthenticateResult.Fail(Resources.AuthenticationAccessDenied));
            }

            var token = tokenHandler.ReadJwtToken(bearerToken);
            if (token == null)
            {
                _logger.LogError("Cannot read token");
                return Task.FromResult(AuthenticateResult.Fail(Resources.AuthenticationAccessDenied));
            }

            var utf8Encoder = new UTF8Encoding(true, true);
            var key = Encoding.ASCII.GetBytes(_authConfig.SigningKeyValue);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _authConfig.Issuer,
                ValidateIssuer = true,
                ValidateAudience = false,
                IssuerSigningKey = signingCredentials.Key
            };

            var principal = tokenHandler.ValidateToken(bearerToken, validationParameters, out var validatedToken);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, _authConfig.Scheme)));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Authentication Error");
            return Task.FromResult(AuthenticateResult.Fail(Resources.AuthenticationAccessDenied));
        }
    }
}