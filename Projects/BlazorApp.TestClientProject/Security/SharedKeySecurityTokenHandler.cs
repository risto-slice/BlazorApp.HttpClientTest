using System.Security.Cryptography;
using System.Text;

namespace BlazorApp.TestClientProject.Client.Security;

/// <summary>
/// Provides methods for generating and validating signatures.
/// </summary>
public class SharedKeySecurityTokenHandler
{
    private readonly string _sharedSecret;

    /// <summary>
    /// Initializes a new instance of the <see cref="SharedKeySecurityTokenHandler"/> type.
    /// </summary>
    /// <param name="sharedSecret">The shared secret used for generating the signature.</param>
    public SharedKeySecurityTokenHandler(string sharedSecret)
    {
        _sharedSecret = sharedSecret;
    }

    /// <summary>
    /// Generates a signature for the specified body.
    /// </summary>
    /// <param name="body">The body to generate the signature for.</param>
    /// <returns>The generated signature.</returns>
    public string Generate(string body)
    {
        var urlAsBytes = Encoding.UTF8.GetBytes(body);

        using var mac = new HMACSHA256(Encoding.UTF8.GetBytes(_sharedSecret));
        var hashAsByte = mac.ComputeHash(urlAsBytes);

        return ToHexString(hashAsByte);
    }

    /// <summary>
    /// Generates a signature for the specified body and URL.
    /// </summary>
    /// <param name="body">The body to generate the signature for.</param>
    /// <param name="url">The URL to generate the signature for.</param>
    /// <returns>The generated signature.</returns>
    public string Generate(string body, string url)
    {
        var bodyAsBytes = Encoding.UTF8.GetBytes($"{url}{body}");

        using var mac = new HMACSHA256(Encoding.UTF8.GetBytes(_sharedSecret));
        var hashAsByte = mac.ComputeHash(bodyAsBytes);

        return ToHexString(hashAsByte);
    }

    /// <summary>
    /// Validates the specified hash against the generated hash for the specified body.
    /// </summary>
    /// <param name="hash">The hash to validate.</param>
    /// <param name="body">The body used to generate the hash.</param>
    /// <returns>A value indicating whether the specified hash is valid.</returns>
    public bool IsValid(string hash, string body)
    {
        var bodyAsBytes = Encoding.UTF8.GetBytes(body);

        using var mac = new HMACSHA256(Encoding.UTF8.GetBytes(_sharedSecret));
        var hashAsByte = mac.ComputeHash(bodyAsBytes);

        return ToHexString(hashAsByte).Equals(hash);
    }

    /// <summary>
    /// Validates the specified hash against the generated hash for the specified body and URL.
    /// </summary>
    /// <param name="hash">The hash to validate.</param>
    /// <param name="body">The body used to generate the hash.</param>
    /// <param name="url">The URL used to generate the hash.</param>
    /// <returns>A value indicating whether the specified hash is valid.</returns>
    public bool IsValid(string hash, string body, string url)
    {
        var bodyAsBytes = Encoding.UTF8.GetBytes($"{url}{body}");

        using var mac = new HMACSHA256(Encoding.UTF8.GetBytes(_sharedSecret));
        var hashAsByte = mac.ComputeHash(bodyAsBytes);

        return ToHexString(hashAsByte).Equals(hash);
    }

    /// <summary>
    /// Converts the specified byte array to a hexadecimal string.
    /// </summary>
    /// <param name="array">The byte array to convert.</param>
    /// <returns>The hexadecimal string.</returns>
    private static string ToHexString(byte[] array)
    {
        var hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
        {
            hex.AppendFormat("{0:x2}", b);
        }

        return hex.ToString();
    }
}