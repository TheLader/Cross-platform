using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class CustomerMachine
    {
        [Key]
        public int MachineId { get; set; }
        public int CustomerId { get; set; }
        public int DistributorId { get; set; }
        public int MachineType { get; set; }
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public string InstallationLocationAddress { get; set; }
        public string MachineOtherDetails { get; set; }

        public Customer Customer { get; set; }
        public Distributor Distributor { get; set; }
        public RefMachineType MachineTypeNavigation { get; set; }
    }
}
