using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PLS.Application.Common.Exceptions;
using PLS.Application.Common.Interfaces;
using PLS.Domain.Enumerations;

namespace PLS.Application.Parking.Commands.Park
{
    public class ParkCommand : IRequest<List<SpotDto>>
    {
        public string LicensePlate { get; set; }
        public VehicleType Type { get; set; }
    }

    public class ParkCommandHandler : IRequestHandler<ParkCommand, List<SpotDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParkCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SpotDto>> Handle(ParkCommand request, CancellationToken cancellationToken)
        {
            if (_context.Spots.Where(x => x.VehicleLicensePlate == request.LicensePlate.ToLower()).Any()) 
                throw new BadRequestException("Your License Plate is duplicated!");

            int size = (int)request.Type;
            var spots = await _context.Spots.Where(x => x.VehicleLicensePlate == null)
                .ProjectTo<SpotDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Level).ThenBy(x => x.Row).ThenBy(x => x.Number).ToListAsync();

            var parkingSpots = new List<SpotDto>();
            if (size == 1 && spots.Count > 0)
            {
                parkingSpots.Add(spots[0]);
            }
            else
            {
                parkingSpots = FindSpots(spots, size);
            }
            if (parkingSpots.Count == 0) 
                throw new BadRequestException("There is no available spots for you vehicle!");

            var parkingSpotIds = parkingSpots.Select(x => x.Id).ToList();
            _context.Spots.Where(x => parkingSpotIds.Contains(x.Id)).ToList()
                .ForEach(a =>
                {
                    a.VehicleLicensePlate = request.LicensePlate.ToLower();
                    a.VehicleSize = (int)request.Type;
                });

            await _context.SaveChangesAsync(cancellationToken);
            return parkingSpots;
        }

        /// <summary>
        /// Find consecutive spots
        /// </summary>
        /// <param name="spots"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<SpotDto> FindSpots(List<SpotDto> spots, int size)
        {
            for (var i = 0; i < spots.Count - size; i++)
            {
                var isValid = true;
                var result = new List<SpotDto>();
                for (var j = 0; j < size; j++)
                {
                    var curSpot = spots[i + j];
                    var nextSpot = spots[i + j + 1];
                    if (curSpot.Level != nextSpot.Level ||
                        curSpot.Row != nextSpot.Row ||
                        curSpot.Number + 1 != nextSpot.Number)
                    {
                        isValid = false;
                        break;
                    }
                    result.Add(curSpot);
                }

                if (isValid) return result;
            }
            return new List<SpotDto>();
        }
    }
}
