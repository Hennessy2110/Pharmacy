using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pharmacy
{
    public partial class PharmacyContext : DbContext
    {
        public virtual DbSet<Arrival> Arrival { get; set; }
        public virtual DbSet<Delivers> Delivers { get; set; }
        public virtual DbSet<Expence> Expence { get; set; }
        public virtual DbSet<Medicaments> Medicaments { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public PharmacyContext(DbContextOptions<PharmacyContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Pharmacy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arrival>(entity =>
            {
                entity.Property(e => e.ArrivalId).HasColumnName("arrivalId");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.DeliverId).HasColumnName("deliverId");

                entity.Property(e => e.MedicamentId).HasColumnName("medicamentId");

                entity.Property(e => e.PurchasePrice).HasColumnName("purchasePrice");

                entity.Property(e => e.ReceiptDate)
                    .IsRequired()
                    .HasColumnName("receiptDate")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Deliver)
                    .WithMany(p => p.Arrival)
                    .HasForeignKey(d => d.DeliverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arrival_ToDelivers");

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Arrival)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arrival_ToMedicaments");
            });

            modelBuilder.Entity<Delivers>(entity =>
            {
                entity.HasKey(e => e.DeliverId);

                entity.Property(e => e.DeliverId).HasColumnName("deliverId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasColumnName("patronymic")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Expence>(entity =>
            {
                entity.HasKey(e => e.ExpenseId);

                entity.Property(e => e.ExpenseId).HasColumnName("expenseId");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.DateOfSale)
                    .IsRequired()
                    .HasColumnName("dateOfSale")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicamentId).HasColumnName("medicamentId");

                entity.Property(e => e.SellingPrice).HasColumnName("sellingPrice");

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Expence)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Expence_ToMedicaments");
            });

            modelBuilder.Entity<Medicaments>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.MedicamentId).HasColumnName("medicamentId");

                entity.Property(e => e.Annotation)
                    .IsRequired()
                    .HasColumnName("annotation")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Producer)
                    .IsRequired()
                    .HasColumnName("producer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Storage)
                    .IsRequired()
                    .HasColumnName("storage")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Units)
                    .IsRequired()
                    .HasColumnName("units")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
