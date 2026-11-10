using System;
using System.Collections.Generic;
using DatabaseFirstMasterpeice.Models.DeepSproutTask;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstMasterpeice.Data;

public partial class DeepSproutTaskContext : DbContext
{
    public DeepSproutTaskContext(DbContextOptions<DeepSproutTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConsentRecord> ConsentRecords { get; set; }

    public virtual DbSet<IndividualProfile> IndividualProfiles { get; set; }

    public virtual DbSet<OfflineRoutine> OfflineRoutines { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<RoutineCheckIn> RoutineCheckIns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConsentRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ConsentR__3214EC078F45080A");

            entity.ToTable("ConsentRecord");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Person).WithMany(p => p.ConsentRecords)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__ConsentRe__Perso__5629CD9C");
        });

        modelBuilder.Entity<IndividualProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Individu__3214EC075CCAED5C");

            entity.ToTable("IndividualProfile");

            entity.HasIndex(e => e.PersonId, "UQ__Individu__AA2FFBE482E3D0A1").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Goal)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Person).WithOne(p => p.IndividualProfile)
                .HasForeignKey<IndividualProfile>(d => d.PersonId)
                .HasConstraintName("FK__Individua__Perso__4D94879B");
        });

        modelBuilder.Entity<OfflineRoutine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OfflineR__3214EC07DF06B0DF");

            entity.ToTable("OfflineRoutine");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Person).WithMany(p => p.OfflineRoutines)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__OfflineRo__Perso__5070F446");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC0739F9443D");

            entity.ToTable("Person");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoutineCheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoutineC__3214EC0791D41498");

            entity.ToTable("RoutineCheckIn");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Routine).WithMany(p => p.RoutineCheckIns)
                .HasForeignKey(d => d.RoutineId)
                .HasConstraintName("FK__RoutineCh__Routi__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
