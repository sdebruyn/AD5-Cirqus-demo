using Oditel.Models;

namespace Oditel.Cirqus.Models.Commands
{
    public class AddCustomerCommand : CreateCommand<Customer>
    {
        private readonly Address _address;
        private readonly string _email;
        private readonly string _name;

        public AddCustomerCommand(string name, string email, Address address)
        {
            _name = name;
            _email = email;
            _address = address;
        }

        public AddCustomerCommand(ICustomer customer): this(customer.Name, customer.Email, customer.Address)
        { }

        protected override void Update(Customer instance)
        {
            instance.UpdateInfo(_name, _email, _address);
        }
    }
}