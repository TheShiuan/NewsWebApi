using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace News.Models;

public partial class WebContext : DbContext
{
    public WebContext(DbContextOptions<WebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enployees> Enployees { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enployees>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.EmployeesId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.JobId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.InsertDateTime).HasColumnType("datetime");
            entity.Property(e => e.NewsId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
