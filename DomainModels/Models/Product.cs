using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z ]{2,20}$", ErrorMessage = "Enter valid Product Name ")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]+.+-+[0-9]+", ErrorMessage = "Enter valid Product Code")]
        public string Code { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString ="{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Available { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
