using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.Models
{
    public class CategoryModel
    {
        [Key]
        [BindNever]
        public int CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryDescription { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        [BindNever]
        public int CreatedBy { get; set; }
        [Required]
        [BindNever]
        public DateTime CreatedDate { get; set; }
        [BindNever]
        public Nullable<int> ModifiedBy { get; set; }
        [BindNever]
        public Nullable<DateTime> ModifiedDate { get; set; }
    }    
}
