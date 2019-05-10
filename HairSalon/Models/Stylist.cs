using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _first;
    private string _last;

    //stylist constructor
    public Stylist(string first, string last, int id = 0)
    {
      _first = first;
      _last = last;
      _id = id;
    }

    //returns the stylist's full name
    public string GetName()
    {
      return _first + " " + _last;
    }


    //deletes all instances of stylist from the DB
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }
  }
}
