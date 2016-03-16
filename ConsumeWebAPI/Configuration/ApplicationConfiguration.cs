using System.IO;
using System.Linq;
using System.Web;
using ConsumeWebAPI.Helper;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Configuration
{
  /// <summary>
  /// Get settings from JSON config file
  /// </summary>
  internal static class ApplicationConfiguration
  {
    readonly static string exeRuntimeDirectory = System.IO.Path.GetDirectoryName(HttpRuntime.AppDomainAppPath);
    private static readonly PlexApiConfiguration plexApiConfiguration = JsonConvert.DeserializeObject<PlexApiConfiguration>(File.ReadAllText(exeRuntimeDirectory + @"\App_Data\PlexConnectConfig.json"));

    internal static PlexApiConfiguration PlexApiConfiguration
    {
      get { return plexApiConfiguration; }
    }

    // use the secret with the furthest out expiry. Use the JSON configuration file to manage your rolling secrets 
    internal static System.Collections.Generic.List<ClientSecret> SortedClientSecretList()
    {
      return PlexApiConfiguration.clientSecrets.OrderByDescending(o => o.expiry).ToList();
    }

    internal static string ClientSecret()
    {
      return ApplicationConfiguration.SortedClientSecretList()[0].clientSecret;
    }
  }
}