namespace Lab6.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int CompanyId { get; set; }
        public int AddressTypeCode { get; set; }
        public string AddressDetails { get; set; }

        public Company Company { get; set; }
        public RefAddressType AddressType { get; set; }
    }
}
