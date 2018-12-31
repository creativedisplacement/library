using Library.Domain.Entities;
using System;
using Xunit;

namespace Library.Domain.Tests
{
    public class PersonTests
    {
        private readonly string _name;
        private readonly string _email;
        private readonly bool _isAdmin;

        public PersonTests()
        {
            _name = "John";
            _email = "john@test.com";
            _isAdmin = true;
        }

        [Fact]
        public void Create_Person()
        {
            var person = new Person(_name, _email, _isAdmin);

            Assert.Equal(_name, person.Name);
            Assert.Equal(_email, person.Email);
            Assert.Equal(_isAdmin, person.IsAdmin);
        }

        [Fact]
        public void Create_Person_With_Invalid_Email()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Person()
        {
            const string newName = "Trevor";
            const string newEmail = "trevor@test.com";
            const bool newIsAdmin = false;

            var person = new Person(_name, _email, _isAdmin);
            person.UpdatePerson(newName, newEmail, newIsAdmin);

            Assert.Equal(newName, person.Name);
            Assert.Equal(newEmail, person.Email);
            Assert.Equal(newIsAdmin, person.IsAdmin);
        }

        [Fact]
        public void Remove_Person()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Update_Person_Name()
        {
            const string newName = "Peter";
            var person = new Person(_name, _email, _isAdmin);

            person.UpdateName(newName);
            Assert.Equal(newName, person.Name);
        }

        [Fact]
        public void Update_Person_Email()
        {
            const string newEmail = "peter@test.com";
            var person = new Person(_name, _email, _isAdmin);

            person.UpdateEmail(newEmail);
            Assert.Equal(newEmail, person.Email);
        }

        [Fact]
        public void Update_Person_With_Admin_Permissions()
        {
            var person = new Person(_name, _email, false);
            person.GiveAdminPermissions();

            Assert.True(person.IsAdmin);
        }

        [Fact]
        public void Update_Person_Without_Admin_Permissions()
        {
            var person = new Person(_name, _email, true);
            person.RemoveAdminPermissions();

            Assert.False(person.IsAdmin);
        }
    }
}