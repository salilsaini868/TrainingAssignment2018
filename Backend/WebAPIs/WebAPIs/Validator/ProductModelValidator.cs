using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIs.Models;

namespace WebAPIs.Validator
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(prop => prop.ProductName).NotEmpty();
            RuleFor(prop => prop.ProductDescription).NotEmpty();
            RuleFor(prop => prop.CategoryID).NotEmpty();
            RuleFor(prop => prop.IsActive).NotEmpty();
            RuleFor(prop => prop.Price).NotEmpty();
            RuleFor(prop => prop.Quantity).NotEmpty();
            RuleFor(prop => prop.VisibleTill).NotEmpty();
        }
    }
}
