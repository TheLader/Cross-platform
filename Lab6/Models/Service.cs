using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Service
    {
        [Key]
        public int ServiceTypeCode { get; set; }
        public int DistributorId { get; set; }
        public int ServiceVendorId { get; set; }
        public DateTime DateOfService { get; set; }
        public string OtherServiceDetails { get; set; }

        public RefServiceType ServiceType { get; set; }
        public Distributor Distributor { get; set; }
        public ServiceVendor ServiceVendor { get; set; }
    }
}
