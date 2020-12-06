using Aplicacao.Domain.Shared.Enums;

namespace Aplicacao.Domain.ValueObject
{
    public class Document : Domain.Shared.ValueObject
    {
        public Document(int number)
        {
            Number = number;
            Type = Validate();
        }

        public int Number { get; private set; }

        public EDocumentType Type { get; private set; }

        private EDocumentType Validate()
        {
            if (Number.ToString().Length == 14)
                return EDocumentType.CNPJ;
            else
                return EDocumentType.CPF;
        }
    }
}
