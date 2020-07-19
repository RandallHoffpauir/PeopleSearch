using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeopleSearch.Application.Common.Exceptions;
using PeopleSearch.Application.Common.Interfaces;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest
    {

        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public DateTime BirthDate { get; set; }


        public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdatePersonCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Persons.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Person), request.Id);
                }

                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Address = request.Address;
                entity.City = request.City;
                entity.State = request.State;
                entity.Zip = request.Zip;
                entity.BirthDate = request.BirthDate;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
