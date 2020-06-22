using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace cootathome.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }
    }
}
