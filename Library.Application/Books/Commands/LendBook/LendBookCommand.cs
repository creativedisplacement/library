using MediatR;
using System;

namespace Library.Application.Books.Commands.LendBook
{
    public class LendBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid LenderId { get; set; }
    }
}