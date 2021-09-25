using System;
using System.ComponentModel.DataAnnotations;

namespace MyBus.Models.User.Base
{
    public class User
    {
        /// <summary>
        /// ID - database primary key
        /// </summary>
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^\+[7]\(\d{3}\)-\d{3}-\d{4}$", ErrorMessage = "Номер телефона должен иметь формат +7xxx-xxx-xxxx")]
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }

        public string Rank
        {
            get
            {
                return Level switch
                {
                    1 => "Клиент",
                    2 => "Сотрудник",
                    3 => "Администратор",
                    _ => ""
                };
            }
        }
        public DateTime CreationTime { get; }
    }
}
