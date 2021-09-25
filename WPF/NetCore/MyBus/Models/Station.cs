
namespace MyBus.Models
{
    public class Station
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
    }
}
