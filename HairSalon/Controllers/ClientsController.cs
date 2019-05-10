using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int clientId)
    {
      Client myClient = Client.Find(clientId);
      return View(myClient);
    }

    [HttpGet("/stylists/{id}/clients/new")]
    public ActionResult New(int id)
    {
      Stylist myStylist = Stylist.Find(id);
      return View(myStylist);
    }

    [HttpPost("/stylists/{stylistId}/clients/create")]
    public ActionResult Create(string first, string last)
    {
      Stylist myStylist = new Stylist(first, last);
      myStylist.Save();
      return RedirectToAction("Show");
    }


  }
}
