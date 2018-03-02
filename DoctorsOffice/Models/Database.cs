using System;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using DoctorsOffice;

namespace DoctorsOffice.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
