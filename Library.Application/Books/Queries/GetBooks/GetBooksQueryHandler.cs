﻿using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksModel>
    {
        private readonly LibraryDbContext _context;

        public GetBooksQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetBooksModel> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Book> books = _context.Books
                .Include(i => i.BookCategories)
                .ThenInclude(c => c.Category);

            if (!string.IsNullOrEmpty(request.Title))
            {
                books = books.Where(b => b.Title.Contains(request.Title));
            }

            if (request.CategoryIds.Any())
            {
                books = books.Where(b => b.BookCategories.Any(c => request.CategoryIds.Any()));
            }

            if (request.LenderId != Guid.Empty)
            {
                books = books.Where(b => b.Lender.Id == request.LenderId);
            }

            if (request.IsAvailable.HasValue)
            {
                books = books.Where(b => b.IsAvailable == request.IsAvailable.Value);
            }

            return new GetBooksModel
            {
                Books = await books
                    .Select(b => new GetBookModel {Id = b.Id, Title = b.Title, Categories = b.BookCategories.Select(c => new GetBookModelCategory{ Id = c.CategoryId, Name = c.Category.Name})})
                    .Include(i => i.Categories)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}