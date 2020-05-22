using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_CodeFirst.Models
{
    public class CodeFirstContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {

                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");
                entity.Property(e => e.IdPatient).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();


            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");
               // entity.Property(e => e.IdDoctor).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");
               entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions).HasForeignKey(d => d.IdPatient).HasConstraintName("Prescription_Patient");
                entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions).HasForeignKey(d => d.IdDoctor).HasConstraintName("Prescription_Doctor");
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.ToTable("Prescription_Medicament");
                entity.HasKey(e => e.IdMedicament).HasName("PMMedicament_PK");
                entity.HasKey(e => e.IdPrescription).HasName("PMPrescription_PK");
                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();

                entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionMedicaments).HasForeignKey(d => d.IdPrescription).HasConstraintName("PrescriptionMedicament_Prescription");
                entity.HasOne(d => d.Medicament).WithMany(p => p.PrescriptionMedicaments).HasForeignKey(d => d.IdMedicament).HasConstraintName("PrescriptionMedicament_Medicament");
            });

            Seed(modelBuilder);

        }

        public static void Seed(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    IdPatient = 1,
                    FirstName = "Arthur",
                    LastName = "Morgan",
                    Birthdate = DateTime.Parse("1998.05.05")
                }) ;

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "Mario",
                    LastName = "Luigi",
                    Email = "inanothercastle@gmail.com"
                }
                );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament
                {
                    IdMedicament = 1,
                    Name = "Parogen",
                    Description="Anxiety medication",
                    Type="SSRI"

                }) ;

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(30),
                    IdDoctor = 1,
                    IdPatient = 1
                }) ;

            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament
                {
                    IdMedicament = 1,
                    IdPrescription = 1,
                    Dose = 30,
                    Details="1,5 tabletki codziennie rano"
                }) ;
        }
    }
}
