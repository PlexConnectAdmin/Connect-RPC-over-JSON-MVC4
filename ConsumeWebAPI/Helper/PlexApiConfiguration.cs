using System;
using System.Collections.Generic;

namespace ConsumeWebAPI.Helper
{
  public class PlexApiConfiguration
  {
    public string clientId { get; set;  }
    public string subscriptionKey { get; set; }
    public List<ClientSecret> clientSecrets { get; set; }
    public Uri plexResource { get; set; }
    public Uri tokenEndPoint { get; set; }
    public Uri apiEndPointDomain { get; set; }
  }
}