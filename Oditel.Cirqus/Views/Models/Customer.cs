using System;
using Oditel.Models;

namespace Oditel.Cirqus.Views.Models
{
    public class Customer : ICustomer
    {
        public Customer(Guid id)
        {
            CustomerId = id;
        }

        public Guid? CustomerId { get; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string Email { get; private set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }

        public void UpdateInfo(string name, string email, Address address)
        {
            Name = name;
            Email = email;
            Address = address;
        }
    }
}