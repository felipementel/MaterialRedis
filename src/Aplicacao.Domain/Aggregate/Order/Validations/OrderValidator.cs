using Aplicacao.Domain.Aggregate.Customers.Validations;
using FluentValidation;

namespace Aplicacao.Domain.Aggregate.Order.Validations
{
    public class OrderValidator : AbstractValidator<Model.Order>
    {
        public OrderValidator()
        {
            RuleSet("new", () =>
            {
                RuleFor(n => n.Customer).SetValidator(new CustomerValidator());
            });
        }
    }
}