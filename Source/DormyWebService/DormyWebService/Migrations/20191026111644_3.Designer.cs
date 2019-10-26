﻿// <auto-generated />
using System;
using DormyWebService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DormyWebService.Migrations
{
    [DbContext(typeof(DormyDbContext))]
    [Migration("20191026111644_3")]
    partial class _3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.Admin", b =>
                {
                    b.Property<int>("AdminId");

                    b.Property<string>("IdentityNumber")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.Staff", b =>
                {
                    b.Property<int>("StaffId");

                    b.Property<bool>("Gender");

                    b.Property<string>("HomeTown");

                    b.Property<string>("IdentityNumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("StaffId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.Student", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<decimal>("AccountBalance");

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<DateTime>("BirthDay");

                    b.Property<int>("EvaluationScore");

                    b.Property<bool>("Gender");

                    b.Property<string>("IdentityCardImageUrl");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<bool>("IsRoomLeader");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PriorityImageUrl");

                    b.Property<int>("PriorityType");

                    b.Property<int?>("RoomId");

                    b.Property<int>("StartedSchoolYear");

                    b.Property<string>("StudentCardImageUrl");

                    b.Property<string>("StudentCardNumber")
                        .IsRequired();

                    b.Property<int>("Term");

                    b.HasKey("StudentId");

                    b.HasIndex("RoomId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Role")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DormyWebService.Entities.EquipmentEntities.Equipment", b =>
                {
                    b.Property<int>("EquipmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<int?>("RoomId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("EquipmentId");

                    b.HasIndex("RoomId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.MoneyTransaction", b =>
                {
                    b.Property<int>("MoneyTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("MoneyAmount");

                    b.Property<decimal>("OriginalBalance");

                    b.Property<decimal>("ResultBalance");

                    b.Property<int?>("RoomId");

                    b.Property<int>("StudentId");

                    b.Property<int>("Type");

                    b.HasKey("MoneyTransactionId");

                    b.HasIndex("RoomId");

                    b.HasIndex("StudentId");

                    b.ToTable("MoneyTransactions");
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.PricePerUnit", b =>
                {
                    b.Property<int>("PricePerUnitId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("ElectricityPricePerUnit");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("TargetMonth");

                    b.Property<int>("TargetYear");

                    b.Property<decimal>("WaterPricePerUnit");

                    b.HasKey("PricePerUnitId");

                    b.ToTable("PricePerUnits");
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.RoomMonthlyBill", b =>
                {
                    b.Property<int>("RoomMonthlyBillId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("ElectricityBill");

                    b.Property<bool>("IsPaid");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("NewElectricityNumber");

                    b.Property<int>("NewWaterNumber");

                    b.Property<int>("PreviousElectricityNumber");

                    b.Property<int>("PreviousWaterNumber");

                    b.Property<int>("RoomId");

                    b.Property<int>("TargetMonth");

                    b.Property<int>("TargetYear");

                    b.Property<decimal>("TotalAmount");

                    b.Property<decimal>("TotalRoomFee");

                    b.Property<decimal>("WaterBill");

                    b.HasKey("RoomMonthlyBillId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomMonthlyBills");
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.StudentMonthlyBill", b =>
                {
                    b.Property<int>("StudentMonthlyBillId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsPaid");

                    b.Property<decimal>("Percentage");

                    b.Property<decimal>("RoomFee");

                    b.Property<int>("RoomId");

                    b.Property<int>("RoomMonthlyBillId");

                    b.Property<decimal>("RoomUtilityFee");

                    b.Property<int>("StudentId");

                    b.Property<DateTime>("TargetMonth");

                    b.Property<DateTime>("TargetYear");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("UtilityFee");

                    b.HasKey("StudentMonthlyBillId");

                    b.HasIndex("RoomId");

                    b.HasIndex("RoomMonthlyBillId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentMonthlyBills");
                });

            modelBuilder.Entity("DormyWebService.Entities.NewsEntities.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttachedFileUrl");

                    b.Property<int>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("NewsId");

                    b.HasIndex("AuthorId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("DormyWebService.Entities.NotificationEntities.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int?>("OwnerUserId");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("NotificationId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DormyWebService.Entities.ParamEntities.Param", b =>
                {
                    b.Property<int>("ParamId");

                    b.Property<string>("Name");

                    b.Property<int>("ParamTypeId");

                    b.Property<string>("TextValue");

                    b.Property<DateTime?>("TimeValue");

                    b.Property<int?>("Value");

                    b.HasKey("ParamId");

                    b.HasIndex("ParamTypeId");

                    b.ToTable("Params");
                });

            modelBuilder.Entity("DormyWebService.Entities.ParamEntities.ParamType", b =>
                {
                    b.Property<int>("ParamTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("ParamTypeId");

                    b.ToTable("ParamTypes");
                });

            modelBuilder.Entity("DormyWebService.Entities.RoomEntities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CurrentNumberOfStudent");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<bool>("Gender");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<string>("RoomStatus")
                        .IsRequired();

                    b.Property<int>("RoomType");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.CancelContractForm", b =>
                {
                    b.Property<int>("CancelContractFormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CancelationDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int?>("StaffId");

                    b.Property<int>("Status");

                    b.Property<int>("StudentId");

                    b.HasKey("CancelContractFormId");

                    b.HasIndex("StaffId");

                    b.HasIndex("StudentId");

                    b.ToTable("CancelContractForms");
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.ContractRenewalForm", b =>
                {
                    b.Property<int>("ContractRenewalFormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("Month");

                    b.Property<int?>("StaffId");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<int>("StudentId");

                    b.HasKey("ContractRenewalFormId");

                    b.HasIndex("StaffId");

                    b.HasIndex("StudentId");

                    b.ToTable("ContractRenewalForms");
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.IssueTicket", b =>
                {
                    b.Property<int>("IssueTicketId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int?>("EquipmentId");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("OwnerId");

                    b.Property<int?>("Point");

                    b.Property<int?>("RoomId");

                    b.Property<int?>("StaffId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Type");

                    b.HasKey("IssueTicketId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("RoomId");

                    b.HasIndex("StaffId");

                    b.ToTable("IssueTickets");
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.RoomBookingRequestForm", b =>
                {
                    b.Property<int>("RoomBookingRequestFormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Gender");

                    b.Property<string>("IdentityCardImageUrl")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("Month");

                    b.Property<string>("PriorityImageUrl")
                        .IsRequired();

                    b.Property<int>("PriorityType");

                    b.Property<string>("Reason");

                    b.Property<int?>("StaffId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("StudentCardImageUrl")
                        .IsRequired();

                    b.Property<int>("StudentId");

                    b.Property<int>("TargetRoomType");

                    b.HasKey("RoomBookingRequestFormId");

                    b.HasIndex("StaffId");

                    b.HasIndex("StudentId");

                    b.ToTable("RoomBookingRequestForm");
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.RoomTransferRequestForm", b =>
                {
                    b.Property<int>("RoomTransferRequestFormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int?>("StaffId");

                    b.Property<int>("Status");

                    b.Property<int>("StudentId");

                    b.Property<int>("TargetRoomId");

                    b.HasKey("RoomTransferRequestFormId");

                    b.HasIndex("StaffId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TargetRoomId");

                    b.ToTable("RoomTransferRequestForms");
                });

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.Admin", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.Staff", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.AccountEntities.Student", b =>
                {
                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "Room")
                        .WithMany("Students")
                        .HasForeignKey("RoomId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.EquipmentEntities.Equipment", b =>
                {
                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "Room")
                        .WithMany("Equipments")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.MoneyTransaction", b =>
                {
                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.RoomMonthlyBill", b =>
                {
                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.MoneyEntities.StudentMonthlyBill", b =>
                {
                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "Room")
                        .WithMany("StudentMonthlyBills")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DormyWebService.Entities.MoneyEntities.RoomMonthlyBill", "RoomMonthlyBill")
                        .WithMany("StudentMonthlyBill")
                        .HasForeignKey("RoomMonthlyBillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.NewsEntities.News", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.Admin", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.NotificationEntities.Notification", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserId");
                });

            modelBuilder.Entity("DormyWebService.Entities.ParamEntities.Param", b =>
                {
                    b.HasOne("DormyWebService.Entities.ParamEntities.ParamType", "ParamType")
                        .WithMany()
                        .HasForeignKey("ParamTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.CancelContractForm", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.ContractRenewalForm", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.IssueTicket", b =>
                {
                    b.HasOne("DormyWebService.Entities.EquipmentEntities.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.RoomBookingRequestForm", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.TicketEntities.RoomTransferRequestForm", b =>
                {
                    b.HasOne("DormyWebService.Entities.AccountEntities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.HasOne("DormyWebService.Entities.AccountEntities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DormyWebService.Entities.RoomEntities.Room", "TargetRoom")
                        .WithMany()
                        .HasForeignKey("TargetRoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}