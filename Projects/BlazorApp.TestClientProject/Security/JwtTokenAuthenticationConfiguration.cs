using System.ComponentModel.DataAnnotations;

namespace BlazorApp.TestClientProject.Client.Security
{
    /// <summary>
    /// Represents the configuration for JWT token authentication.
    /// </summary>
    public abstract class JwtTokenAuthenticationConfiguration
    {
        /// <summary>
        /// Gets or sets the signing key value used for authentication.
        /// </summary>
        [Required]
        public virtual string SigningKeyValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issuer used for authentication.
        /// </summary>
        [Required]
        public virtual string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scheme used for authentication.
        /// </summary>
        [Required]
        public virtual string Scheme { get; set; } = string.Empty;
    }
}