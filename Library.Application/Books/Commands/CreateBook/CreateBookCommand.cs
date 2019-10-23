using Library.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using Library.Application.Books.Queries.GetBook;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<GetBookModel>
    {
        public string Title { get; set; }
        public ICollection<CreateBookModelCategory> Categories { get; set; }
    }
}