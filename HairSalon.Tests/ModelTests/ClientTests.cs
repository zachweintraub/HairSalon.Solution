
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
      //Appointment.ClearAll();
    }

    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=zach_weintraub_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      string first = "Juana";
      string last = "Trimm";
      int stylistId = 1;
      Client newClient = new Client(first, last, stylistId);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsFullName_ClientName()
    {
      Client newClient = new Client("Juana", "Trimm", 1);
      string output = "Juana Trimm";
      Assert.AreEqual(output, newClient.GetName());
    }
//
//     [TestMethod]
//     public void GetId_ReturnsId_Id()
//     {
//       Stylist newStylist = new Stylist("Cutter", "O'Hare", 5);
//       int output = 5;
//       Assert.AreEqual(output, newStylist.GetId());
//     }
//
//     [TestMethod]
//     public void GetAll_ReturnsAllStylistObjects_StylistList()
//     {
//       Stylist stylist1 = new Stylist("Cutter", "O'Hare");
//       stylist1.Save();
//       Stylist stylist2 = new Stylist("Snippy", "Bangz");
//       stylist2.Save();
//       List<Stylist> newList = new List<Stylist> { stylist1, stylist2 };
//       List<Stylist> result = Stylist.GetAll();
//       CollectionAssert.AreEqual(newList, result);
//     }
//
//
  }
}
