using System;
using HrLeaves.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HrLeaves.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HrLeaves.Api.Models.Employee", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("Email").IsRequired().HasColumnType("nvarchar(450)");
                b.Property<string>("FullName").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<DateOnly>("HireDate").HasColumnType("date");
                b.HasKey("Id");
                b.HasIndex("Email").IsUnique();
                b.ToTable("Employees");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveType", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<decimal>("AccrualRatePerMonth").HasPrecision(10, 2).HasColumnType("decimal(10,2)");
                b.Property<decimal>("DefaultDays").HasPrecision(10, 2).HasColumnType("decimal(10,2)");
                b.Property<bool>("IsAccrued").HasColumnType("bit");
                b.Property<string>("Name").IsRequired().HasColumnType("nvarchar(450)");
                b.HasKey("Id");
                b.HasIndex("Name").IsUnique();
                b.ToTable("LeaveTypes");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveBalance", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<int>("EmployeeId").HasColumnType("int");
                b.Property<int>("LeaveTypeId").HasColumnType("int");
                b.Property<decimal>("TotalDays").HasPrecision(10, 2).HasColumnType("decimal(10,2)");
                b.Property<decimal>("UsedDays").HasPrecision(10, 2).HasColumnType("decimal(10,2)");
                b.HasKey("Id");
                b.HasIndex("LeaveTypeId");
                b.HasIndex("EmployeeId", "LeaveTypeId").IsUnique();
                b.ToTable("LeaveBalances");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveRequest", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
                b.Property<decimal>("DaysRequested").HasPrecision(10, 2).HasColumnType("decimal(10,2)");
                b.Property<DateOnly>("EndDate").HasColumnType("date");
                b.Property<int>("EmployeeId").HasColumnType("int");
                b.Property<int>("LeaveTypeId").HasColumnType("int");
                b.Property<string>("Reason").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("RejectionComment").HasColumnType("nvarchar(max)");
                b.Property<DateOnly>("StartDate").HasColumnType("date");
                b.Property<int>("Status").HasColumnType("int");
                b.HasKey("Id");
                b.HasIndex("EmployeeId");
                b.HasIndex("LeaveTypeId");
                b.ToTable("LeaveRequests");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveSettlement", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<decimal>("AdjustmentDays").HasPrecision(10, 2).HasColumnType("decimal(10,2)");
                b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
                b.Property<int>("EmployeeId").HasColumnType("int");
                b.Property<int>("LeaveTypeId").HasColumnType("int");
                b.Property<string>("Reason").IsRequired().HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.HasIndex("EmployeeId");
                b.HasIndex("LeaveTypeId");
                b.ToTable("LeaveSettlements");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveBalance", b =>
            {
                b.HasOne("HrLeaves.Api.Models.Employee", "Employee").WithMany("LeaveBalances").HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.HasOne("HrLeaves.Api.Models.LeaveType", "LeaveType").WithMany("LeaveBalances").HasForeignKey("LeaveTypeId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.Navigation("Employee");
                b.Navigation("LeaveType");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveRequest", b =>
            {
                b.HasOne("HrLeaves.Api.Models.Employee", "Employee").WithMany("LeaveRequests").HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.HasOne("HrLeaves.Api.Models.LeaveType", "LeaveType").WithMany("LeaveRequests").HasForeignKey("LeaveTypeId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.Navigation("Employee");
                b.Navigation("LeaveType");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveSettlement", b =>
            {
                b.HasOne("HrLeaves.Api.Models.Employee", "Employee").WithMany().HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.HasOne("HrLeaves.Api.Models.LeaveType", "LeaveType").WithMany().HasForeignKey("LeaveTypeId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.Navigation("Employee");
                b.Navigation("LeaveType");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.Employee", b =>
            {
                b.Navigation("LeaveBalances");
                b.Navigation("LeaveRequests");
            });

            modelBuilder.Entity("HrLeaves.Api.Models.LeaveType", b =>
            {
                b.Navigation("LeaveBalances");
                b.Navigation("LeaveRequests");
            });
#pragma warning restore 612, 618
        }
    }
}
