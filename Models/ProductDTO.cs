using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceProject.Models
{
    public class ProductDTO
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Categ { get; set; }
        [Required]
        public int QuantityInDataBase { get; set; }
        [Required]
        public int QuantityInOrder { get; set; }
        public string ProductImage { get; set; }
    }
}