using Aplicacao.Domain.ValueObject.Address.Validations;
using FluentValidation;

namespace Aplicacao.Domain.Aggregate.Customers.Validations
{
    public class CustomerValidator : AbstractValidator<Customer.Model.Customer>
    {
        public CustomerValidator()
        {
            RuleSet("new", () =>
            {
                RuleFor(n => n.Name).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
                RuleFor(n => n.BirthDate).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
                RuleFor(n => n.BirthDate).NotEmpty().LessThan(System.DateTime.Now).WithMessage("{PropertyName} não pode ser maior que a data atual");
                RuleFor(n => n.Email).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
                RuleForEach(a => a.Address).SetValidator(new AddressValidator());
            });
        }
    }
}