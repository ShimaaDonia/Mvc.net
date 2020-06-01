using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsDelete { get; set; }


        public Category()
        {
            IsDelete = false;
        }
        public virtual ICollection<Product> Products { get; set; }
    }
}