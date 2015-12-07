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

    public bool? Copy { get; set; }
    public bool? CopyAttachments { get; set; }
    public bool? CopyBOM { get; set; }
    public bool? CopyCommissionSetup { get; set; }
    public bool? CopyCustomerPartNumbers { get; set; }
    public bool? CopyHeatTreatParameters { get; set; }
    public bool? CopyNew { get; set; }
    public bool? CopyPartDetails { get; set; }
    public bool? CopyPartMaterialsMultiples { get; set; }
    public bool? CopyPPAP { get; set; }
    public bool? CopyProcessRouting { get; set; }
    public bool? CopyQualityDocuments { get; set; }
    public bool? CopySpecifications { get; set; }
    public bool? CopyTestSetup { get; set; }
    public bool? CopyToExisting { get; set; }
    public bool? CopyToolingPlan { get; set; }
    public bool? SkipApprovedWorkCentersSuppliers { get; set; }
    public bool? SkipBulletins { get; set; }
    public bool? SkipCosts { get; set; }
    public bool? SkipOperationDescription { get; set; }
    public bool? SkipPieceWeight { get; set; }
    public bool? SkipSchedulingParams { get; set; }
  }
}
