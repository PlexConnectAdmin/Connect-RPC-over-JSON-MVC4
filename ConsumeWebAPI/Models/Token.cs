using Newtonsoft.Json;

namespace Plex.Restful.Api.Testing.Models
{
  /// <summary>
  /// Token response instance
  /// See "Access Token Response" at https://msdn.microsoft.com/en-us/library/azure/dn645542.aspx
  /// </summary>
  public class Token
  {
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    /// Gets or sets the expires on.
    /// How long the access token is valid (in seconds).
    /// </summary>
    /// <value>
    /// The expires on.
    /// </value>
    [JsonProperty("expires_on")]
    public string ExpiresOn { get; set; }

    [JsonProperty("not_before")]
    public string NotBefore { get; set; }

    [JsonProperty("resource")]
    public string Resource { get; set; }

    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
  }
}
