using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace cootathome.Models
{
    public class Categories
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public string CategoryName { get; set; }

        public string ImageURL { get; set; }

    }
}
