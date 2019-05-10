
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

  }
}
