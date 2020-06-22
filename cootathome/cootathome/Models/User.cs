using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;

namespace cootathome.Models
{
    public class User
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Unique]
        public string Email { get; set; }
        
        [Unique]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
