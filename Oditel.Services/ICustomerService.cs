using System;
using Oditel.Models;

namespace Oditel.Services
{
    public interface ICustomerService
    {
        Guid AddCustomer(ICustomer customer);
    }
}