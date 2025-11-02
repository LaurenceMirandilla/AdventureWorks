using AdventureWorks.DTO;
using FluentValidation;

namespace AdventureWorks.Validation
{
    public class ProductCategoryValidator : AbstractValidator<ProductCategoryDTO>
    {
        public ProductCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(50);
        }
    }
}
