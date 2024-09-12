using System.ComponentModel.DataAnnotations;

namespace BlazorApp.TestClientProject.Client;

/// <summary>
/// Represents the configuration settings for the client.
/// </summary>
public class ClientConfiguration
{
    /// <summary>
    /// Gets or sets the access token used for authentication.
    /// </summary>
    /// <remarks>
    /// This property is required.
    /// </remarks>
    [Required]
    public required string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the base URI for the endpoint.
    /// </summary>
    /// <remarks>
    /// This property is required and must be a valid URL.
    /// </remarks>
    [Url]
    [Required]
    public required string EndpointBaseUri { get; set; }

    /// <summary>
    /// Gets or sets the number of retry attempts to be made on a failed request.
    /// </summary>
    /// <remarks>
    /// The default value is 3 retry attempts. A value of zero disables retry attempts.
    /// This property must be a value between 0 and 10.
    /// </remarks>
    [Range(0, 10)]
    public int Retries { get; set; } = 3;

    /// <summary>
    /// Gets or sets the number of seconds to wait before the request times out.
    /// </summary>
    /// <remarks>
    /// The default value is 100 seconds.
    /// This property must be a value between 1 and 600.
    /// </remarks>
    [Range(1, 600)]
    public int Timeout { get; set; } = 100;
}