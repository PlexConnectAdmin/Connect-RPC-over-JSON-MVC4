using System;
using System.Web.Mvc;
using ConsumeWebAPI.Helper;
using ConsumeWebAPI.Models;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
  [HandleError]
  public class PartController : Controller
  {
    static readonly IPartRestClient RestClient = new PartRestClient();
    //
    // GET: /part/

    public ActionResult Index()
    {
      return View(RestClient.GetSeveralParts());
    }

    //
    // GET: /part/Details/5

    public ActionResult Details(int id)
    {
      return View(RestClient.GetById(id));
    }

    //
    // GET: /part/Create

    public ActionResult Create()
    {
      return View();
    }

    //
    // GET: /part/Error

    public ActionResult Error()
    {
      return View();
    }

    //
    // POST: /part/Create

    [HttpPost]
    public ActionResult Create(PartAddModel part)
    {
      try
      {
        RestClient.Add(part);

        // todo: retrieve new key and return to its detail form
        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    //
    // GET: /part/Edit/5

    public ActionResult Copy(int id)
    {
      return View(RestClient.GetById(id));
    }

    //
    // POST: /part/Edit/5

    [HttpPost]
    public ActionResult Copy(PartModel part)
    {
      PartCopyModel partCopyModel = new PartCopyModel();
      partCopyModel.NewPartNo = part.PartNo;
      partCopyModel.NewRevision = part.Revision;

      // some of the properties that could be exposed.
      partCopyModel.Copy = false;
      partCopyModel.CopyAttachments = false;
      partCopyModel.CopyBOM = false;
      partCopyModel.CopyCommissionSetup = false;
      partCopyModel.CopyCustomerPartNumbers = false;
      partCopyModel.CopyHeatTreatParameters = false;
      partCopyModel.CopyNew = false;
      partCopyModel.CopyPartDetails = false;
      partCopyModel.CopyPartMaterialsMultiples = false;
      partCopyModel.CopyPPAP = false;
      partCopyModel.CopyProcessRouting = false;
      partCopyModel.CopyQualityDocuments = false;
      partCopyModel.CopySpecifications = false;
      partCopyModel.CopyTestSetup = false;
      partCopyModel.CopyToExisting = false;
      partCopyModel.CopyToolingPlan = false;
      partCopyModel.SkipApprovedWorkCentersSuppliers = true;
      partCopyModel.SkipBulletins = true;
      partCopyModel.SkipCosts = true;
      partCopyModel.SkipOperationDescription = true;
      partCopyModel.SkipPieceWeight = true;
      partCopyModel.SkipSchedulingParams = true;

      RestClient.Copy(partCopyModel, part.PartKey);

      return RedirectToAction("Index");
    }

    //
    // GET: /part/Delete/5

    public ActionResult Delete(int id)
    {
      return View(RestClient.GetById(id));
    }

    //
    // POST: /part/Delete/5

    [HttpPost]
    public ActionResult Delete(PartModel part)
    {
      RestClient.Delete(part.PartKey);
      return RedirectToAction("Index");
    }

    protected override void OnException(ExceptionContext filterContext)
    {
      this.Session["ExceptionMessage"] = filterContext.Exception; // Can use this exception message later on.


      filterContext.ExceptionHandled = true;

      // todo: put some error logging in
    }
  }
}