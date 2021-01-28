using Aplicacao.Domain.Aggregate.Customer.Model;
using Aplicacao.Domain.Shared.Model;

namespace Aplicacao.Domain.ValueObject.Address
{
    public class Address : TEntity<int>
    {
        public Address(string country, string state, string city, string neighborhood, string street, string number, string complement, string zipCode)
        {
            Country = country;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            Street = street;
            Number = number;
            Complement = complement;
            ZipCode = zipCode;
        }

        public virtual Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Street { get; private set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string ZipCode { get; set; }
    }
}