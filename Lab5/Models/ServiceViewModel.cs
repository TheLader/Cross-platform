namespace Lab5.Models
{
    public class ServiceViewModel
    {
        public int ServiceTypeCode { get; set; }
        public string ServiceTypeName { get; set; } // Можна витягти з RefServiceType
        public DateTime DateOfService { get; set; }
        public string DistributorName { get; set; } // Можна витягти з Distributor
        public string VendorName { get; set; } // Можна витягти з ServiceVendor
        public string OtherDetails { get; set; }
    }
}
