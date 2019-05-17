using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpPost("/specialties/{id}/edit")]
    public ActionResult Edit(int id, string newName)
    {
      Specialty editSpecialty = Specialty.Find(id);
      editSpecialty.Edit(newName);
      return RedirectToAction("Index");
    }

    [HttpPost("/specialties/{id}/delete")]
    public ActionResult Destroy(int id)
    {
      Specialty deleteSpecialty = Specialty.Find(id);
      deleteSpecialty.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Show(int id)
    {
      Specialty specialty = Specialty.Find(id);
      return View(specialty);
    }

    [HttpPost("/specialties/create")]
    public ActionResult Create(string newName)
    {
      Specialty newSpecialty = new Specialty(newName);
      newSpecialty.Save();
      return RedirectToAction("Index");
    }
  }
}