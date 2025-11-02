using AdventureWorks.DTO;
using FluentValidation;

namespace AdventureWorks.Validation
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100);

            RuleFor(p => p.ProductNumber)
                .NotEmpty().WithMessage("Product number is required.");

            RuleFor(p => p.ListPrice)
                .GreaterThanOrEqualTo(0).WithMessage("List price cannot be negative.");

            RuleFor(p => p.StandardCost)
                .GreaterThanOrEqualTo(0).WithMessage("Standard cost cannot be negative.");
        }
    }
}
