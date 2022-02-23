using System;
using System.Linq;
using FluentValidation;
using Library.Persistence;

namespace Library.Application;

public abstract class BasePersonCommandValidator<T> : AbstractValidator<T> where T : class
{
    private readonly LibraryDbContext _context;

    protected BasePersonCommandValidator(LibraryDbContext context)
    {
        _context = context;
    }

    protected bool EmailAddressExists(string email, Guid personId)
    {
        var result = _context.Persons.Count(c => c.Email == email && c.Id != personId);
        return result <= 0;
    }

    protected bool NameExists(string name, Guid personId)
    {
        var result = _context.Persons.Count(c => c.Name == name && c.Id != personId);
        return result <= 0;
    }
}