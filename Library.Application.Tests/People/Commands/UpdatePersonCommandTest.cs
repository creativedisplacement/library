﻿using Library.Application.People.Commands.UpdatePerson;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Xunit;

namespace Library.Application.Tests.People.Commands
{

    public class UpdatePeopleCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly UpdatePersonCommandHandler _commandHandler;

        public UpdatePeopleCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new UpdatePersonCommandHandler(_context);
        }

        [Fact]
        public async Task Update_Person()
        {
            var command = new UpdatePersonCommand
            {
                Id = (await _context.Persons.FirstOrDefaultAsync()).Id,
                Name = "Name2",
                Email = "email2@mail.com",
                IsAdmin = true
            };

            await _commandHandler.Handle(command, CancellationToken.None);

            var person = await _context.Persons.FindAsync(command.Id);

            Assert.Equal(command.Name, person.Name);
            Assert.Equal(command.Email, person.Email);
            Assert.Equal(command.IsAdmin, person.IsAdmin);
        }

        [Fact]
        public void Update_Person_With_No_Id_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Update_Person_With_No_Name_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [Fact]
        public void Update_Person_With_No_Email_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
        }

        [Fact]
        public void Update_Person_With_Invalid_Email_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, "111");
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Persons.Add(new Person("Name", "email@mail.com", false));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}