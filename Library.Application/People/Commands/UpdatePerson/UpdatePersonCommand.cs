﻿using MediatR;
using System;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}