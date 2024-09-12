namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Defines the interface for a service that provides authentication for Administration.
/// This service uses JWT token authentication.
/// </summary>
public interface IAdministrationAuthenticationService : IJwtTokenAuthenticationService
{
}