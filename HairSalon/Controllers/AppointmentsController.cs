
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class AppointmentsController : Controller
  {
    [HttpGet("/appointments/new")]
    public ActionResult New()
    {
      return View();
    }
  }
}
