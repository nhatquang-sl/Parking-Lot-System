using System.Collections.Generic;

namespace PLS.Domain
{
    public class Level
    {
        public Level()
        {
            Spots = new HashSet<Spot>();
        }

        public int Id { get; set; }

        public int Floor { get; set; }
        public int RowTotal { get; set; }
        public int SpotPerRow { get; set; }

        public virtual ICollection<Spot> Spots { get; private set; }
    }
}
