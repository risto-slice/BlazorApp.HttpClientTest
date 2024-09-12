using System.ComponentModel.DataAnnotations;

namespace BlazorApp.TestClientProject.Client.Security
{
    /// <summary>
    /// Represents the configuration for authentication.
    /// </summary>
    public abstract class SharedKeyAuthenticationConfiguration
    {
        /// <summary>
        /// Gets or sets the scheme used for authentication.
        /// </summary>
        [Required]
        public virtual string Scheme { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the web hook signature used for authentication.
        /// </summary>
        [Required]
        public virtual string WebHookSignatureName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the web hook keys used for authentication.
        /// </summary>
        [Required]
        public virtual string[] WebHookKeys { get; set; } = [];
    }
}