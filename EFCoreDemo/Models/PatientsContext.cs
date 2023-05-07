using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Models;

public partial class PatientsContext : DbContext
{
    public PatientsContext()
    {
    }

    public PatientsContext(DbContextOptions<PatientsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<EmailAddress> EmailAddresses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AddressID");
            entity.Property(e => e.Address1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addresses_Patients");
        });

        modelBuilder.Entity<EmailAddress>(entity =>
        {
            entity.HasKey(e => e.EmailId);

            entity.Property(e => e.EmailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("EmailID");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Patient).WithMany(p => p.EmailAddresses)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailAddresses_Patients");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.PatientId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PatientID");
            entity.Property(e => e.Birthdate).HasColumnType("date");
            entity.Property(e => e.DateAdded).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.HasKey(e => e.PhoneId);

            entity.Property(e => e.PhoneId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PhoneID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PhoneNumber1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhoneNumber");
            entity.Property(e => e.PhoneType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhoneNumbers_Patients");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
