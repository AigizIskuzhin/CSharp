using System;
using System.Collections.Generic;

namespace MyBus.Models.Company.Base
{
    public class Company
    {
        public IEnumerable<string> Phones { get; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
