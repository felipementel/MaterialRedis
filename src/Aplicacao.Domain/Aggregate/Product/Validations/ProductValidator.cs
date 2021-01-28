using Aplicacao.Domain.Aggregate.Product.Model;
using FluentValidation;

namespace Aplicacao.Domain.Aggregate.Product.Validations
{
    public class ProductValidator : AbstractValidator<Model.Product>
    {
        public ProductValidator()
        {
            RuleSet("new", () =>
            {
                RuleFor(n => n.Price).NotEmpty().WithMessage("{{PropertyName}} não pode ser nulo.");
            });           
        }
    }
}