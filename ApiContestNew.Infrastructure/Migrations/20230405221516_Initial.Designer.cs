﻿// <auto-generated />
using System;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiContestNew.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230405221516_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalAnimalType", b =>
                {
                    b.Property<long>("AnimalTypesId")
                        .HasColumnType("bigint");

                    b.Property<long>("AnimalsId")
                        .HasColumnType("bigint");

                    b.HasKey("AnimalTypesId", "AnimalsId");

                    b.HasIndex("AnimalsId");

                    b.ToTable("AnimalAnimalType");
                });

            modelBuilder.Entity("AnimalAnimalVisitedLocation", b =>
                {
                    b.Property<long>("AnimalsId")
                        .HasColumnType("bigint");

                    b.Property<long>("VisitedLocationsId")
                        .HasColumnType("bigint");

                    b.HasKey("AnimalsId", "VisitedLocationsId");

                    b.HasIndex("VisitedLocationsId");

                    b.ToTable("AnimalAnimalVisitedLocation");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.Animal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChipperId")
                        .HasColumnType("bigint");

                    b.Property<int>("ChipperId1")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("ChippingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ChippingLocationId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeathDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<string>("LifeStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ChipperId1");

                    b.HasIndex("ChippingLocationId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.AnimalType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AnimalTypes");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.AnimalVisitedLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("DateTimeOfVisitLocationPoint")
                        .HasPrecision(6)
                        .HasColumnType("timestamp(6) with time zone");

                    b.Property<long>("LocationPointId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LocationPointId");

                    b.ToTable("AnimalVisitedLocations");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.LocationPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("LocationPoints");
                });

            modelBuilder.Entity("AnimalAnimalType", b =>
                {
                    b.HasOne("ApiContestNew.Core.Models.Entities.AnimalType", null)
                        .WithMany()
                        .HasForeignKey("AnimalTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiContestNew.Core.Models.Entities.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalAnimalVisitedLocation", b =>
                {
                    b.HasOne("ApiContestNew.Core.Models.Entities.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiContestNew.Core.Models.Entities.AnimalVisitedLocation", null)
                        .WithMany()
                        .HasForeignKey("VisitedLocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.Animal", b =>
                {
                    b.HasOne("ApiContestNew.Core.Models.Entities.Account", "Chipper")
                        .WithMany("ChippedAnimals")
                        .HasForeignKey("ChipperId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiContestNew.Core.Models.Entities.LocationPoint", "ChippingLocation")
                        .WithMany("ChippedAnimals")
                        .HasForeignKey("ChippingLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chipper");

                    b.Navigation("ChippingLocation");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.AnimalVisitedLocation", b =>
                {
                    b.HasOne("ApiContestNew.Core.Models.Entities.LocationPoint", "LocationPoint")
                        .WithMany("AnimalVisitedLocation")
                        .HasForeignKey("LocationPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationPoint");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.Account", b =>
                {
                    b.Navigation("ChippedAnimals");
                });

            modelBuilder.Entity("ApiContestNew.Core.Models.Entities.LocationPoint", b =>
                {
                    b.Navigation("AnimalVisitedLocation");

                    b.Navigation("ChippedAnimals");
                });
#pragma warning restore 612, 618
        }
    }
}
