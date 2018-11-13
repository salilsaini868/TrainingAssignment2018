using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIs.Models
{
    public class ProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(prop => prop.ProductName).NotNull()
                                             .Length(3, 20)
                                             .WithMessage("Product Name should be greater than 3 characters");

            RuleFor(prop => prop.ProductDescription).NotNull()
                                             .WithMessage("Description Can't be empty");

            RuleFor(prop => prop.CategoryID).NotNull()
                                             .GreaterThanOrEqualTo(1)
                                             .WithMessage("Category ID Can't be empty");

            RuleFor(prop => prop.IsActive).NotEmpty()
                                             .Must(prop => prop == false || prop == true)
                                             .WithMessage("IsActive Can't be empty"); 
            
            RuleFor(prop => prop.Price).NotNull()
                                             .GreaterThanOrEqualTo(1)
                                             .WithMessage("Price Can't be empty");

            RuleFor(prop => prop.Quantity).NotNull()
                                             .GreaterThanOrEqualTo(1)
                                             .WithMessage("Quantity Can't be empty");

            RuleFor(prop => prop.VisibleTill).NotNull()
                                             .GreaterThan(DateTime.Today)
                                             .WithMessage("You cannot enter a  date from the past");
                                             
        }
    }   
}