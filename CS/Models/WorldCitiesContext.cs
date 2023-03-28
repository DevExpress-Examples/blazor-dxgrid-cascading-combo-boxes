using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CascadingComboBoxes.Models;

public partial class WorldCitiesContext : DbContext {
    public WorldCitiesContext() { }

    public WorldCitiesContext(DbContextOptions<WorldCitiesContext> options) : base(options) { }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=WorldCities;Integrated Security=true");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.UseCollation("Latin1_General_CI_AS");
        modelBuilder.Entity<City>(entity => {
            entity.HasKey(e => e.CityId).HasName("Cities$PrimaryKey");
            entity.HasIndex(e => e.CountryId, "Cities$CountryId");
            entity.Property(e => e.CityName).HasMaxLength(255);
            entity.Property(e => e.CountryId).HasDefaultValueSql("((0))");
            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Cities_Countries");
        });
        modelBuilder.Entity<Country>(entity => {
            entity.HasKey(e => e.CountryId).HasName("Countries$PrimaryKey");
            entity.HasIndex(e => e.CapitalId, "Countries$CapitalId");
            entity.Property(e => e.CapitalId).HasDefaultValueSql("((0))");
            entity.Property(e => e.CountryName).HasMaxLength(255);
            entity.HasOne(d => d.Capital).WithMany(p => p.Countries)
                .HasForeignKey(d => d.CapitalId)
                .HasConstraintName("FK_Countries_Cities");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
