using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceProject.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [MaxLength(10)]
        public string Type { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? ModifiedDate { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public bool? IsDelete { get; set; }


        public Product()
        {
            IsDelete = false;
            ModifiedDate = null;
        }
        public virtual Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}