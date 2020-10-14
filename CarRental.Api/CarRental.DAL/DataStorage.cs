using CarRental.Identity.Entities;
using System.Collections.Generic;


namespace CarRental.DAL
{
    public class DataStorage
    {
        public List<User> Users { get; set; }

        public List<string> Values { get; set; } 

        public DataStorage()
        {
            Users = new List<User>
            {
                //new User {Email = "email1@mail.ru", Name = "Tanya"}
               /*new User {Email = "email2@mail.ru", Password = "2222", Name = "Ksusha", Role = "user"},
                new User {Email = "email3@mail.ru", Password = "3333", Name = "Maxim", Role = "admin"},
                new User {Email = "email4@mail.ru", Password = "4444", Name = "Nikita", Role = "admin"}*/
            };

            Values = new List<string>(new[] { "value1", "value2", "value3", "value4" });
        }
    }
}
