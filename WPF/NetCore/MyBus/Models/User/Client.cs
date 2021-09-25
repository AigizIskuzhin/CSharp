using System;

namespace MyBus.Models.User
{
    public class Client : Base.User
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
    }
}
