using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DoctorsOffice.Models;

namespace DoctorsOffice.Tests
{
    [TestClass]
    public class DoctorTests : IDisposable
    {
        public DoctorTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=doctorsoffice_tests;";
        }

        public void Dispose()
        {
            Doctor.DeleteAll();
            Patient.DeleteAll();
        }

        [TestMethod]
        public void GetName_ReturnsDoctorName_String()
        {
            string testName = "Bob";
            Doctor testDoctor = new Doctor(testName, 50, "Pediatrics");
            string result = testDoctor.GetName();
            Assert.AreEqual(testName, result);
        }

        [TestMethod]
        public void Save_SavesDoctorToDatabase_DoctorList()
        {
            Doctor testDoctor = new Doctor("Bob", 50, "Pediatrics");
            testDoctor.Save();

            List<Doctor> testList = new List<Doctor>{testDoctor};
            List<Doctor> result = Doctor.GetAll();

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetAll_DoctorsEmptyAtFirst_0()
        {
            Assert.AreEqual(0, Doctor.GetAll().Count);
        }

        [TestMethod]
        public void GetAll_ReturnsAllDoctors_DoctorList()
        {
            Doctor newDoctor1 = new Doctor("Bob", 50, "Pediatrics");
            Doctor newDoctor2 = new Doctor("Susan", 38, "Oncology");

            newDoctor1.Save();
            newDoctor2.Save();

            List<Doctor> newList = new List<Doctor>{newDoctor1, newDoctor2};
            List<Doctor> result = Doctor.GetAll();

            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Delete_DeletesDoctorFromDatabase_DoctorList()
        {
            Doctor newDoctor1 = new Doctor("Bob", 50, "Pediatrics");
            Doctor newDoctor2 = new Doctor("Susan", 38, "Oncology");

            newDoctor1.Save();
            newDoctor2.Save();
            newDoctor2.Delete();

            List<Doctor> newList = new List<Doctor>{newDoctor1};
            List<Doctor> result = Doctor.GetAll();

            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void GetPatients_RetrievesAllPatientsBelongingToDoctor_PatientList()
        {
            Doctor testDoctor = new Doctor("Bob", 50, "Pediatrics");
            testDoctor.Save();

            Patient testPatient = new Patient("Sarah", "Clark", new DateTime(2000, 1, 1), testDoctor.GetId());
            testPatient.Save();

            List<Patient> newList = new List<Patient>{testPatient};
            List<Patient> result = testDoctor.GetPatients();

            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Find_FindsDoctorInDatabase_Doctor()
        {
            Doctor testDoctor = new Doctor("Bob", 50, "Pediatrics");
            testDoctor.Save();

            Doctor foundDoctor = Doctor.Find(testDoctor.GetId());

            Assert.AreEqual(testDoctor, foundDoctor);
        }
    }
}
