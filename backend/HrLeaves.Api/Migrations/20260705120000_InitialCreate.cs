using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrLeaves.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DefaultDays = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsAccrued = table.Column<bool>(type: "bit", nullable: false),
                    AccrualRatePerMonth = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    UsedDays = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalances", x => x.Id);
                    table.ForeignKey("FK_LeaveBalances_Employees_EmployeeId", x => x.EmployeeId, "Employees", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_LeaveBalances_LeaveTypes_LeaveTypeId", x => x.LeaveTypeId, "LeaveTypes", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DaysRequested = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RejectionComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey("FK_LeaveRequests_Employees_EmployeeId", x => x.EmployeeId, "Employees", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_LeaveRequests_LeaveTypes_LeaveTypeId", x => x.LeaveTypeId, "LeaveTypes", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveSettlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    AdjustmentDays = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveSettlements", x => x.Id);
                    table.ForeignKey("FK_LeaveSettlements_Employees_EmployeeId", x => x.EmployeeId, "Employees", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_LeaveSettlements_LeaveTypes_LeaveTypeId", x => x.LeaveTypeId, "LeaveTypes", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex("IX_Employees_Email", "Employees", "Email", unique: true);
            migrationBuilder.CreateIndex("IX_LeaveBalances_EmployeeId_LeaveTypeId", "LeaveBalances", new[] { "EmployeeId", "LeaveTypeId" }, unique: true);
            migrationBuilder.CreateIndex("IX_LeaveBalances_LeaveTypeId", "LeaveBalances", "LeaveTypeId");
            migrationBuilder.CreateIndex("IX_LeaveRequests_EmployeeId", "LeaveRequests", "EmployeeId");
            migrationBuilder.CreateIndex("IX_LeaveRequests_LeaveTypeId", "LeaveRequests", "LeaveTypeId");
            migrationBuilder.CreateIndex("IX_LeaveSettlements_EmployeeId", "LeaveSettlements", "EmployeeId");
            migrationBuilder.CreateIndex("IX_LeaveSettlements_LeaveTypeId", "LeaveSettlements", "LeaveTypeId");
            migrationBuilder.CreateIndex("IX_LeaveTypes_Name", "LeaveTypes", "Name", unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("LeaveBalances");
            migrationBuilder.DropTable("LeaveRequests");
            migrationBuilder.DropTable("LeaveSettlements");
            migrationBuilder.DropTable("Employees");
            migrationBuilder.DropTable("LeaveTypes");
        }
    }
}
