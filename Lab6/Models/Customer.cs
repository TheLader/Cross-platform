using System.Text.Json.Serialization;

namespace Lab6.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int? CompanyId { get; set; }
        public int? CustomerTypeCode { get; set; }
        public int? EndUserId { get; set; }
        public string CustomerDetails { get; set; }

        public RefCustomerType? CustomerType { get; set; }
        public EndUser? EndUser { get; set; }
        public Company? Company { get; set; }
        public ICollection<CustomerMachine>? CustomerMachines { get; set; }
    }
}
