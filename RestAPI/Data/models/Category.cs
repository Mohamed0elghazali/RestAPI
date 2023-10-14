﻿using System.ComponentModel.DataAnnotations;

namespace RestAPI.Data.models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public string? notes { get; set; }

        public List<Item> Items { get; set; }
    }
}
