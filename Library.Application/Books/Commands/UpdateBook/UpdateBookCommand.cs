using Library.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<BookCategory> Categories { get; set; }
    }
}