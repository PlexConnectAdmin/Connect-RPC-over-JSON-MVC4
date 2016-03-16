using Newtonsoft.Json;

namespace Plex.Restful.Api.Testing.Models
{
  public class Token
  {
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

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
