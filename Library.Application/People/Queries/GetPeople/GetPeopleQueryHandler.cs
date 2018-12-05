﻿using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, GetPeopleModel>
    {
        private readonly LibraryDbContext _context;

        public GetPeopleQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetPeopleModel> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Person> people = _context.Persons;

            if (request.Id != default(Guid))
            {
                people = people.Where(p => p.Id == request.Id);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                people = people.Where(p => p.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                people = people.Where(p => p.Email.Contains(request.Email));
            }

            if (request.IsAdmin.HasValue)
            {
                people = people.Where(p => p.IsAdmin == request.IsAdmin);
            }

            return new GetPeopleModel()
            {
                People = await people
                    .Select(p => new GetPersonModel() { Id = p.Id, Name = p.Name, Email = p.Email, IsAdmin = p.IsAdmin})
                    .ToListAsync(cancellationToken)
            };
        }
    }
}