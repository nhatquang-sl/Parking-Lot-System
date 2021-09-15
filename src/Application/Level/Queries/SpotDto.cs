using AutoMapper;
using PLS.Application.Common.Interfaces.Mappings;

namespace PLS.Application.Level.Queries
{
    public class SpotDto : IMapFrom<Domain.Spot>
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public int Level { get; set; }
        public string VehicleLicensePlate { get; set; }
        public int VehicleSize { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Spot, SpotDto>()
                .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level.Floor));
        }
    }
}
