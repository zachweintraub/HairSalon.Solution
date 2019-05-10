using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private int _stylistId;
    private string _first;
    private string _last;

    public Client(string first, string last, int stylistId, int id = 0)
    {
      _first = first;
      _last = last;
      _stylistId = stylistId;
      _id = id;
    }

    //returns the client's full name
    public string GetName()
    {
      return _first + " " + _last;
    }


    //deletes all instances of client from the DB
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }
  }
}
