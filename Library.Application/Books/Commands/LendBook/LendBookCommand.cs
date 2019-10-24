using Library.Common;
using MediatR;
using System;

namespace Library.Application.Books.Commands.LendBook
{
    public class LendBookCommand : BaseItem, IRequest
    {
        public Guid LenderId { get; set; }
    }
}