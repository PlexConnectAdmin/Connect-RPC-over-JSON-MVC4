using System;
using System.Web.Mvc;
using ConsumeWebAPI.Helper;
using ConsumeWebAPI.Models;

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
    public ActionResult Create(PartModel part)
    {
      try
      {
        RestClient.Add(part);
        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    //
    // GET: /part/Edit/5

    public ActionResult Edit(int id)
    {
      return View(RestClient.GetById(id));
    }

    //
    // POST: /part/Edit/5

    [HttpPost]
    public ActionResult Edit(PartModel part)
    {
      try
      {
        RestClient.Update(part);

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
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