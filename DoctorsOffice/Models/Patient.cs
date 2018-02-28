using System;
using System.Collections.Generic;
using DoctorsOffice;
using MySql.Data.MySqlClient;

namespace DoctorsOffice.Models
{
    public class Patient
    {
        private int _id = 0;
        private string _name;
        private DateTime _birthdate;

        public Patient(string name, DateTime birthdate)
        {
            _name = name;
            _birthdate = birthdate;
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

        public string GetName()
        {
            return _name;
        }

        public DateTime GetBirthdate()
        {
            return _birthdate;
        }

        public void Save()
        {
            //save to database
        }

        public void Delete()
        {
            //delete from database
        }

        public static List<Patient> GetAll()
        {
            List<Patient> allPatients = new List<Patient>{};

            //get all from database

            return allPatients;
        }
    }
}
