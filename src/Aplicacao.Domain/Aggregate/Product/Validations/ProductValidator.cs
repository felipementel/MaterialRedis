using Aplicacao.Domain.Model;
using FluentValidation;

namespace Aplicacao.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            //RuleSet("new", () =>
            //{
            //    RuleFor(n => n.Price).NotEmpty().WithMessage("{{PropertyName}} não pode ser nulo.");
            //});

            RuleFor(n => n.Price).NotEmpty().WithMessage("{{PropertyName}} não pode ser nulo.");
        }
    }
}
