using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLS.Domain;

namespace PLS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Spot> Spots { get; set; }

        DbSet<Domain.Level> Levels { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
