using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
