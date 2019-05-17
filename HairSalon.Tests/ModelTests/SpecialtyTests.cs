using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
      Specialty.ClearAll();
      //Appointment.ClearAll();
    }

    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=zach_weintraub_test;";
    }

    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
        Specialty specialty = new Specialty("Bangs");
        Assert.AreEqual(typeof(Specialty), specialty.GetType());
    }

    [TestMethod]
    public void GetId_ReturnsSpecialtyId_0()
    {
        Specialty specialty = new Specialty("Bangs");
        int expected = 0;
        int result = specialty.GetId();
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetName_ReturnsSpecialtyName_Name()
    {
        Specialty specialty = new Specialty("Bangs");
        string expected = "Bangs";
        string result = specialty.GetName();
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectSpecialty_Specialty()
    {
        Specialty specialty = new Specialty("Bangs");
        specialty.Save();
        Specialty expected = specialty;
        Specialty result = Specialty.Find(specialty.GetId());
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllSpecialties_List()
    {
        Specialty specialty = new Specialty("Bangs");
        Specialty specialty2 = new Specialty("Curly Hair");
        specialty.Save();
        specialty2.Save();
        List<Specialty> expected = new List<Specialty>{specialty, specialty2};
        List<Specialty> result = Specialty.GetAll();
        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Delete_DeletesInstanceofSpecialty_EmptyList()
    {
        Specialty specialty = new Specialty("Bangs");
        specialty.Save();
        specialty.Delete();
        List<Specialty> expected = new List<Specialty>{};
        List<Specialty> result = Specialty.GetAll();
        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Edit_EditSpecialtyName_NewName()
    {
        Specialty specialty = new Specialty("Bangs");
        specialty.Save();
        specialty.Edit("Curly Hair");
        string expected = "Curly Hair";
        string result = specialty.GetName();
        Assert.AreEqual(expected, result);
    }

  }

}