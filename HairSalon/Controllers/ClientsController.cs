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
    public ActionResult Create(string first, string last, int stylistId)
    {
      Client myClient = new Client(first, last, stylistId);
      myClient.Save();
      return View("Show", myClient);
    }

    [HttpPost("/clients/{id}/delete")]
    public ActionResult Destroy(int id)
    {
      Client deleteClient = Client.Find(id);
      deleteClient.Delete();
      return RedirectToAction("Index");
    }

    [HttpPost("/clients/{id}/edit")]
    public ActionResult Update(int id, string newFirst, string newLast)
    {
      Client editClient = Client.Find(id);
      editClient.Edit(id, newFirst, newLast);
      return RedirectToAction("Index");
    }
  }
}
