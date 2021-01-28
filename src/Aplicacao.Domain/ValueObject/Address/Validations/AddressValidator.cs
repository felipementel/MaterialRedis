using Aplicacao.Domain.ValueObject.Address;
using FluentValidation;

namespace Aplicacao.Domain.ValueObject.Address.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(n => n.Country).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(n => n.State).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(n => n.City).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(n => n.Neighborhood).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(n => n.Street).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            //RuleFor(n => n.Complement).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(n => n.Number).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(n => n.ZipCode).NotEmpty().WithMessage("{PropertyName} não pode ser nulo.");
        }
    }
}