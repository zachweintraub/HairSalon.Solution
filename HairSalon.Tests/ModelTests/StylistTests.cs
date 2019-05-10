
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
      //Appointment.ClearAll();
    }

    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=zach_weintraub_test;";
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      string first = "Cutter";
      string last = "O'Hare";
      Stylist newStylist = new Stylist(first, last);
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsFullName_StylistName()
    {
      Stylist newStylist = new Stylist("Cutter", "O'Hare");
      string output = "Cutter O'Hare";
      Assert.AreEqual(output, newStylist.GetName());
    }

    [TestMethod]
    public void GetId_ReturnsId_Id()
    {
      Stylist newStylist = new Stylist("Cutter", "O'Hare", 5);
      int output = 5;
      Assert.AreEqual(output, newStylist.GetId());
    }

    // [TestMethod]
    // public void GetAll_ReturnsAllStylistObjects_StylistList()
    // {
    //   Category newCategory1 = new Category(name01);
    //   newCategory1.Save();
    //   Category newCategory2 = new Category(name02);
    //   newCategory2.Save();
    //   List<Category> newList = new List<Category> { newCategory1, newCategory2 };
    //   List<Category> result = Category.GetAll();
    //   CollectionAssert.AreEqual(newList, result);
    // }


  }
}
