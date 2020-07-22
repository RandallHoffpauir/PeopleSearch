using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Application.Common.Interfaces;

namespace PeopleSearch.Application.Persons.Queries.GetPersons
{
    public class GetPersonsQuery : IRequest<PersonsVm>
    {
        public string NameSearch { get; set; }

        public class GetPersonQueryHandler : IRequestHandler<GetPersonsQuery, PersonsVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonsVm> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
            {
                var vm = new PersonsVm();
                vm.Persons = await _context.Persons
                    .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                    .Where(p => p.FirstName.Contains(request.NameSearch) || p.LastName.Contains(request.NameSearch))
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToListAsync(cancellationToken);

                return vm;
            }
        }
    }
}
