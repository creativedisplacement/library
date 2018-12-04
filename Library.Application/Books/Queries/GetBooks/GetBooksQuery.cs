using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<GetBooksModel>
    {
        public string Title { get; set; }
        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();
        public Guid LenderId { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
