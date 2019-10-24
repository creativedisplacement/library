using Library.Common;
using Library.Common.Book.Commands.LendBook;
using MediatR;
using System;

namespace Library.Application.Books.Commands.LendBook
{
    public class LendBookCommand : BaseItem, IRequest<LendBookModel>
    {
        public Guid LenderId { get; set; }
    }
}