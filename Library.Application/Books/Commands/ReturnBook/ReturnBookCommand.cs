using MediatR;
using System;

namespace Library.Application.Books.Commands.ReturnBook
{
    public class ReturnBookCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}