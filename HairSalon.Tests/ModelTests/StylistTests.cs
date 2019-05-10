
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

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      Stylist stylist1 = new Stylist("Cutter", "O'Hare");
      stylist1.Save();
      Stylist stylist2 = new Stylist("Snippy", "Bangz");
      stylist2.Save();
      List<Stylist> newList = new List<Stylist> { stylist1, stylist2 };
      List<Stylist> result = Stylist.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectStylist_CorrectStylist()
    {
      Stylist stylist1 = new Stylist("Cutter", "O'Hare");
      stylist1.Save();
      int idToFind = stylist1.GetId();
      Stylist foundStylist = Stylist.Find(idToFind);
      Assert.AreEqual(stylist1, foundStylist);
    }

    [TestMethod]
    public void GetClients_ReturnsClientList_ClientList()
    {
      Stylist stylist1 = new Stylist("Cutter", "O'Hare");
      stylist1.Save();
      int stylistId = stylist1.GetId();
      Client client1 = new Client("Juana", "Trimm", 10);
      Client client2 = new Client("Shaggy", "Topp", stylistId);
      client1.Save();
      client2.Save();
      List<Client> expectedClients = new List<Client>{client2};
      List<Client> foundClients = stylist1.GetClients();
      CollectionAssert.AreEqual(expectedClients, foundClients);
    }
  }
}
