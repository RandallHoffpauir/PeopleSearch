using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeopleSearch.Application.Common.Interfaces;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public DateTime BirthDate { get; set; }


        public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreatePersonCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                var entity = new Person();

                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Address = request.Address;
                entity.City = request.City;
                entity.State = request.State;
                entity.Zip = request.Zip;
                entity.BirthDate = request.BirthDate;

                _context.Persons.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
