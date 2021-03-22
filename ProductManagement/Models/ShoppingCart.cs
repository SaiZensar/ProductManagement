using DomainModels;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductManagement.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = Count + 1;
        }


        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual UserRegister UserRegister { get; set; }

        public int ProductId { get; set; }

        [NotMapped]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than or equal to {1}")]
        public int Count { get; set; }
    }
}
