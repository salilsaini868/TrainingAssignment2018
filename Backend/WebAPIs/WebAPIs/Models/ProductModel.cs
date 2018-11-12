using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.Models
{
    public class ProductModel
    {
        [Key]
        [BindNever]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime VisibleTill { get; set; }

        [BindNever]
        //[Required]
        public int CreatedBy { get; set; }

        [BindNever]
        //[Required]
        public DateTime CreatedDate { get; set; }

        [BindNever]
        public Nullable<int> ModifiedBy { get; set; }

        [BindNever]
        public Nullable<DateTime> ModifiedDate { get; set; }
    }

}
