using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CineFlix.Models;

public partial class CineFlixDbContext : DbContext
{
    public CineFlixDbContext()
    {
    }

    public CineFlixDbContext(DbContextOptions<CineFlixDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tvshow> Tvshows { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("pk_MovieId");

            entity.Property(e => e.Genres)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MovieDesc).IsUnicode(false);
            entity.Property(e => e.MovieLanguage)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MovieName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Movies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Movies__UserId__160F4887");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("pk_RoleId");

            entity.HasIndex(e => e.RoleName, "uq_RoleName").IsUnique();

            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tvshow>(entity =>
        {
            entity.HasKey(e => e.ShowId).HasName("pk_ShowId");

            entity.ToTable("TVShows");

            entity.Property(e => e.Genres)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Show)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShowDesc).IsUnicode(false);
            entity.Property(e => e.ShowLanguage)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Tvshows)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TVShows__UserId__19DFD96B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pk_UserId");

            entity.HasIndex(e => e.EmailId, "uq_EmailId").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "uq_phoneNumber").IsUnique();

            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MembershipEndDate).HasColumnType("date");
            entity.Property(e => e.MembershipStartDate).HasColumnType("date");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PlanType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
