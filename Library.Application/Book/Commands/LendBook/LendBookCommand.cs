using System;
using Library.Common.Models.Book;

namespace Library.Application.Book.Commands.LendBook;

public class LendBookCommand : BaseItem, IRequest<LendBookModel>
{
    public Guid LenderId { get; init; }
}