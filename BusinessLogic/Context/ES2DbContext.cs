using System;
using System.Collections.Generic;
using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Context;

public partial class ES2DbContext : DbContext
{
    public ES2DbContext()
    {
    }

    public ES2DbContext(DbContextOptions<ES2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<activity_info> activity_infos { get; set; }

    public virtual DbSet<activity_participant> activity_participants { get; set; }

    public virtual DbSet<event_category> event_categories { get; set; }

    public virtual DbSet<event_info> event_infos { get; set; }

    public virtual DbSet<event_regist> event_regists { get; set; }

    public virtual DbSet<event_ticket> event_tickets { get; set; }

    public virtual DbSet<message> messages { get; set; }

    public virtual DbSet<regist_state> regist_states { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<ticket_type> ticket_types { get; set; }

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

        modelBuilder.Entity<activity_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("activity_info_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d._event).WithMany(p => p.activity_infos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activity_info_event_id_fkey");
        });

        modelBuilder.Entity<activity_participant>(entity =>
        {
            entity.HasOne(d => d.activity).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activity_participant_activity_id_fkey");

            entity.HasOne(d => d.participant).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activity_participant_participant_id_fkey");
        });

        modelBuilder.Entity<event_category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("event_category_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
        });

        modelBuilder.Entity<event_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("event_info_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d.categoryNavigation).WithMany(p => p.event_infos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_info_category_fkey");

            entity.HasOne(d => d.organizer).WithMany(p => p.event_infos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_info_organizer_id_fkey");
        });

        modelBuilder.Entity<event_regist>(entity =>
        {
            entity.HasKey(e => e.id).HasName("event_regist_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d._event).WithMany(p => p.event_regists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_regist_event_id_fkey");

            entity.HasOne(d => d.participant).WithMany(p => p.event_regists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_regist_participant_id_fkey");

            entity.HasOne(d => d.state).WithMany(p => p.event_regists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_regist_state_id_fkey");

            entity.HasOne(d => d.ticket_type).WithMany(p => p.event_regists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_regist_ticket_type_id_fkey");
        });

        modelBuilder.Entity<event_ticket>(entity =>
        {
            entity.HasOne(d => d._event).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_ticket_event_id_fkey");

            entity.HasOne(d => d.ticket_typeNavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_ticket_ticket_type_fkey");
        });

        modelBuilder.Entity<message>(entity =>
        {
            entity.HasKey(e => e.id).HasName("messages_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d._event).WithMany(p => p.messages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_event_id_fkey");

            entity.HasOne(d => d.organizer).WithMany(p => p.messageorganizers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_organizer_id_fkey");

            entity.HasOne(d => d.participant).WithMany(p => p.messageparticipants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_participant_id_fkey");
        });

        modelBuilder.Entity<regist_state>(entity =>
        {
            entity.HasKey(e => e.id).HasName("regist_state_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("roles_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
        });

        modelBuilder.Entity<ticket_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("ticket_type_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
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
