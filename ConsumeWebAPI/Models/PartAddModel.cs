using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Models
{
  /// <summary>
  /// This is for illustrative purposes. Obtain the latest models from Plex
  /// </summary>
  [JsonObject]
  public class PartAddModel
  {
    [Display(Name = "Part_No")]
    public string PartNo { get; set; }

    [Display(Name = "Revision")]
    public string Revision { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; }

  }
}
