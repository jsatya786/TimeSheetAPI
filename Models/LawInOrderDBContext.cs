using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmpTimeSheet.Models
{
    public partial class LawInOrderDBContext : DbContext
    {
        public LawInOrderDBContext()
        {
        }

        public LawInOrderDBContext(DbContextOptions<LawInOrderDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonTimeSheet> PersonTimeSheet { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonTimeSheet>(entity =>
            {
                entity.ToTable("personTimeSheet");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.PersonTimeSheet)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__personTimeS__PID__286302EC");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__Persons__C57755208C132572");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.PersonName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
