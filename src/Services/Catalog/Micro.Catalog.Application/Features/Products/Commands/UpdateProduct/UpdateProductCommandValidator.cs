using FluentValidation;
using Micro.Catalog.Application.Features.Products.Commands.CreateProduct;

namespace Micro.Catalog.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
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