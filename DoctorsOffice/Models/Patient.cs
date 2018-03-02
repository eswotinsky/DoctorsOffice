using System;
using System.Collections.Generic;
using DoctorsOffice;
using MySql.Data.MySqlClient;

namespace DoctorsOffice.Models
{
    public class Patient
    {
        private int _id = 0;
        private string _firstName;
        private string _lastName;
        private DateTime _birthdate;
        private int _doctorId;

        public Patient(string firstName, string lastName, DateTime birthdate, int doctorId = 0)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthdate = birthdate;
            _doctorId = doctorId;
        }

        public override bool Equals(System.Object otherPatient)
        {
            if (!(otherPatient is Patient))
            {
                return false;
            }
            else
            {
                Patient newPatient = (Patient) otherPatient;
                return this.GetId().Equals(newPatient.GetId());
            }
        }

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }

        public int GetId()
        {
            return _id;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
            return _lastName;
        }

        public DateTime GetBirthdate()
        {
            return _birthdate;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO patients (firstname, lastname, birthdate, doctor_id) VALUES (@firstName, @lastName, @birthdate, @doctorId);";

            MySqlParameter firstName = new MySqlParameter();
            firstName.ParameterName = "@firstName";
            firstName.Value = this._firstName;
            cmd.Parameters.Add(firstName);

            MySqlParameter lastName = new MySqlParameter();
            lastName.ParameterName = "@lastName";
            lastName.Value = this._lastName;
            cmd.Parameters.Add(lastName);

            MySqlParameter birthdate = new MySqlParameter();
            birthdate.ParameterName = "@birthdate";
            birthdate.Value = this._birthdate;
            cmd.Parameters.Add(birthdate);

            MySqlParameter doctorId = new MySqlParameter();
            doctorId.ParameterName = "@doctorId";
            doctorId.Value = this._doctorId;
            cmd.Parameters.Add(doctorId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            //delete from database
        }

        public static Patient Find(int id)
        {
            //find patient from database

            Patient myPatient = new Patient("Sarah", "Clark", new DateTime(2000, 1, 1)); //firstName, lastName, birthdate

            return myPatient;
        }

        public static List<Patient> GetAll()
        {
            List<Patient> allPatients = new List<Patient>{};

            //get all from database

            return allPatients;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM patients;";
            cmd.ExecuteNonQuery();

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
