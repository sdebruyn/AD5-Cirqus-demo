﻿using d60.Cirqus.Events;
using Oditel.Models;

namespace Oditel.Cirqus.Models.Events
{
    public class CustomerInfoUpdatedEvent : DomainEvent<Customer>
    {
        public CustomerInfoUpdatedEvent(string name, string email, Address address)
        {
            Name = name;
            Email = email;
            Address = address;
        }

        public string Name { get; }
        public string Email { get; }
        public Address Address { get; }
    }
}