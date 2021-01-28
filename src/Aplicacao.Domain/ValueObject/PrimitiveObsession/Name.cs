namespace Aplicacao.Domain.ValueObject.PrimitiveObsession
{
    public class Name : Domain.Shared.ValueObject
    {
        public Name(string firstName, string surename)
        {
            FirstName = firstName;
            Surename = surename;
        }

        public string FirstName { get; private set; }

        public string Surename { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {Surename}";
        }
    }
}