using System;
using System.Collections.Generic;
using DoctorsOffice;
using MySql.Data.MySqlClient;

namespace DoctorsOffice.Models
{
  public class Patient
  {
    private string _name;
    private DateTime _birthdate;

    public Patient(string name, DateTime birthdate)
    {
      _name = name;
      _birthdate = birthdate;
    }

    public string GetName()
    {
      return _name;
    }

    public DateTime GetBirthdate()
    {
      return _birthdate;
    }
  }
}
