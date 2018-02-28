using System;
using System.Collections.Generic;
using DoctorsOffice;
using MySql.Data.MySqlClient;

namespace DoctorsOffice.Models
{
    public class Doctor
    {
        private int _id = 0;
        private string _name;
        private int _age;
        private string _specialty;

        public Doctor(string name, int age, string specialty)
        {
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
            //add to database
        }

        public void Delete()
        {
            //delete from database
        }

        public static List<Doctor> GetAll()
        {
            List<Doctor> allDoctors = new List<Doctor>{};

            //get all from database

            return allDoctors;
        }
    }
}
