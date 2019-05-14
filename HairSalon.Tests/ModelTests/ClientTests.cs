
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

    [TestMethod]
    public void GetId_ReturnsId_Id()
    {
      Client newClient = new Client("Juana", "Trimm", 1, 5);
      int output = 5;
      Assert.AreEqual(output, newClient.GetId());
    }
//
    [TestMethod]
    public void GetAll_ReturnsAllClientObjects_ClientList()
    {
      Client client1 = new Client("Juana", "Trimm", 1);
      Client client2 = new Client("Shaggy", "Topp", 2);
      client1.Save();
      client2.Save();
      List<Client> newList = new List<Client> {client1, client2};
      List<Client> result = Client.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClient_Client()
    {
      Client newClient = new Client("Juana", "Trimm", 1);
      newClient.Save();
      int searchId = newClient.GetId();
      Client foundClient = Client.Find(searchId);
      Assert.AreEqual(newClient, foundClient);
    }

    [TestMethod]
    public void Delete_DeletesCorrectInstance_GetAllEmpty()
    {
      Client newClient = new Client("Juana", "Trimm", 1);
      newClient.Save();
      newClient.Delete();
      int expected = 0;
      int actual = Client.GetAll().Count;
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Edit_UpdatesClientData_UpdatedClient()
    {
      Client newClient = new Client("Juana", "Trimm", 1);
      newClient.Save();
      newClient.Edit("John", "Smith");
      string expected = "John Smith";
      Assert.AreEqual(expected, newClient.GetName());
    }
  }
}
