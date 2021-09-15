using System.Collections.Generic;
using PLS.Application.Common.Interfaces.Mappings;

namespace PLS.Application.Level.Queries
{
    public class LevelDto : IMapFrom<Domain.Entities.Level>
    {
        public LevelDto()
        {
            Spots = new List<SpotDto>();
        }

        public int Id { get; set; }
        public int Floor { get; set; }

        public List<SpotDto> Spots { get; set; }
    }
}
