using System;
using System.Collections.Generic;
using System.Linq;
using d60.Cirqus;
using Oditel.Cirqus.Models.Commands;
using Oditel.Cirqus.Models.Exceptions;
using Oditel.Cirqus.Views;
using Oditel.Models.CustomerContext;
using Oditel.Services;

namespace Oditel.Cirqus.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IQueryable<CustomerView> _customers;

        private readonly ICommandProcessor _processor;

        public CustomerService(ICommandProcessor processor, IQueryable<CustomerView> customers)
        {
            _customers = customers;
            _processor = processor;
        }

        private IQueryable<CustomerView> Customers
            => _customers.Where(c => c.DeletedDate == null && c.CreatedDate != null);

        public Guid AddCustomer(ICustomer customer)
        {
            var command = new AddCustomerCommand(customer);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof (ICustomer));
        }

        public Guid AddCustomer(string name, string email, Address address)
        {
            var command = new AddCustomerCommand(name, email, address);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof(ICustomer));
        }

        public IEnumerable<ICustomer> GetAllCustomers() => Customers.Select(c => c.GetCustomerFromView());

        public ICustomer GetCustomer(Guid customerId)
        {
            try
            {
                return Customers.Single(c => c.CustomerId == customerId).GetCustomerFromView();
            }
            catch (Exception)
            {
                throw new LoadingFailedException(customerId, typeof (ICustomer));
            }
        }
    }
}