using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _name;

    //specialty constructor
    public Specialty(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@thisName);";
        MySqlParameter thisName = new MySqlParameter("@thisName", _name);
        cmd.Parameters.Add(thisName);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }
    }

    public static List<Specialty> GetAll()
    {
        List<Specialty> allSpecialties = new List<Specialty>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties;";
        MySqlDataReader rdr = cmd.ExecuteReader();
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            Specialty newSpecialty = new Specialty(name, id);
            allSpecialties.Add(newSpecialty);
        }
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }
        return allSpecialties;
    }

    public static Specialty Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties WHERE id = @thisId;";
        MySqlParameter thisId = new MySqlParameter("@thisId", id);
        cmd.Parameters.Add(thisId);
        MySqlDataReader rdr = cmd.ExecuteReader();
        int foundId = 0;
        string foundName = "";
        while(rdr.Read())
        {
            foundId = rdr.GetInt32(0);
            foundName = rdr.GetString(1);
        }
        Specialty foundSpecialty = new Specialty(foundName, foundId);
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }
        return foundSpecialty;
    }

    public void Edit(string newName)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE specialties SET name = @newName WHERE id = @thisId;";
        MySqlParameter thisNewName = new MySqlParameter("@newName", newName);
        MySqlParameter thisId = new MySqlParameter("@thisId", _id);
        cmd.Parameters.Add(thisNewName);
        cmd.Parameters.Add(thisId);
        cmd.ExecuteNonQuery();
        _name = newName;
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter("@thisId", _id);
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM specialties";
        cmd.ExecuteNonQuery();
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }
    }




    //equality override to compare client objects
    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool nameEquality = (this.GetName() == newSpecialty.GetName());
        return (idEquality && nameEquality);
      }
    }
  }
}