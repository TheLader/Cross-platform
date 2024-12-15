using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "EndUser",
                columns: table => new
                {
                    EndUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndUserDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUser", x => x.EndUserId);
                });

            migrationBuilder.CreateTable(
                name: "RefAddressType",
                columns: table => new
                {
                    AddressTypeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAddressType", x => x.AddressTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "RefCustomerTypes",
                columns: table => new
                {
                    CustomerTypeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCustomerTypes", x => x.CustomerTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "RefMachineTypes",
                columns: table => new
                {
                    MachineType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefMachineTypes", x => x.MachineType);
                });

            migrationBuilder.CreateTable(
                name: "RefServiceTypes",
                columns: table => new
                {
                    ServiceTypeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefServiceTypes", x => x.ServiceTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "ServiceVendors",
                columns: table => new
                {
                    ServiceVendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceVendorDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceVendors", x => x.ServiceVendorId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AddressTypeCode = table.Column<int>(type: "int", nullable: false),
                    AddressDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_RefAddressType_AddressTypeCode",
                        column: x => x.AddressTypeCode,
                        principalTable: "RefAddressType",
                        principalColumn: "AddressTypeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CustomerTypeCode = table.Column<int>(type: "int", nullable: true),
                    EndUserId = table.Column<int>(type: "int", nullable: true),
                    CustomerDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_EndUser_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUser",
                        principalColumn: "EndUserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Customers_RefCustomerTypes_CustomerTypeCode",
                        column: x => x.CustomerTypeCode,
                        principalTable: "RefCustomerTypes",
                        principalColumn: "CustomerTypeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    DistributorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceVendorId = table.Column<int>(type: "int", nullable: false),
                    DistributorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherDistributorDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.DistributorId);
                    table.ForeignKey(
                        name: "FK_Distributors_ServiceVendors_ServiceVendorId",
                        column: x => x.ServiceVendorId,
                        principalTable: "ServiceVendors",
                        principalColumn: "ServiceVendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMachines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    MachineType = table.Column<int>(type: "int", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallationLocationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineOtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMachines", x => x.MachineId);
                    table.ForeignKey(
                        name: "FK_CustomerMachines_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerMachines_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "DistributorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerMachines_RefMachineTypes_MachineType",
                        column: x => x.MachineType,
                        principalTable: "RefMachineTypes",
                        principalColumn: "MachineType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceTypeCode = table.Column<int>(type: "int", nullable: false),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    ServiceVendorId = table.Column<int>(type: "int", nullable: false),
                    DateOfService = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtherServiceDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceTypeCode);
                    table.ForeignKey(
                        name: "FK_Services_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "DistributorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_RefServiceTypes_ServiceTypeCode",
                        column: x => x.ServiceTypeCode,
                        principalTable: "RefServiceTypes",
                        principalColumn: "ServiceTypeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ServiceVendors_ServiceVendorId",
                        column: x => x.ServiceVendorId,
                        principalTable: "ServiceVendors",
                        principalColumn: "ServiceVendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "CompanyDetails", "CompanyName" },
                values: new object[,]
                {
                    { 1, "Leading tech solutions", "TechCorp" },
                    { 2, "Software development specialists", "SoftSolutions" }
                });

            migrationBuilder.InsertData(
                table: "EndUser",
                columns: new[] { "EndUserId", "EndUserDetails" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "RefAddressType",
                columns: new[] { "AddressTypeCode", "AddressTypeDescription" },
                values: new object[,]
                {
                    { 1, "Home" },
                    { 2, "Office" }
                });

            migrationBuilder.InsertData(
                table: "RefCustomerTypes",
                columns: new[] { "CustomerTypeCode", "CustomerTypeDescription" },
                values: new object[,]
                {
                    { 1, "Regular" },
                    { 2, "Premium" }
                });

            migrationBuilder.InsertData(
                table: "RefMachineTypes",
                columns: new[] { "MachineType", "MachineTypeDescription" },
                values: new object[,]
                {
                    { 1, "Printer" },
                    { 2, "Scanner" }
                });

            migrationBuilder.InsertData(
                table: "RefServiceTypes",
                columns: new[] { "ServiceTypeCode", "ServiceTypeDescription" },
                values: new object[,]
                {
                    { 1, "Maintenance" },
                    { 2, "Repair" }
                });

            migrationBuilder.InsertData(
                table: "ServiceVendors",
                columns: new[] { "ServiceVendorId", "ServiceVendorDetails" },
                values: new object[,]
                {
                    { 1, "FastTech Service" },
                    { 2, "GlobalTech Service" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "AddressDetails", "AddressTypeCode", "CompanyId" },
                values: new object[,]
                {
                    { 1, "123 Tech Street", 1, 1 },
                    { 2, "456 Business Ave", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CompanyId", "CustomerDetails", "CustomerTypeCode", "EndUserId" },
                values: new object[,]
                {
                    { 1, 1, "Regular customer details for TechCorp", 1, 1 },
                    { 2, 2, "Premium customer details for SoftSolutions", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Distributors",
                columns: new[] { "DistributorId", "DistributorName", "OtherDistributorDetails", "ServiceVendorId" },
                values: new object[,]
                {
                    { 1, "Tech Distributors", "Expert in tech distribution", 1 },
                    { 2, "Global Distributors", "Worldwide distribution network", 2 }
                });

            migrationBuilder.InsertData(
                table: "CustomerMachines",
                columns: new[] { "MachineId", "CustomerId", "DistributorId", "InstallationLocationAddress", "MachineDescription", "MachineName", "MachineOtherDetails", "MachineType" },
                values: new object[,]
                {
                    { 1, 1, 1, "123 Tech Street", "High-speed office printer", "Office Printer", "Requires monthly maintenance", 1 },
                    { 2, 2, 2, "456 Business Ave", "High-resolution scanner", "Office Scanner", "Requires quarterly maintenance", 2 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceTypeCode", "DateOfService", "DistributorId", "OtherServiceDetails", "ServiceVendorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Routine maintenance", 1 },
                    { 2, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Urgent repair", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressTypeCode",
                table: "Addresses",
                column: "AddressTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CompanyId",
                table: "Addresses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMachines_CustomerId",
                table: "CustomerMachines",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMachines_DistributorId",
                table: "CustomerMachines",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMachines_MachineType",
                table: "CustomerMachines",
                column: "MachineType");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeCode",
                table: "Customers",
                column: "CustomerTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EndUserId",
                table: "Customers",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_ServiceVendorId",
                table: "Distributors",
                column: "ServiceVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DistributorId",
                table: "Services",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceVendorId",
                table: "Services",
                column: "ServiceVendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CustomerMachines");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "RefAddressType");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "RefMachineTypes");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "RefServiceTypes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "EndUser");

            migrationBuilder.DropTable(
                name: "RefCustomerTypes");

            migrationBuilder.DropTable(
                name: "ServiceVendors");
        }
    }
}
