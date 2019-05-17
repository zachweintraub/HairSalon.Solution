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

    public string GetFirstName()
    {
      return _first;
    }

    public string GetLastName()
    {
      return _last;
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

    public List<Client> GetClients()
    {
      List<Client> foundClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int stylistId = rdr.GetInt32(1);
        string first = rdr.GetString(2);
        string last = rdr.GetString(3);

        Client newClient = new Client(first, last, stylistId, id);
        foundClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundClients;
    }

    //returns stylist by id
    public static Stylist Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string first = "";
      string last = "";
      while (rdr.Read())
      {
        id = rdr.GetInt32(0);
        first = rdr.GetString(1);
        last = rdr.GetString(2);
      }
      Stylist foundStylist = new Stylist(first, last, id);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }

    //edit details for a stylist
    public void Edit(int id, string newFirst, string newLast)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET first = @newFirst, last = @newLast WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      MySqlParameter newFirstName = new MySqlParameter();
      newFirstName.ParameterName = "@newFirst";
      newFirstName.Value = newFirst;
      cmd.Parameters.Add(newFirstName);
      MySqlParameter newLastName = new MySqlParameter();
      newLastName.ParameterName = "@newLast";
      newLastName.Value = newLast;
      cmd.Parameters.Add(newLastName);
      cmd.ExecuteNonQuery();
      _first = newFirst;
      _last = newLast;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //deletes a single instance of a stylist, as well as all associated clients
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      foreach(Client client in GetClients())
      {
        client.Delete();
      }
    }

    //adds a new specialty to the stylist
    public void AddSpecialty(int specialtyId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@thisStylistId, @thisSpecialtyId);";
      MySqlParameter thisSpecialtyId = new MySqlParameter("@thisSpecialtyId", specialtyId);
      MySqlParameter thisStylistId = new MySqlParameter("@thisStylistId", _id);
      cmd.Parameters.Add(thisSpecialtyId);
      cmd.Parameters.Add(thisStylistId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Specialty> GetSpecialties()
    {
      List<Specialty> allSpecialties = new List<Specialty>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialties.* FROM stylists JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id) WHERE stylist_id = @thisId;";
      MySqlParameter thisId = new MySqlParameter("@thisId", _id);
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);
        Specialty thisSpecialty = new Specialty(specialtyName, specialtyId);
        allSpecialties.Add(thisSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
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
