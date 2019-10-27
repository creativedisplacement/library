using Library.Common;
using Library.Common.Book.Commands.CreateBook;
using Library.Common.Book.Queries.GetBook;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Book.Commands.CreateBook
{
    public class CreateBookCommand : BaseTitleItem, IRequest<GetBookModel>
    {
        public ICollection<CreateBookModelCategory> Categories { get; set; }
    }
}