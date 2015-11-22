namespace Oditel.Models.CustomerContext
{
    public sealed class Address
    {
        public enum ECountry
        {
            Belgium,
            Netherlands,
            Luxemburg
        }

        public Address(string firstLine, string secondLine, string postalCode, string city, ECountry country)
        {
            FirstLine = firstLine;
            SecondLine = secondLine;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public Address()
        {
        }

        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public ECountry Country { get; set; }
    }
}