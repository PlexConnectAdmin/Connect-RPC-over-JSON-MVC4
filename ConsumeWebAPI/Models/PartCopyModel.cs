using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Models
{
  /// <summary>
  /// This is for illustrative purposes. Obtain the latest models from Plex
  /// </summary>
  [JsonObject]
  public class PartCopyModel
  {
    public string NewPartNo { get; set; }
    public string NewRevision { get; set; }
  }
}
