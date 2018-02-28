using System;
using System.Collections.Generic;
using DoctorsOffice;
using MySql.Data.MySqlClient;

namespace DoctorsOffice.Models
{
  public class Doctor
  {
    private string _name;
    private int _age;
    private string _specialty;

    public Doctor(string name, int age, string specialty)
    {
      _name = name;
      _age = age;
      _specialty = specialty;
    }
  }
}
