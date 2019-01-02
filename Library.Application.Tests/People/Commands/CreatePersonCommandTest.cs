using Library.Application.People.Commands.CreatePerson;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Xunit;

namespace Library.Application.Tests.People.Commands
{

    public class CreatePersonCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly CreatePersonCommandHandler _commandHandler;

        public CreatePersonCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new CreatePersonCommandHandler(_context);
        }

        [Fact]
        public async Task Create_New_Person()
        {
            var command = new CreatePersonCommand
            {
                Name = "Name",
                Email = "email@mail.com",
                IsAdmin = false
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            var person = await _context.Persons.SingleOrDefaultAsync(c => c.Name == command.Name);

            Assert.Equal(command.Name, person.Name);
            Assert.Equal(command.Email, person.Email);
            Assert.Equal(command.IsAdmin, person.IsAdmin);
        }

        [Fact]
        public void Create_New_Person_With_No_Email_Throws_Exception()
        {
            var validator = new CreatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
        }


        [Fact]
        public void Create_New_Person_With_Invalid_Email_Throws_Exception()
        {
            var validator = new CreatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, "111");
        }

        [Fact]
        public void Create_New_Person_With_No_Name_Throws_Exception()
        {
            var validator = new CreatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}