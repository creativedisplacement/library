using Library.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Library.Domain.Tests
{
    [TestClass]
    public class PersonTests
    {
        private string _name;
        private string _email;
        private bool _isAdmin;

        [TestInitialize]
        public void Initialise()
        {
            _name = "John";
            _email = "john@test.com";
            _isAdmin = true;
        }

        [TestMethod]
        public void Create_Person()
        {
            var person = new Person(_name, _email, _isAdmin);

            Assert.AreEqual(_name, person.Name);
            Assert.AreEqual(_email, person.Email);
            Assert.AreEqual(_isAdmin, person.IsAdmin);
        }

        [TestMethod]
        public void Create_Person_With_Invalid_Email()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Update_Person()
        {
            const string newName = "Trevor";
            const string newEmail = "trevor@test.com";
            const bool newIsAdmin = false;

            var person = new Person(_name, _email, _isAdmin);
            person.UpdatePerson(newName, newEmail, newIsAdmin);

            Assert.AreEqual(newName, person.Name);
            Assert.AreEqual(newEmail, person.Email);
            Assert.AreEqual(newIsAdmin, person.IsAdmin);
        }

        [TestMethod]
        public void Remove_Person()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Update_Person_Name()
        {
            const string newName = "Peter";
            var person = new Person(_name, _email, _isAdmin);

            person.UpdateName(newName);
            Assert.AreEqual(newName, person.Name);
        }

        [TestMethod]
        public void Update_Person_Email()
        {
            const string newEmail = "peter@test.com";
            var person = new Person(_name, _email, _isAdmin);

            person.UpdateEmail(newEmail);
            Assert.AreEqual(newEmail, person.Email);
        }

        [TestMethod]
        public void Update_Person_With_Admin_Permissions()
        {
            var person = new Person(_name, _email, false);
            person.GiveAdminPermissions();

            Assert.AreEqual(true, person.IsAdmin);
        }

        [TestMethod]
        public void Update_Person_Without_Admin_Permissions()
        {
            var person = new Person(_name, _email, true);
            person.RemoveAdminPermissions();

            Assert.AreEqual(false, person.IsAdmin);
        }
    }
}