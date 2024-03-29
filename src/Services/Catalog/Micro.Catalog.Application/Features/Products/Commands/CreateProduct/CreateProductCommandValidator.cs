using FluentValidation;

namespace Micro.Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(255)
            .NotEmpty();
        
        RuleFor(v => v.Description)
            .MaximumLength(511)
            .NotEmpty();
        
        RuleFor(v => v.Price)
            .GreaterThanOrEqualTo(0)
            .NotEmpty();
    }
}