namespace Lab6.Models
{
    public class RefCustomerType
    {
        public int CustomerTypeCode { get; set; }
        public string CustomerTypeDescription { get; set; }
        public ICollection<Customer>? Customers { get; set; } = new List<Customer>();
    }
}
