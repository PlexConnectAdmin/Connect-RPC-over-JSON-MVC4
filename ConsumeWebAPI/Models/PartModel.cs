using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Models
{
  /// <summary>
  /// This is for illustrative purposes. Obtain the latest models from Plex
  /// </summary>
  [JsonObject]
  public class PartModel
  {
    [Display(Name = "Account_Key")]
    public int? AccountKey { get; set; }
    [Display(Name = "Account_No")]
    public string AccountNo { get; set; }
    [Display(Name = "Add_By")]
    public string AddBy { get; set; }
    // [CustomerSetting("Part", "Additional Documents Display")]
    [Display(Name = "Additional_Documents")]
    public string AdditionalDocuments { get; set; }
    // [CustomerSetting("Part", "Part Attributes Use")]
    [Display(Name = "Allow_Weighing")]
    public bool? AllowWeighing { get; set; }
    [Display(Name = "APQP_Checklist_No")]
    public int? APQPChecklistNo { get; set; }
    [Display(Name = "Area")]
    public decimal? Area { get; set; }
    [Display(Name = "Average_Value")]
    public decimal? AverageValue { get; set; }
    [Display(Name = "Barcode_Extension")]
    public string BarcodeExtension { get; set; }
    [Display(Name = "Batch_Scrap_Percent")]
    public decimal? BatchScrapPercent { get; set; }
    [Display(Name = "Batch_Scrap_Pieces")]
    public int? BatchScrapPieces { get; set; }
    [Display(Name = "Blank_Weight")]
    public decimal? BlankWeight { get; set; }
    // [CustomerSetting("Part", "BOM Allocation Variance Display")]
    [Display(Name = "BOM_Allocation_Variance")]
    public decimal? BOMAllocationVariance { get; set; }
    [Display(Name = "Building_Code")]
    public string BuildingCode { get; set; }
    [Display(Name = "Building_Key")]
    public int? BuildingKey { get; set; }
    [Display(Name = "Coil_Over_Consume_Pct")]
    public decimal? CoilOverConsumePct { get; set; }
    [Display(Name = "Coil_Under_Consume_Pct")]
    public decimal? CoilUnderConsumePct { get; set; }
    [Display(Name = "Commodity_Code")]
    public string CommodityCode { get; set; }
    [Display(Name = "Commodity_Code_Key")]
    public int? CommodityCodeKey { get; set; }
    // [CustomerSetting("Part", "Additional Shipping Label Info Display")]
    [Display(Name = "Controlled_Shipping_1")]
    public bool? ControlledShipping1 { get; set; }
    // [CustomerSetting("Part", "Additional Shipping Label Info Display")]
    [Display(Name = "Controlled_Shipping_2")]
    public bool? ControlledShipping2 { get; set; }
    [Display(Name = "Control_Level")]
    public string ControlLevel { get; set; }
    [Display(Name = "Control_Level_Key")]
    public int? ControlLevelKey { get; set; }
    [Display(Name = "Country_Of_Orgin_Code")]
    public string CountryOfOrginCode { get; set; }
    [Display(Name = "Country_Of_Origin")]
    public int? CountryOfOrigin { get; set; }
    [Display(Name = "Created_By")]
    public int? CreatedBy { get; set; }
    [Display(Name = "Created_Date")]
    public DateTime? CreatedDate { get; set; }
    [Display(Name = "Currency_HTML")]
    public string CurrencyHTML { get; set; }
    [Display(Name = "Customer_Approval_End_Date")]
    public DateTime? CustomerApprovalEndDate { get; set; }
    [Display(Name = "Customer_Approval_Note")]
    public string CustomerApprovalNote { get; set; }
    [Display(Name = "Customer_Approval_Quantity")]
    public int? CustomerApprovalQuantity { get; set; }
    [Display(Name = "Customer_Approval_Start_Date")]
    public DateTime? CustomerApprovalStartDate { get; set; }
    [Display(Name = "Customer_Approval_Status_Key")]
    public int? CustomerApprovalStatusKey { get; set; }
    [Display(Name = "Customer_Shipping_Code")]
    public string CustomerShippingCode { get; set; }
    // [CustomerSetting("Part", "Customer Supplied Material Display")]
    [Display(Name = "Customer_Supplied_Material")]
    public short? CustomerSuppliedMaterial { get; set; }
    [Display(Name = "Customs_Currency_Key")]
    public int? CustomsCurrencyKey { get; set; }
    [Display(Name = "Customs_Currency_Symbol")]
    public string CustomsCurrencySymbol { get; set; }
    [Display(Name = "Customs_Value")]
    public decimal? CustomsValue { get; set; }
    [Display(Name = "Customs_Value_Currency_Code")]
    public string CustomsValueCurrencyCode { get; set; }
    [Display(Name = "Cycle_Frequency")]
    public string CycleFrequency { get; set; }
    [Display(Name = "Cycle_Frequency_Key")]
    public int? CycleFrequencyKey { get; set; }
    // [CustomerSetting("Part", "Daily Pull Display")]
    [Display(Name = "Daily_Pull")]
    public decimal? DailyPull { get; set; }
    [Display(Name = "Default_Stock_Location")]
    public string DefaultStockLocation { get; set; }
    [Display(Name = "Density")]
    public decimal? Density { get; set; }
    [Display(Name = "Department_Code")]
    public string DepartmentCode { get; set; }
    [Display(Name = "Department_No")]
    public int? DepartmentNo { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    // [CustomerSetting("Part", "Deviation Display")]
    [Display(Name = "Deviation")]
    public bool? Deviation { get; set; }
    // [CustomerSetting("Part", "Direct Sales Display")]
    [Display(Name = "Direct_Sales")]
    public bool? DirectSales { get; set; }
    [Display(Name = "Dock_Alert")]
    public bool? DockAlert { get; set; }
    // [CustomerSetting("Part", "Ecommerce Display")]
    [Display(Name = "Ecommerce")]
    public bool? Ecommerce { get; set; }
    [Display(Name = "Effective_Date_Edit")]
    public int? EffectiveDateEdit { get; set; }
    [Display(Name = "Engineer")]
    public string Engineer { get; set; }
    [Display(Name = "Engineer2")]
    public string Engineer2 { get; set; }
    [Display(Name = "Engineer2_Name")]
    public string Engineer2Name { get; set; }
    [Display(Name = "Engineer3")]
    public string Engineer3 { get; set; }
    [Display(Name = "Engineer3_Name")]
    public string Engineer3Name { get; set; }
    [Display(Name = "Engineer_Name")]
    public string EngineerName { get; set; }
    // [CustomerSetting("Part", "Excess Inventory Level Display")]
    [Display(Name = "Excess_Inventory_Level")]
    public bool? ExcessInventoryLevel { get; set; }
    [Display(Name = "Exclusive_Use_Rights_Customer_Code")]
    public string ExclusiveUseRightsCustomerCode { get; set; }
    [Display(Name = "Exclusive_Use_Rights_Customer_No")]
    public int? ExclusiveUseRightsCustomerNo { get; set; }
    // [CustomerSetting("Part", "Final Electrical Test Display")]
    [Display(Name = "Final_Electrical_Test")]
    public string FinalElectricalTest { get; set; }
    // [CustomerSetting("Part", "Finished Part Buffer Display")]
    [Display(Name = "Finished_Part_Buffer")]
    public int? FinishedPartBuffer { get; set; }
    [Display(Name = "First_Supplier")]
    public string FirstSupplier { get; set; }
    [Display(Name = "Freight_Classification")]
    public string FreightClassification { get; set; }
    [Display(Name = "Freight_Classification_Key")]
    public int? FreightClassificationKey { get; set; }
    [Display(Name = "Freight_Cost")]
    public decimal? FreightCost { get; set; }
    [Display(Name = "Freight_Cost_Currency_Code")]
    public string FreightCostCurrencyCode { get; set; }
    [Display(Name = "Freight_Currency")]
    public string FreightCurrency { get; set; }
    [Display(Name = "Freight_Currency_Key")]
    public int? FreightCurrencyKey { get; set; }
    [Display(Name = "Freight_Currency_Symbol")]
    public string FreightCurrencySymbol { get; set; }
    [Display(Name = "Freight_Weight_Class_Key")]
    public int? FreightWeightClassKey { get; set; }
    [Display(Name = "Grade")]
    public string Grade { get; set; }
    [Display(Name = "Grade_Key")]
    public int? GradeKey { get; set; }
    [Display(Name = "Harmonized_Tariff_Code")]
    public string HarmonizedTariffCode { get; set; }
    [Display(Name = "Harmonized_Tariff_Code_Key")]
    public int? HarmonizedTariffCodeKey { get; set; }
    [Display(Name = "Hazardous")]
    public bool? Hazardous { get; set; }
    [Display(Name = "Hold_On_Receipt")]
    public bool? HoldOnReceipt { get; set; }
    [Display(Name = "Image")]
    public string Image { get; set; }
    [Display(Name = "Image_Count")]
    public int? ImageCount { get; set; }
    [Display(Name = "Include_In_Standard_Cost")]
    public short? IncludeInStandardCost { get; set; }
    // [CustomerSetting("Part", "Additional Shipping Label Info Display")]
    [Display(Name = "Increased_Inspection")]
    public bool? IncreasedInspection { get; set; }
    [Display(Name = "Industry")]
    public string Industry { get; set; }
    [Display(Name = "Industry_Key")]
    public int? IndustryKey { get; set; }
    [Display(Name = "Internal_Approval_End_Date")]
    public DateTime? InternalApprovalEndDate { get; set; }
    [Display(Name = "Internal_Approval_Note")]
    public string InternalApprovalNote { get; set; }
    [Display(Name = "Internal_Approval_Quantity")]
    public int? InternalApprovalQuantity { get; set; }
    [Display(Name = "Internal_Approval_Start_Date")]
    public DateTime? InternalApprovalStartDate { get; set; }
    [Display(Name = "Internal_Approval_Status_Key")]
    public int? InternalApprovalStatusKey { get; set; }
    // [CustomerSetting("Part", "Internal Note Display")]
    [Display(Name = "Internal_Note")]
    public string InternalNote { get; set; }
    [Display(Name = "Inventory_Classification_Code")]
    public string InventoryClassificationCode { get; set; }
    [Display(Name = "Inventory_Classification_Key")]
    public int? InventoryClassificationKey { get; set; }
    [Display(Name = "Item_Tax_Code")]
    public string ItemTaxCode { get; set; }
    [Display(Name = "Item_Tax_Key")]
    public int? ItemTaxKey { get; set; }
    [Display(Name = "Iterations")]
    public string Iterations { get; set; }
    // [CustomerSetting("Part", "Job Consolidation Window Display")]
    [Display(Name = "Job_Consolidation_Window")]
    public int? JobConsolidationWindow { get; set; }
    // [CustomerSetting("Part", "Job Creation Threshold Display")]
    [Display(Name = "Job_Creation_Threshold")]
    public int? JobCreationThreshold { get; set; }
    // [CustomerSetting("Part", "Last Shipped Release Label Preprint Display")]
    [Display(Name = "Last_Shipped_Release_Label_Preprint")]
    public bool? LastShippedReleaseLabelPreprint { get; set; }
    [Display(Name = "Lead_Time")]
    public decimal? LeadTime { get; set; }
    [Display(Name = "Lead_Time_Days")]
    public decimal? LeadTimeDays { get; set; }
    // [CustomerSetting("Part", "Level Manually Display")]
    [Display(Name = "Level_Manually")]
    public bool? LevelManually { get; set; }
    // [CustomerSetting("Part", "Level Scheduled Display")]
    [Display(Name = "Level_Scheduled")]
    public bool? LevelScheduled { get; set; }
    [Display(Name = "Like_Part_Key")]
    public int? LikePartKey { get; set; }
    [Display(Name = "Like_Part_No_Revision")]
    public string LikePartNoRevision { get; set; }
    [Display(Name = "Link_Part_Keys")]
    public string LinkPartKeys { get; set; }
    [Display(Name = "Link_Part_Nos")]
    public string LinkPartNos { get; set; }
    [Display(Name = "Lot_Format_Description")]
    public string LotFormatDescription { get; set; }
    [Display(Name = "Lot_Format_Key")]
    public int? LotFormatKey { get; set; }
    [Display(Name = "Lot_Replenishable")]
    public bool? LotReplenishable { get; set; }
    [Display(Name = "Lot_Required")]
    public bool? LotRequired { get; set; }
    // [CustomerSetting("Part", "LTA Number Display")]
    [Display(Name = "LTA_Number")]
    public string LTANumber { get; set; }
    [Display(Name = "Maximum_Inventory_Quantity")]
    public decimal? MaximumInventoryQuantity { get; set; }
    // [CustomerSetting("Part", "Maximum Job Quantity Display")]
    [Display(Name = "Maximum_Job_Quantity")]
    public decimal? MaximumJobQuantity { get; set; }
    [Display(Name = "Max_Splits_Prime_Control_Panel")]
    public int? MaxSplitsPrimeControlPanel { get; set; }
    [Display(Name = "Max_Splits_Prime_Lift_Entry")]
    public int? MaxSplitsPrimeLiftEntry { get; set; }
    [Display(Name = "Max_Splits_Reject_Control_Panel")]
    public int? MaxSplitsRejectControlPanel { get; set; }
    [Display(Name = "Max_Splits_Reject_Lift_Entry")]
    public int? MaxSplitsRejectLiftEntry { get; set; }
    [Display(Name = "Minimum_Inventory_Quantity")]
    public decimal? MinimumInventoryQuantity { get; set; }
    // [CustomerSetting("Part", "Minimum Job Quantity Display")]
    [Display(Name = "Minimum_Job_Quantity")]
    public decimal? MinimumJobQuantity { get; set; }
    [Display(Name = "Minimum_Order_Quantity")]
    public decimal? MinimumOrderQuantity { get; set; }
    [Display(Name = "MRB_Breakpoint")]
    public decimal? MRBBreakpoint { get; set; }
    // [CustomerSetting("Part", "MRP Demand Level Use")]
    [Display(Name = "MRP_Demand_Level")]
    public bool? MRPDemandLevel { get; set; }
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Display(Name = "Note")]
    public string Note { get; set; }
    [Display(Name = "Offal")]
    public int? Offal { get; set; }
    [Display(Name = "Old_Part_No")]
    public string OldPartNo { get; set; }
    // [CustomerSetting("Part", "Other Note Display")]
    [Display(Name = "Other_Note")]
    public string OtherNote { get; set; }
    [Display(Name = "Packing_Group")]
    public string PackingGroup { get; set; }
    [Display(Name = "Packing_Group_Key")]
    public int? PackingGroupKey { get; set; }
    [Display(Name = "Part_Attribute_Key")]
    public int? PartAttributeKey { get; set; }
    // [CustomerSetting("Customer Part", "Internal Part Drawing Number Display")]
    [Display(Name = "Part_Drawing_Number")]
    public string PartDrawingNumber { get; set; }
    // [CustomerSetting("Customer Part", "Internal Part Drawing Revision Display")]
    [Display(Name = "Part_Drawing_Revision")]
    public string PartDrawingRevision { get; set; }
    [Display(Name = "Part_Group")]
    public string PartGroup { get; set; }
    [Display(Name = "Part_Group_Key")]
    public int? PartGroupKey { get; set; }
    [Display(Name = "Part_Key")]
    public int PartKey { get; set; }
    [Display(Name = "Part_Label_Format")]
    public string PartLabelFormat { get; set; }
    [Display(Name = "Part_Label_Format_Key")]
    public int? PartLabelFormatKey { get; set; }
    [Display(Name = "Part_No")]
    public string PartNo { get; set; }
    [Display(Name = "Part_No_Revision")]
    public string PartNoRevision { get; set; }
    [Display(Name = "Part_Priority")]
    public string PartPriority { get; set; }
    [Display(Name = "Part_Priority_Key")]
    public int? PartPriorityKey { get; set; }
    [Display(Name = "Part_Product_Group")]
    public string PartProductGroup { get; set; }
    [Display(Name = "Part_Product_Group_Key")]
    public int? PartProductGroupKey { get; set; }
    [Display(Name = "Part_Source")]
    public string PartSource { get; set; }
    [Display(Name = "Part_Source_Key")]
    public int? PartSourceKey { get; set; }
    [Display(Name = "Part_Status")]
    public string PartStatus { get; set; }
    [Display(Name = "Part_Status_Security")]
    public string PartStatusSecurity { get; set; }
    [Display(Name = "Part_Type")]
    public string PartType { get; set; }
    [Display(Name = "Part_Type_Key")]
    public int? PartTypeKey { get; set; }
    [Display(Name = "Planner")]
    public int? Planner { get; set; }
    [Display(Name = "Planner_Name")]
    public string PlannerName { get; set; }
    [Display(Name = "Planning_Group")]
    public string PlanningGroup { get; set; }
    [Display(Name = "Planning_Group_Group_Name")]
    public string PlanningGroupGroupName { get; set; }
    [Display(Name = "Planning_Group_Key")]
    public int? PlanningGroupKey { get; set; }
    // [CustomerSetting("Part", "Planning Placeholder Display")]
    [Display(Name = "Planning_Placeholder")]
    public bool? PlanningPlaceholder { get; set; }
    // [CustomerSetting("Part", "Process Days Display")]
    [Display(Name = "Process_Days")]
    public int? ProcessDays { get; set; }
    [Display(Name = "Production_Scrap_Percent")]
    public decimal? ProductionScrapPercent { get; set; }
    [Display(Name = "Product_Type")]
    public string ProductType { get; set; }
    [Display(Name = "Product_Type_Key")]
    public int? ProductTypeKey { get; set; }
    [Display(Name = "Program_Manager")]
    public int? ProgramManager { get; set; }
    [Display(Name = "Program_Manager_Name")]
    public string ProgramManagerName { get; set; }
    // [CustomerSetting("Part", "Quality Advanced Planning Version Display")]
    [Display(Name = "Quality_Advanced_Planning_Version")]
    public string QualityAdvancedPlanningVersion { get; set; }
    [Display(Name = "Quoted_Job_Quantity")]
    public int? QuotedJobQuantity { get; set; }
    // [CustomerSetting("Part", "Quoted Lead Time Display")]
    [Display(Name = "Quoted_Lead_Time")]
    public decimal? QuotedLeadTime { get; set; }
    [Display(Name = "Raw_Material_PO_No")]
    public string RawMaterialPONo { get; set; }
    // [CustomerSetting("Part", "Prevent Reapplication Display")]
    [Display(Name = "Reapplication_Prevent")]
    public bool? ReapplicationPrevent { get; set; }
    // [CustomerSetting("Part", "Release Auto Create Display")]
    [Display(Name = "Release_Auto_Create")]
    public bool? ReleaseAutoCreate { get; set; }
    // [CustomerSetting("Part", "Release Over Receipt Threshold Display")]
    [Display(Name = "Release_Over_Receipt_Threshold")]
    public decimal? ReleaseOverReceiptThreshold { get; set; }
    [Display(Name = "Requirement")]
    public int? Requirement { get; set; }
    [Display(Name = "Revision")]
    public string Revision { get; set; }
    [Display(Name = "Revision_Effective_Date")]
    public DateTime? RevisionEffectiveDate { get; set; }
    [Display(Name = "Revision_Expiration_Date")]
    public DateTime? RevisionExpirationDate { get; set; }
    [Display(Name = "RP_Display_Key")]
    public int? RPDisplayKey { get; set; }
    // [CustomerSetting("Part", "Scrap Display")]
    [Display(Name = "Scrap")]
    public decimal? Scrap { get; set; }
    // [CustomerSetting("Part", "Serialize Display")]
    [Display(Name = "Serialize")]
    public short? Serialize { get; set; }
    [Display(Name = "Side_Key")]
    public int? SideKey { get; set; }
    [Display(Name = "Side_Text")]
    public string SideText { get; set; }
    // [CustomerSetting("Part", "Source Load Split Display")]
    [Display(Name = "Source_Load_Split")]
    public bool? SourceLoadSplit { get; set; }
    [Display(Name = "Special_Symbol")]
    public string SpecialSymbol { get; set; }
    [Display(Name = "Special_Symbol_Key")]
    public int? SpecialSymbolKey { get; set; }
    [Display(Name = "Standard_Job_Quantity")]
    public decimal? StandardJobQuantity { get; set; }
    // [CustomerSetting("Part", "Standard Order Quantity Display")]
    [Display(Name = "Standard_Order_Quantity")]
    public decimal? StandardOrderQuantity { get; set; }
    // [CustomerSetting("Part", "Standard Pack Computed Display")]
    [Display(Name = "Standard_Pack_Computed")]
    public decimal? StandardPackComputed { get; set; }
    [Display(Name = "Submission_Date")]
    public DateTime? SubmissionDate { get; set; }
    [Display(Name = "Supplier_Part_Level_Key")]
    public int? SupplierPartLevelKey { get; set; }
    [Display(Name = "Suppress_Receipt_Transmission")]
    public bool? SuppressReceiptTransmission { get; set; }
    [Display(Name = "Suppress_Transmit_EDI")]
    public bool? SuppressTransmitEDI { get; set; }
    // [CustomerSetting("Part", "Surcharge Display")]
    [Display(Name = "Surcharge")]
    public bool? Surcharge { get; set; }
    [Display(Name = "Team_Code")]
    public string TeamCode { get; set; }
    [Display(Name = "Team_No")]
    public int? TeamNo { get; set; }
    [Display(Name = "Temper_Key")]
    public int? TemperKey { get; set; }
    [Display(Name = "Temper_Text")]
    public string TemperText { get; set; }
    [Display(Name = "Theoretical_Weight")]
    public bool? TheoreticalWeight { get; set; }
    // [CustomerSetting("Part", "Tracking No Required Display")]
    [Display(Name = "Tracking_No_Required")]
    public bool? TrackingNoRequired { get; set; }
    [Display(Name = "Transfer_Company")]
    public string TransferCompany { get; set; }
    [Display(Name = "Transfer_Company_Key")]
    public int? TransferCompanyKey { get; set; }
    // [CustomerSetting("Part", "Transfer Die No Display")]
    [Display(Name = "Transfer_Die_No")]
    public string TransferDieNo { get; set; }
    [Display(Name = "Unit")]
    public string Unit { get; set; }
    [Display(Name = "UPC_Code")]
    public string UPCCode { get; set; }
    [Display(Name = "Update_By")]
    public string UpdateBy { get; set; }
    [Display(Name = "Updated_By")]
    public int? UpdatedBy { get; set; }
    [Display(Name = "Updated_Date")]
    public DateTime? UpdatedDate { get; set; }
    [Display(Name = "Use_DCP")]
    public short? UseDCP { get; set; }
    [Display(Name = "Weight")]
    public decimal? Weight { get; set; }
    [Display(Name = "Workcenter_Code")]
    public string WorkcenterCode { get; set; }
    [Display(Name = "Workcenter_Key")]
    public int? WorkcenterKey { get; set; }
    [Display(Name = "Workflow_Doc_Key")]
    public int? WorkflowDocKey { get; set; }
  }
}
