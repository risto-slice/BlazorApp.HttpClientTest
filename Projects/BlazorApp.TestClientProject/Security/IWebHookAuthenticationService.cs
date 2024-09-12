namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Defines the interface for a service that provides authentication for WebHooks.
/// This service uses shared key authentication.
/// </summary>
public interface IWebHookAuthenticationService : ISharedKeyAuthenticationService
{
}