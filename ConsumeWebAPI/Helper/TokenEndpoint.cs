using System;
using System.Collections.Generic;

namespace ConsumeWebAPI.Helper
{
  public class TokenEndpoint
  {
    public string clientId { get; set; }
    public List<ClientSecret> clientSecrets { get; set; }
    public Uri plexResource { get; set; }
    public Uri tokenEndPoint { get; set; }
  }
}