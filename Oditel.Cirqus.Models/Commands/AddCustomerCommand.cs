using Oditel.Models;

namespace Oditel.Cirqus.Models.Commands
{
    public class AddCustomerCommand : CreateCommand<Customer>
    {
        private readonly string _name;
        private readonly string _email;
        private readonly Address _address;

        public AddCustomerCommand(string name, string email, Address address)
        {
            _name = name;
            _email = email;
            _address = address;
        }

        protected override void Update(Customer instance)
        {
            instance.UpdateInfo(_name, _email, _address);
        }
    }
}