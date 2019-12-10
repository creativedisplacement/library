using Grpc.Core;
using Library.Application.Books.Queries.GetBooks;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Server
{
    public class BooksService :  Books.BooksBase
    {
        private readonly ILogger<BooksService> _logger;
        private readonly IMediator _mediator;
        public BooksService(ILogger<BooksService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task<BooksReply> GetBooks(GetBooksRequest request, ServerCallContext context)
        {
            //Get books back
            //mediator 
            //
            try
            {
                var books = await _mediator.Send(new GetBooksQuery
                {
                    Title = request.Title,
                    //CategoryIds = new List<Guid>(request.CategoryIds.Select(s => new Guid(s)).AsEnumerable()),
                    //LenderId = new Guid(request.LenderId),
                    IsAvailable = request.IsAvailable
                });

                var x = books.Books.Select(b => new BookReply
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    
                });

                return new BooksReply
                {
                    Books = { x.ToList() }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           

            
        }
    }
}
