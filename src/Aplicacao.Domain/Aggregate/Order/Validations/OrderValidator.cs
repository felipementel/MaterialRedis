using Aplicacao.Domain.Model;
using FluentValidation;

namespace Aplicacao.Domain.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleSet("new", () =>
            {
                RuleFor(n => n.Customer).SetValidator(new CustomerValidator());
            });

            //RuleFor(n => n.Customer).SetValidator(new CustomerValidator());
        }
    }
}
