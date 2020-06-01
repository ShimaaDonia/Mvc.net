using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceProject.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [MaxLength(200)]
        [Required]
        public string Body { get; set; }
        [ForeignKey("Product")]

        public int ProductID { get; set; }
        public bool? IsDelete { get; set; }
        
        public virtual Product Product { get; set; }
        public Comment()
        {
            IsDelete = false;
        }
    }
}