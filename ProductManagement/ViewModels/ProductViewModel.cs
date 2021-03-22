using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime Available { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }
}
