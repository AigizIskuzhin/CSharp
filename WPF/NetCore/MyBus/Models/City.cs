using System.Collections.Generic;

namespace MyBus.Models
{
    public class City
    {
        public int ID { get; }
        public List<Station> Stations { get; } = new List<Station>();
    }
}
