using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PLS.Application.Common.Interfaces;


namespace PLS.Application.Level.Queries.GetAllLevelsQuery
{
    public class GetAllLevelsQuery : IRequest<IList<LevelDto>>
    {
    }

    public class GetAllLevelsQueryHandler : IRequestHandler<GetAllLevelsQuery, IList<LevelDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllLevelsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<LevelDto>> Handle(GetAllLevelsQuery request, CancellationToken cancellationToken)
        {
            var levels = await _context.Levels.Include(x => x.Spots).ProjectTo<LevelDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return levels;
        }
    }
}
