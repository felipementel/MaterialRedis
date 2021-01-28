using Aplicacao.Domain.Shared.Model;
using Aplicacao.Domain.ValueObject.Address;
using System;
using System.Collections.Generic;

namespace Aplicacao.Domain.Aggregate.Customer.Model
{
    public class Customer : TEntity<int>
    {
        //TODO: Testar como protected
        public Customer()
        {
           
        }

        public Customer(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            RegistrationDate = DateTime.Now;
        }

        public string Name { get; set; } // ValueObject

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        //public string Document { get; private set; } // ValueObject

        public virtual ICollection<Address> Address { get; set; } // ValueObject
    }
}