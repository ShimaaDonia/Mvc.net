using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceProject.Models
{
    public class Order  //Shipping Details
    {
        public int ID { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfOrder { get; set; }
        public string Address { get; set; }
      
        [MaxLength(10)]
        public string ZipCode { get; set; }
        public double AmountPaid { get; set; }
        public bool? IsDelete { get; set; }
        public int? OrderId { get; set; }
        public string PaymentType { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
        
        public Order()
        {
            IsDelete = false;
        }
    }
}