﻿// <auto-generated />
using System;
using CoJourney.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoJourney.DAL.Migrations
{
    [DbContext(typeof(CoJourneyDbContext))]
    [Migration("20220409133430_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoJourney.DAL.Entities.CarEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("FirstRegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.CarEventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.InvitationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Accepted")
                        .HasColumnType("bit");

                    b.Property<Guid>("JourneyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReceiverUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("JourneyId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.JourneyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CarEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StartLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarEventId");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.ToTable("Journeys");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JourneyEntityUserEntity", b =>
                {
                    b.Property<Guid>("CoRidersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoRidingJourneysId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CoRidersId", "CoRidingJourneysId");

                    b.HasIndex("CoRidingJourneysId");

                    b.ToTable("JourneyEntityUserEntity");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.CarEntity", b =>
                {
                    b.HasOne("CoJourney.DAL.Entities.UserEntity", "Owner")
                        .WithMany("OwnedCars")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.InvitationEntity", b =>
                {
                    b.HasOne("CoJourney.DAL.Entities.JourneyEntity", "Journey")
                        .WithMany("Invitation")
                        .HasForeignKey("JourneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoJourney.DAL.Entities.UserEntity", "ReceiverUser")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("ReceiverUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoJourney.DAL.Entities.UserEntity", "SenderUser")
                        .WithMany("SentInvitations")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Journey");

                    b.Navigation("ReceiverUser");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.JourneyEntity", b =>
                {
                    b.HasOne("CoJourney.DAL.Entities.CarEventEntity", "CarEvent")
                        .WithMany("Journeys")
                        .HasForeignKey("CarEventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoJourney.DAL.Entities.CarEntity", "Car")
                        .WithMany("Journeys")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoJourney.DAL.Entities.UserEntity", "Driver")
                        .WithMany("DrivingJourneys")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("CarEvent");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("JourneyEntityUserEntity", b =>
                {
                    b.HasOne("CoJourney.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("CoRidersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoJourney.DAL.Entities.JourneyEntity", null)
                        .WithMany()
                        .HasForeignKey("CoRidingJourneysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.CarEntity", b =>
                {
                    b.Navigation("Journeys");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.CarEventEntity", b =>
                {
                    b.Navigation("Journeys");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.JourneyEntity", b =>
                {
                    b.Navigation("Invitation");
                });

            modelBuilder.Entity("CoJourney.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("DrivingJourneys");

                    b.Navigation("OwnedCars");

                    b.Navigation("ReceivedInvitations");

                    b.Navigation("SentInvitations");
                });
#pragma warning restore 612, 618
        }
    }
}
