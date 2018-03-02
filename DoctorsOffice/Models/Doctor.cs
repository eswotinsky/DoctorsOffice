using System;
using System.Collections.Generic;
using DoctorsOffice;
using MySql.Data.MySqlClient;

namespace DoctorsOffice.Models
{
    public class Doctor
    {
        private int _id;
        private string _name;
        private int _age;
        private string _specialty;

        public Doctor(string name, int age, string specialty, int id = 0)
        {
            _id = id;
            _name = name;
            _age = age;
            _specialty = specialty;
        }

        public override bool Equals(System.Object otherDoctor)
        {
            if (!(otherDoctor is Doctor))
            {
                return false;
            }
            else
            {
                Doctor newDoctor = (Doctor) otherDoctor;
                return this.GetId().Equals(newDoctor.GetId());
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

        public string GetName()
        {
            return _name;
        }

        public int GetAge()
        {
            return _age;
        }

        public string GetSpecialty()
        {
            return _specialty;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO doctors (name, age, specialty) VALUES (@name, @age, @specialty);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter age = new MySqlParameter();
            age.ParameterName = "@age";
            age.Value = this._age;
            cmd.Parameters.Add(age);

            MySqlParameter specialty = new MySqlParameter();
            specialty.ParameterName = "@specialty";
            specialty.Value = this._specialty;
            cmd.Parameters.Add(specialty);

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

        public static List<Doctor> GetAll()
        {
            List<Doctor> allDoctors = new List<Doctor> {};
            MySqlConnection conn = DB.Connection();

            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM doctors;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int doctorId = rdr.GetInt32(0);
              string doctorName = rdr.GetString(1);
              int doctorAge = rdr.GetInt32(2);
              string doctorSpecialty = rdr.GetString(3);
              Doctor newDoctor = new Doctor(doctorName, doctorAge, doctorSpecialty, doctorId);
              allDoctors.Add(newDoctor);
            }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return allDoctors;
       }

        public List<Patient> GetPatients()
        {
            List<Patient> myPatients = new List<Patient> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patients WHERE doctor_id = @doctorId;";

            MySqlParameter doctorId = new MySqlParameter();
            doctorId.ParameterName = "@doctorId";
            doctorId.Value = this._doctorId;
            cmd.Parameters.Add(doctorId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int patientId = rdr.GetInt32(0);
                string patientFirstName = rdr.GetString(1);
                string patientLastName = rdr.GetString(2);
                //datetime read
                int patientDoctorId = rdr.GetInt32(2);
                Client newClient = new Client(patientFirstName, patientLastName, birthdate, doctorId);

                allMyClients.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return myPatients;
        }

        public static Doctor Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM doctors WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            int doctorId = 0;
            string doctorName = "";
            int doctorAge = 0;
            string doctorSpecialty = "";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                doctorId = rdr.GetInt32(0);
                doctorName = rdr.GetString(1);
                doctorAge = rdr.GetInt32(2);
                doctorSpecialty = rdr.GetString(3);
            }
            Doctor myDoctor = new Doctor(doctorName, doctorAge, doctorSpecialty, doctorId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return myDoctor;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM doctors;";
            cmd.ExecuteNonQuery();

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
