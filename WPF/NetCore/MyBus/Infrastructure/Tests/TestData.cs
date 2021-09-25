using System;
using System.Collections.Generic;
using MyBus.Models.User;
using MyBus.Models.User.Base;

namespace MyBus.Infrastructure.Tests
{
    public class TestData
    {
        public static TestData Data = new();

        public List<User> Users { get; set; }= new();

        private readonly Random gen = new();
        private DateTime start;
        private int range;
        DateTime RandomDay()
        {
            start = new(1995, 1, 1);
            range = (DateTime.Now - start).Days;
            return start.AddDays(gen.Next(range));
        }
        public TestData()
        {
            LoadClients();   
            LoadWorkers();
            LoadAdmins();
        }

        private void LoadClients()
        {
            for (int i = 0; i < 50; i++)
            {
                var client = new Client
                {
                    Name = $"Клиент {i + 1}",
                    Birthday = RandomDay(),
                    ID = i + 1,
                    Level = 1,
                    Password = i.ToString(),
                    Patronymic = $"Отчество {i+1}",
                    Phone = $"892730693{i}",
                    Surname = $"Фамилия {i + 1}"
                };
                client.Phone = client.Phone.Length == 10 ? client.Phone + "9" : client.Phone;
                Users.Add(client);
            }
        }
        private void LoadWorkers()
        {
            for (int i = 50;  i < 101; i++)
            {
                var client = new Worker
                {
                    Name = $"Сотрудник {i+1}",
                    ID = i + 1,
                    Level = 2,
                    Password = i.ToString(),
                    Phone = $"892730693{i}",
                };
                client.Phone = client.Phone.Length == 10 ? client.Phone + "9" : client.Phone;
                client.Phone = client.Phone.Length == 12 ? client.Phone.Substring(0,12) : client.Phone;
                Users.Add(client);
            }
        }

        private void LoadAdmins()
        {
            for (int i = 101; i < 112; i++)
            {
                var client = new Administrator
                {
                    Name = $"Администратор {i + 1}",
                    ID = i + 1,
                    Level = 3,
                    Password = i.ToString(),
                    Phone = $"892730693{i}",
                };
                client.Phone = client.Phone.Length == 10 ? client.Phone + "9" : client.Phone;
                client.Phone = client.Phone.Length == 12 ? client.Phone.Substring(0, 11) : client.Phone;
                Users.Add(client);
            }
        }
    }
}
