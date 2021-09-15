using System.Linq;
using PLS.Infrastructure.Configurations;

namespace PLS.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {

        public static void Initialize(ApplicationDbContext context, ILevelConfig levelConfig)
        {
            var initializer = new ApplicationDbContextSeed();
            initializer.SeedEverything(context, levelConfig);
        }

        public void SeedEverything(ApplicationDbContext context, ILevelConfig levelConfig)
        {
            context.Database.EnsureCreated();

            SeedLevel(context, levelConfig);
        }

        private void SeedLevel(ApplicationDbContext context, ILevelConfig levelConfig)
        {
            // Db has been seeded
            if (context.Levels.Any()) return;
            for (var i = 1; i <= levelConfig.Total; i++)
            {
                var level = new Domain.Level
                {
                    Floor = i,
                    RowTotal = levelConfig.RowPerLevel,
                    SpotPerRow = levelConfig.SpotPerRow
                };
                for (var row = 1; row <= levelConfig.RowPerLevel; row++)
                {
                    for (var spot = 1; spot <= levelConfig.SpotPerRow; spot++)
                    {
                        level.Spots.Add(new Domain.Spot
                        {
                            Row = row,
                            Number = spot
                        });
                    }
                }

                context.Levels.Add(level);
            }

            var re = context.SaveChanges();
        }
    }
}
