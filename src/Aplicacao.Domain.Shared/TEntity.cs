using FluentValidation.Results;

namespace Aplicacao.Domain.Shared.Model
{
    public abstract class TEntity<Tid>
    {
        protected TEntity()
        {
            ValidationResult = new ValidationResult();
        }

        public Tid Id { get; set; }

        public ValidationResult ValidationResult { get; set; }
    }
}