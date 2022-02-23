using System;
using Library.Domain.Entities.Abstract;
using Library.Domain.Enums;
using Library.Persistence;

namespace Library.Application;

public abstract class BaseCommandHandler
{
    private readonly LibraryDbContext _context;

    protected BaseCommandHandler(LibraryDbContext context)
    {
        _context = context;
    }

    protected void SetDomainState(BaseEntity entity)
    {
        switch (entity.Status)
        {
            case Status.Added:
                _context.Add(entity);
                break;
            case Status.Updated:
                _context.Update(entity);
                break;
            case Status.Deleted:
                _context.Remove(entity);
                break;
            case Status.Unchanged:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}