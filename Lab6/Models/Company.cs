namespace Lab6.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDetails { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
