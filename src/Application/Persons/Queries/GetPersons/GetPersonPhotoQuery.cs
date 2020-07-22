using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Application.Common.Interfaces;

namespace PeopleSearch.Application.Persons.Queries.GetPersons
{
    public class GetPersonPhotoQuery : IRequest<string>
    {
        public long PersonId { get; set; }

        public class GetPersonPhotoQueryHandler : IRequestHandler<GetPersonPhotoQuery, string>
        {
            private readonly IApplicationDbContext _context;

            public GetPersonPhotoQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(GetPersonPhotoQuery request, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == request.PersonId, cancellationToken: cancellationToken);

                if (person?.Photo != null)
                {
                    string imageBase64Data = Convert.ToBase64String(person.Photo);
                    string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

                    return imageDataURL;
                }

                return "";
            }
        }
    }
}
