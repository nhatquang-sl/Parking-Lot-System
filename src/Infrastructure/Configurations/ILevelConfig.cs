namespace PLS.Infrastructure.Configurations
{
    public interface ILevelConfig
    {
        int Total { get; set; }
        int RowPerLevel { get; set; }
        int SpotPerRow { get; set; }
    }
}
