using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PLS.Application.Common.Interfaces;

namespace PLS.Application.Parking.Commands.Leave
{
    public class LeaveCommand : IRequest<Unit>
    {
        public string LicensePlate { get; set; }
    }

    public class LeaveCommandHandler : IRequestHandler<LeaveCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public LeaveCommandHandler(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Unit> Handle(LeaveCommand request, CancellationToken cancellationToken)
        {
            _context.Spots.Where(x => x.VehicleLicensePlate == request.LicensePlate.ToLower()).ToList()
                .ForEach(a =>
                {
                    a.VehicleLicensePlate = null;
                    a.VehicleSize = 0;
                });

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
