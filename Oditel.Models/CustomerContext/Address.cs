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

        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public ECountry Country { get; set; }
    }
}