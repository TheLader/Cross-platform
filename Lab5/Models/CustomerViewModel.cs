namespace Lab5.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerDetails { get; set; }
        public string CompanyName { get; set; } // Можна витягти з Company
        public string CustomerTypeName { get; set; } // Можна витягти з RefCustomerType
    }
}
