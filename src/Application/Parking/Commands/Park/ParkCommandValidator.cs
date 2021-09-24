using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PLS.Application.Common.Interfaces;

namespace PLS.Application.Parking.Commands.Park
{
    public class ParkCommandValidator : AbstractValidator<ParkCommand>

    {
        private readonly IApplicationDbContext _context;

        public ParkCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.LicensePlate).NotEmpty().WithMessage("This License Plate is required!")
                .MustAsync(BeUniqueLicensePlate).WithMessage("Your License Plate is duplicated!");
        }

        public async Task<bool> BeUniqueLicensePlate(string licensePlate, CancellationToken cancellationToken)
        {
            return await _context.Spots.AllAsync(l => l.VehicleLicensePlate != licensePlate.ToLower());
        }
    }
}
 