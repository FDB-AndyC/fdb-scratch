﻿namespace RoadmapData.Models
{
    using Microsoft.EntityFrameworkCore;

    public partial class RoadmapContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Roadmap> Roadmap { get; set; }
        public virtual DbSet<Swimlane> Swimlane { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=.\SQL2017;Database=Roadmap01;Trusted_Connection=True;");
        //            }
        //        }

        public RoadmapContext(DbContextOptions<RoadmapContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Roadmap>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Roadmap)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roadmap_Category");

                entity.HasOne(d => d.Swimlane)
                    .WithMany(p => p.Roadmap)
                    .HasForeignKey(d => d.SwimlaneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roadmap_Swimlane");
            });

            modelBuilder.Entity<Swimlane>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });
        }
    }
}
