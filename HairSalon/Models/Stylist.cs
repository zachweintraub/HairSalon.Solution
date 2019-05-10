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

    public int GetId()
    {
      return _id;
    }

    //save an instance of stylist to the DB
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (first, last) VALUES (@StylistFirst, @StylistLast);";
      MySqlParameter first = new MySqlParameter();
      first.ParameterName = "@StylistFirst";
      first.Value = _first;
      cmd.Parameters.Add(first);
      MySqlParameter last = new MySqlParameter();
      last.ParameterName = "@StylistLast";
      last.Value = _last;
      cmd.Parameters.Add(last);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //returns all instances of stylist from the DB
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string first = rdr.GetString(1);
        string last = rdr.GetString(2);

        Stylist newStylist = new Stylist(first, last, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
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

    //equality override to compare stylist objects
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (idEquality && nameEquality);
      }
    }
  }
}
