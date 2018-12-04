using MediatR;
using System;

namespace Library.Application.Books.Queries.GetBook
{
    public class GetBookQuery : IRequest<GetBookModel>
    {
        public Guid Id { get; set; }
    }
}