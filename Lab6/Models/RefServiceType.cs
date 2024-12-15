namespace Lab6.Models
{
    public class RefServiceType
    {
        public int ServiceTypeCode { get; set; }
        public string ServiceTypeDescription { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
