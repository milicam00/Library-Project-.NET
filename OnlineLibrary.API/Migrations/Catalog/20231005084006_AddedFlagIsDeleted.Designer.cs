﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineLibrary.Modules.Catalog.Infrastructure;

#nullable disable

namespace OnlineLibrary.API.Migrations.Catalog
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20231005084006_AddedFlagIsDeleted")]
    partial class AddedFlagIsDeleted
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineLibrary.BuildingBlocks.Application.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions.Book", b =>
                {
                    b.Property<Guid>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genres")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LibraryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfCopies")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRatings")
                        .HasColumnType("int");

                    b.Property<int>("Pages")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("PubblicationYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("UserRating")
                        .HasColumnType("float");

                    b.HasKey("BookId");

                    b.HasIndex("LibraryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription.Library", b =>
                {
                    b.Property<Guid>("LibraryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LibraryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LibraryId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription.Rental", b =>
                {
                    b.Property<Guid>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Returned")
                        .HasColumnType("bit");

                    b.HasKey("RentalId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription.RentalBook", b =>
                {
                    b.Property<Guid>("RentalBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RatedRating")
                        .HasColumnType("int");

                    b.Property<Guid>("RentalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TextualComment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RentalBookId");

                    b.HasIndex("BookId");

                    b.HasIndex("RentalId");

                    b.ToTable("RentalBooks");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription.Owner", b =>
                {
                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OwnerId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription.Reader", b =>
                {
                    b.Property<Guid>("ReaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReaderId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions.Book", b =>
                {
                    b.HasOne("OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription.Library", "Library")
                        .WithMany("Books")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription.Library", b =>
                {
                    b.HasOne("OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription.Owner", "Owner")
                        .WithMany("Libraries")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription.Rental", b =>
                {
                    b.HasOne("OnlineLibrary.Modules.Catalog.Domain.LibraryUser.UserSubscription.Reader", "Reader")
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription.RentalBook", b =>
                {
                    b.HasOne("OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions.Book", "Book")
                        .WithMany("RentalBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription.Rental", "Rental")
                        .WithMany("RentalBooks")
                        .HasForeignKey("RentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryBooks.BookSubscriptions.Book", b =>
                {
                    b.Navigation("RentalBooks");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryLibraries.LibrarySubscription.Library", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryRental.RentalSubscription.Rental", b =>
                {
                    b.Navigation("RentalBooks");
                });

            modelBuilder.Entity("OnlineLibrary.Modules.Catalog.Domain.LibraryUser.OwnerSubscription.Owner", b =>
                {
                    b.Navigation("Libraries");
                });
#pragma warning restore 612, 618
        }
    }
}
