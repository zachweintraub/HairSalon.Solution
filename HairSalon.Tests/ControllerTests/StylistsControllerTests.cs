using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistsControllerTests
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      StylistsController controller = new StylistsController();
      ActionResult indexView = controller.Index();
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

    [TestMethod]
    public void Show_ReturnsCorrectView_True()
    {
      StylistsController controller = new StylistsController();
      ActionResult indexView = controller.Show(1);
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

    [TestMethod]
    public void New_ReturnsCorrectView_True()
    {
      StylistsController controller = new StylistsController();
      ActionResult indexView = controller.New();
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }
  }
}
