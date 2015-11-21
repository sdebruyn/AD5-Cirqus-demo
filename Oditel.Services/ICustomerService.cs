using System;
using System.Collections.Generic;
using Oditel.Models;

namespace Oditel.Services
{
    public interface ICustomerService
    {
        Guid AddCustomer(ICustomer customer);
        IEnumerable<ICustomer> GetAllCustomers();
        ICustomer GetCustomer(Guid customerId);
    }
}