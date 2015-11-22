using System;
using System.Collections.Generic;
using Oditel.Models.CustomerContext;

namespace Oditel.Services
{
    public interface ICustomerService
    {
        Guid AddCustomer(ICustomer customer);
        Guid AddCustomer(string name, string email, Address address);
        ICollection<ICustomer> GetAllCustomers();
        ICustomer GetCustomer(Guid customerId);
    }
}