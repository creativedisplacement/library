﻿using FluentValidation.TestHelper;
using Library.Application.People.Commands.DeletePerson;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Commands
{

    public class DeletePersonCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly DeletePersonCommandHandler _commandHandler;

        public DeletePersonCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new DeletePersonCommandHandler(_context);
        }

        [Fact]
        public async Task Delete_Person()
        {
            var command = new DeletePersonCommand
            {
                Id = (await _context.Persons.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.Null(await _context.Persons.FindAsync(command.Id));
        }

        [Fact]
        public void Delete_Person_With_No_Id_Throws_Exception()
        {
            var validator = new DeletePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
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