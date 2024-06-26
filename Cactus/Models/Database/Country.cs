﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("country")]
    public class Country
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(64)")]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
