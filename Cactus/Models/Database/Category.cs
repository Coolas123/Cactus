﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("category")]
    public class Category
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name",TypeName ="varchar(64)")]
        public string Name { get; set; }
    }
}
