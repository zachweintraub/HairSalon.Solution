
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

   [TestMethod]
    public void Delete_DeletesCorrectInstance_GetAllEmpty()
    {
      Stylist newStylist = new Stylist("Cutter", "O'Hare");
      newStylist.Save();
      newStylist.Delete();
      int expected = 0;
      int actual = Client.GetAll().Count;
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Edit_UpdatesStylistData_UpdatedStylist()
    {
      Stylist newStylist = new Stylist("Cutter", "O'Hare");
      newStylist.Save();
      newStylist.Edit(1, "John", "Smith");
      string expected = "John Smith";
      Assert.AreEqual(expected, newStylist.GetName());
    }

    [TestMethod]
    public void GetSpecialties_ReturnListOfSpecialties_SpecialtyList()
    {
      Stylist newStylist = new Stylist("Cutter", "O'Hare");
      newStylist.Save();
      Specialty newSpecialty = new Specialty("Blowouts");
      newSpecialty.Save();
      newStylist.AddSpecialty(newSpecialty.GetId());
      List<Specialty> expected = new List<Specialty>{newSpecialty};
      List<Specialty> result = newStylist.GetSpecialties();
      List<Stylist> expected2 = new List<Stylist>{newStylist};
      List<Stylist> result2 = newSpecialty.GetStylists();
      CollectionAssert.AreEqual(expected, result);
      CollectionAssert.AreEqual(expected2, result2);
    }

    [TestMethod]
    public void RemoveSpecialty_RemovesDesignatedSpecialty_EmptySpecialtyList()
    {
      Stylist newStylist = new Stylist("Cutter", "O'Hare");
      newStylist.Save();
      Specialty newSpecialty = new Specialty("Blowouts");
      newSpecialty.Save();
      newStylist.AddSpecialty(newSpecialty.GetId());
      newStylist.RemoveSpecialty(newSpecialty.GetId());
      List<Specialty> expected = new List<Specialty>{};
      List<Specialty> result = newStylist.GetSpecialties();
      List<Stylist> expected2 = new List<Stylist>{};
      List<Stylist> result2 = newSpecialty.GetStylists();
      CollectionAssert.AreEqual(expected, result);
      CollectionAssert.AreEqual(expected2, result2);
    }
  }
}
