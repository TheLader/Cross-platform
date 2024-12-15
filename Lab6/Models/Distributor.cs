namespace Lab6.Models
{
    public class Distributor
    {
        public int DistributorId { get; set; }
        public int ServiceVendorId { get; set; }
        public string DistributorName { get; set; }
        public string OtherDistributorDetails { get; set; }

        public ServiceVendor ServiceVendor { get; set; }
        public ICollection<CustomerMachine> CustomerMachines { get; set; }
    }
}
