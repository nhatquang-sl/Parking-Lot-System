namespace PLS.Domain
{
    public class Spot
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public string VehicleLicensePlate { get; set; }
        public int VehicleSize { get; set; }

        public int LevelId { get; set; }
        public Level Level { get; set; }
    }
}
