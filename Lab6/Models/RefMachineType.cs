namespace Lab6.Models
{
    public class RefMachineType
    {
        public int MachineType { get; set; }
        public string MachineTypeDescription { get; set; }
        public ICollection<CustomerMachine> CustomerMachines { get; set; }
    }
}
