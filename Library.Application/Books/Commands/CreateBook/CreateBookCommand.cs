using Library.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest
    {
        public string Title { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}