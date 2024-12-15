using Lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.DbUtils
{
    public class ProductServicingContext : DbContext
    {
        public ProductServicingContext(DbContextOptions<ProductServicingContext> options) : base(options) { }

        public DbSet<RefCustomerType> RefCustomerTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RefMachineType> RefMachineTypes { get; set; }
        public DbSet<CustomerMachine> CustomerMachines { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<ServiceVendor> ServiceVendors { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RefServiceType> RefServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Company)
                .WithMany(co => co.Customers)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomerType)
                .WithMany(rt => rt.Customers)
                .HasForeignKey(c => c.CustomerTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.EndUser)
                .WithMany(eu => eu.Customers)
                .HasForeignKey(c => c.EndUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Company)
                .WithMany(co => co.Addresses)
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.AddressType)
                .WithMany(at => at.Addresses)
                .HasForeignKey(a => a.AddressTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerMachine>()
                .HasOne(cm => cm.Customer)
                .WithMany(c => c.CustomerMachines)
                .HasForeignKey(cm => cm.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerMachine>()
                .HasOne(cm => cm.Distributor)
                .WithMany(d => d.CustomerMachines)
                .HasForeignKey(cm => cm.DistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerMachine>()
                .HasOne(cm => cm.MachineTypeNavigation)
                .WithMany(mt => mt.CustomerMachines)
                .HasForeignKey(cm => cm.MachineType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distributor>()
                .HasOne(d => d.ServiceVendor)
                .WithMany(sv => sv.Distributors)
                .HasForeignKey(d => d.ServiceVendorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceType)
                .WithMany(st => st.Services)
                .HasForeignKey(s => s.ServiceTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceVendor)
                .WithMany(sv => sv.Services)
                .HasForeignKey(s => s.ServiceVendorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefCustomerType>()
                .HasKey(rt => rt.CustomerTypeCode);

            modelBuilder.Entity<RefAddressType>()
                .HasKey(at => at.AddressTypeCode);

            modelBuilder.Entity<RefMachineType>()
                .HasKey(mt => mt.MachineType);

            modelBuilder.Entity<RefServiceType>()
                .HasKey(st => st.ServiceTypeCode);

            modelBuilder.Entity<RefCustomerType>().HasData(
                new RefCustomerType { CustomerTypeCode = 1, CustomerTypeDescription = "Regular" },
                new RefCustomerType { CustomerTypeCode = 2, CustomerTypeDescription = "Premium" }
            );

            modelBuilder.Entity<RefAddressType>().HasData(
                new RefAddressType { AddressTypeCode = 1, AddressTypeDescription = "Home" },
                new RefAddressType { AddressTypeCode = 2, AddressTypeDescription = "Office" }
            );

            modelBuilder.Entity<RefMachineType>().HasData(
                new RefMachineType { MachineType = 1, MachineTypeDescription = "Printer" },
                new RefMachineType { MachineType = 2, MachineTypeDescription = "Scanner" }
            );

            modelBuilder.Entity<RefServiceType>().HasData(
                new RefServiceType { ServiceTypeCode = 1, ServiceTypeDescription = "Maintenance" },
                new RefServiceType { ServiceTypeCode = 2, ServiceTypeDescription = "Repair" }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "TechCorp", CompanyDetails = "Leading tech solutions" },
                new Company { CompanyId = 2, CompanyName = "SoftSolutions", CompanyDetails = "Software development specialists" }
            );

            modelBuilder.Entity<EndUser>().HasData(
                new EndUser { EndUserId = 1, EndUserDetails = "John Doe" },
                new EndUser { EndUserId = 2, EndUserDetails = "Jane Smith" }
            );

            modelBuilder.Entity<Distributor>().HasData(
                new Distributor { DistributorId = 1, ServiceVendorId = 1, DistributorName = "Tech Distributors", OtherDistributorDetails = "Expert in tech distribution" },
                new Distributor { DistributorId = 2, ServiceVendorId = 2, DistributorName = "Global Distributors", OtherDistributorDetails = "Worldwide distribution network" }
            );

            modelBuilder.Entity<ServiceVendor>().HasData(
                new ServiceVendor { ServiceVendorId = 1, ServiceVendorDetails = "FastTech Service" },
                new ServiceVendor { ServiceVendorId = 2, ServiceVendorDetails = "GlobalTech Service" }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1, CompanyId = 1, AddressTypeCode = 1, AddressDetails = "123 Tech Street" },
                new Address { AddressId = 2, CompanyId = 2, AddressTypeCode = 2, AddressDetails = "456 Business Ave" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CompanyId = 1,
                    CustomerTypeCode = 1,
                    EndUserId = 1,
                    CustomerDetails = "Regular customer details for TechCorp"
                },
                new Customer
                {
                    CustomerId = 2,
                    CompanyId = 2,
                    CustomerTypeCode = 2,
                    EndUserId = 2,
                    CustomerDetails = "Premium customer details for SoftSolutions"
                }
            );


            modelBuilder.Entity<CustomerMachine>().HasData(
                new CustomerMachine
                {
                    MachineId = 1,
                    CustomerId = 1,
                    DistributorId = 1,
                    MachineType = 1,
                    MachineName = "Office Printer",
                    MachineDescription = "High-speed office printer",
                    InstallationLocationAddress = "123 Tech Street",
                    MachineOtherDetails = "Requires monthly maintenance"
                },
                new CustomerMachine
                {
                    MachineId = 2,
                    CustomerId = 2,
                    DistributorId = 2,
                    MachineType = 2,
                    MachineName = "Office Scanner",
                    MachineDescription = "High-resolution scanner",
                    InstallationLocationAddress = "456 Business Ave",
                    MachineOtherDetails = "Requires quarterly maintenance"
                }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    ServiceTypeCode = 1,
                    DistributorId = 1,
                    ServiceVendorId = 1,
                    DateOfService = new DateTime(2023, 10, 1),
                    OtherServiceDetails = "Routine maintenance"
                },
                new Service
                {
                    ServiceTypeCode = 2,
                    DistributorId = 2,
                    ServiceVendorId = 2,
                    DateOfService = new DateTime(2023, 11, 15),
                    OtherServiceDetails = "Urgent repair"
                }
            );

        }
    }
}
