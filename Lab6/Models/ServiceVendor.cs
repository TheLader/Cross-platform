namespace Lab6.Models
{
    public class ServiceVendor
    {
        public int ServiceVendorId { get; set; }
        public string ServiceVendorDetails { get; set; }
        public ICollection<Distributor> Distributors { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
