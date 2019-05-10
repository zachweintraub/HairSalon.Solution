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

    //returns the client's id
    public int GetId()
    {
      return _id;
    }

    //returns the client's corresponding stylist id
    public int GetStylistId()
    {
      return _stylistId;
    }

    //save an instance of client to the DB
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (stylist_id, first, last) VALUES (@StylistId, @ClientFirst, @ClientLast);";
      MySqlParameter first = new MySqlParameter();
      first.ParameterName = "@ClientFirst";
      first.Value = _first;
      cmd.Parameters.Add(first);
      MySqlParameter last = new MySqlParameter();
      last.ParameterName = "@ClientLast";
      last.Value = _last;
      cmd.Parameters.Add(last);
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = _stylistId;
      cmd.Parameters.Add(stylistId);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //returns all instances of client from the DB
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int stylistId = rdr.GetInt32(1);
        string first = rdr.GetString(2);
        string last = rdr.GetString(3);

        Client newClient = new Client(first, last, stylistId, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    //returns client by id
    public static Client Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      int stylistId = 0;
      string first = "";
      string last = "";
      while (rdr.Read())
      {
        id = rdr.GetInt32(0);
        stylistId = rdr.GetInt32(1);
        first = rdr.GetString(2);
        last = rdr.GetString(3);
      }
      Client foundClient = new Client(first, last, stylistId, id);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }

    //equality override to compare client objects
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        return (idEquality && stylistIdEquality && nameEquality);
      }
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
