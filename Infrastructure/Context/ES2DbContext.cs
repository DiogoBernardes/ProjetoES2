using System;
using System.Collections.Generic;
using IPVC.ESTG.ES2.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPVC.ESTG.ES2.Context;

public partial class ES2DbContext : DbContext
{
    public ES2DbContext()
    {
    }

    public ES2DbContext(DbContextOptions<ES2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<permission> permissions { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<role_permission> role_permissions { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=es2;Username=es2;Password=es2;SearchPath=public;Port=15432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("uuid-ossp")
            .HasPostgresExtension("topology", "postgis_topology");

        modelBuilder.Entity<permission>(entity =>
        {
            entity.HasKey(e => e.id).HasName("permissions_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("roles_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
        });

        modelBuilder.Entity<role_permission>(entity =>
        {
            entity.HasOne(d => d.permission).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_permission_id_fkey");

            entity.HasOne(d => d.role).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_role_id_fkey");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d.role).WithMany(p => p.users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
