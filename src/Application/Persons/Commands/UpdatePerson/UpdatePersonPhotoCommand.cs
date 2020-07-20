using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Application.Common.Interfaces;

namespace PeopleSearch.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonPhotoCommand : IRequest
    {
        public long PersonId { get; set; }
        public byte[] Photo { get; set; }

        public class UpdatePersonPhotoCommandHandler: IRequestHandler<UpdatePersonPhotoCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdatePersonPhotoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdatePersonPhotoCommand request, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == request.PersonId, cancellationToken);
                if (person != null)
                {
                    person.Photo = request.Photo;

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}
