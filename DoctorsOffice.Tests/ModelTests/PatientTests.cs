using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DoctorsOffice.Models;

namespace DoctorsOffice.Tests
{
    [TestClass]
    public class PatientTests : IDisposable
    {
        public PatientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=doctorsoffice_tests;";
        }

        public void Dispose()
        {
            Doctor.DeleteAll();
            Patient.DeleteAll();
        }

        [TestMethod]
        public void GetFirstName_ReturnsPatientFirstName_String()
        {
            string testFirstName = "Sarah";
            string testLastName = "Clark";
            Patient testPatient = new Patient(testFirstName, testLastName, new DateTime(2000, 1, 1));
            string result = testPatient.GetFirstName();
            Assert.AreEqual(testFirstName, result);
        }

        [TestMethod]
        public void GetLastName_ReturnsPatientLastName_String()
        {
            string testFirstName = "Sarah";
            string testLastName = "Clark";
            Patient testPatient = new Patient(testFirstName, testLastName, new DateTime(2000, 1, 1));
            string result = testPatient.GetLastName();
            Assert.AreEqual(testLastName, result);
        }

        [TestMethod]
        public void GetAll_PatientsEmptyAtFirst_0()
        {
            Assert.AreEqual(0, Patient.GetAll().Count);
        }

        [TestMethod]
        public void GetAll_ReturnsAllPatients_DoctorList()
        {
            Patient newPatient1 = new Patient("Sarah", "Clark", new DateTime(2000, 1, 1));
            Patient newPatient2 = new Patient("Theo", "Powell", new DateTime(2000, 1, 1));

            newPatient1.Save();
            newPatient2.Save();

            List<Patient> newList = new List<Patient>{newPatient1, newPatient2};
            List<Patient> result = Patient.GetAll();

            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Save_SavesPatientToDatabase_PatientList()
        {
            Patient testPatient = new Patient("Sarah", "Clark", new DateTime(2000, 1, 1));
            testPatient.Save();

            List<Patient> testList = new List<Patient>{testPatient};
            List<Patient> result = Patient.GetAll();

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_FindsPatientInDatabase_Patient()
        {
            Patient testPatient = new Patient("Sarah", "Clark", new DateTime(2000, 1, 1));
            testPatient.Save();

            Patient foundPatient = Patient.Find(testPatient.GetId());

            Assert.AreEqual(testPatient, foundPatient);
        }

        [TestMethod]
        public void Delete_DeletesPatientFromDatabase_PatientList()
        {
            Patient newPatient1 = new Patient("Sarah", "Clark", new DateTime(2000, 1, 1));
            Patient newPatient2 = new Patient("Theo", "Powell", new DateTime(2000, 1, 1));

            newPatient1.Save();
            newPatient2.Save();
            newPatient2.Delete();

            List<Patient> newList = new List<Patient>{newPatient1};
            List<Patient> result = Patient.GetAll();

            CollectionAssert.AreEqual(newList, result);
        }
    }
}
