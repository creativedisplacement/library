using Library.Common.Book.Commands.CreateBook;
using Library.Common.Book.Queries.GetBook;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<GetBookModel>
    {
        public string Title { get; set; }
        public ICollection<CreateBookModelCategory> Categories { get; set; }
    }
}