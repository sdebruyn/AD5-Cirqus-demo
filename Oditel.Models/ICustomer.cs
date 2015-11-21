using System;

namespace Oditel.Models
{
    public interface ICustomer
    {
        Guid? CustomerId { get; }
        string Name { get; }
        Address Address { get; }
        string Email { get; }
        void UpdateInfo(string name, string email, Address address);
        DateTimeOffset? CreatedDate { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
    }
}