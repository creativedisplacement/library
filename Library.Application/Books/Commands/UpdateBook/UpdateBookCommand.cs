using System;
using Library.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}