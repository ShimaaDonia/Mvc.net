using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceProject.Models
{
    public class Cart //OrderProduct
    {
        public int ID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public int QuantityInOrder { get; set; }
        [ForeignKey("Order")]
        
        public int OrderID { get; set; }
        public string UserID { get; set; }
          
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}