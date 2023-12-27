using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NASA_Asteroids.Data.Entities;

public partial class NASAContext : DbContext
{
    public NASAContext()
    {
    }

    public NASAContext(DbContextOptions<NASAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asteroids> Asteroids { get; set; }

    public virtual DbSet<Explorations> Explorations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asteroids>(entity =>
        {
            entity.Property(e => e.close_approach_date).HasColumnType("date");
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.neo_reference_id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.orbiting_body)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Explorations>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_Searches");

            entity.Property(e => e.dateFrom).HasColumnType("date");
            entity.Property(e => e.dateTo).HasColumnType("date");
            entity.Property(e => e.entryDate).HasColumnType("datetime");

            entity.HasMany(d => d.asteroid).WithMany(p => p.exploration)
                .UsingEntity<Dictionary<string, object>>(
                    "ExplorationAsteroid",
                    r => r.HasOne<Asteroids>().WithMany()
                        .HasForeignKey("asteroidId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExplorationAsteroid_Asteroids"),
                    l => l.HasOne<Explorations>().WithMany()
                        .HasForeignKey("explorationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExplorationAsteroid_Explorations"),
                    j =>
                    {
                        j.HasKey("explorationId", "asteroidId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
