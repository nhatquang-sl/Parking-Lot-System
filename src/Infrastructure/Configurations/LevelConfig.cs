namespace PLS.Infrastructure.Configurations
{
    public class LevelConfig : ILevelConfig
    {
        public int Total { get; set; }
        public int RowPerLevel { get; set; }
        public int SpotPerRow { get; set; }
    }
}
