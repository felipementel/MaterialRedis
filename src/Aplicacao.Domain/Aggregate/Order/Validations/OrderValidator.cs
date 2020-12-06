using Aplicacao.Domain.Model;
using FluentValidation;

namespace Aplicacao.Domain.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(n => n.Customer).SetValidator(new CustomerValidator());
        }
    }
}
