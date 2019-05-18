using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists/create")]
    public ActionResult Create(string first, string last)
    {
      Stylist myStylist = new Stylist(first, last);
      myStylist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Stylist myStylist = Stylist.Find(id);
      return View(myStylist);
    }

    [HttpPost("/stylists/{id}/delete")]
    public ActionResult Destroy(int id)
    {
      Stylist deleteStylist = Stylist.Find(id);
      deleteStylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{id}/edit")]
    public ActionResult Update(int id, string newFirst, string newLast)
    {
      Stylist editStylist = Stylist.Find(id);
      editStylist.Edit(id, newFirst, newLast);
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{id}/updatespecialties")]
    public ActionResult Add(int id, int selectedSpecialty)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.AddSpecialty(selectedSpecialty);
      return View("Show", thisStylist);
    }

    [HttpPost("/stylists/{stylistId}/removespecialty/{specialtyId}")]
    public ActionResult Update(int stylistId, int specialtyId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      thisStylist.RemoveSpecialty(specialtyId);
      return View("Show", thisStylist);
    }
  }
}
