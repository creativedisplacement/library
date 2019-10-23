﻿using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : BaseCommandHandler, IRequestHandler<UpdateBookCommand, UpdateBookModel>
    {
        private readonly LibraryDbContext _context;

        public UpdateBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UpdateBookModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            book.UpdateBook(request.Title, request.Categories.ToList());
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(c => new UpdateBookModelCategory { Id = c.CategoryId, Name = c.Category.Name }).ToList()
            };
        }
    }
}