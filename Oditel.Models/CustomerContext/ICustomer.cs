using System;

namespace Oditel.Models.CustomerContext
{
    public interface ICustomer
    {
        Guid? CustomerId { get; }
        string Name { get; }
        Address Address { get; }
        string Email { get; }
        DateTimeOffset? CreatedDate { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        void UpdateInfo(string name, string email, Address address);
    }
}