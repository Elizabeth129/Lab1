using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab1
{
    public partial class Professor_PublicationContext : DbContext
    {
        public Professor_PublicationContext()
        {
        }

        public Professor_PublicationContext(DbContextOptions<Professor_PublicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cathedra> Cathedra { get; set; }
        public virtual DbSet<DegreeCollection> DegreeCollection { get; set; }
        public virtual DbSet<FacultyCollection> FacultyCollection { get; set; }
        public virtual DbSet<PlaceOfWork> PlaceOfWork { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<ProfessorPublicationLinker> ProfessorPublicationLinker { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<PublishingCollection> PublishingCollection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-RQH379V\\SQLEXPRESS; Database=Professor_Publication; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cathedra>(entity =>
            {
                entity.Property(e => e.CathedraName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FacultyId).HasColumnName("FacultyID");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Cathedra)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cathedra_Faculty");
            });

            modelBuilder.Entity<DegreeCollection>(entity =>
            {
                entity.Property(e => e.DegreeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FacultyCollection>(entity =>
            {
                entity.Property(e => e.FacultyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PlaceOfWork>(entity =>
            {
                entity.Property(e => e.CathedraId).HasColumnName("CathedraID");

                entity.Property(e => e.DateOfEndWork).HasColumnType("date");

                entity.Property(e => e.DateOfStartWork).HasColumnType("date");

                entity.HasOne(d => d.Cathedra)
                    .WithMany(p => p.PlaceOfWork)
                    .HasForeignKey(d => d.CathedraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceOfWork_Cathedra");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DegreeId).HasColumnName("DegreeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.Professor)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professor_Degree_Categories");

                entity.HasOne(d => d.PlaceOfWorking)
                    .WithMany(p => p.Professor)
                    .HasForeignKey(d => d.PlaceOfWorkingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professor_PlaceOfWorking");
            });

            modelBuilder.Entity<ProfessorPublicationLinker>(entity =>
            {
                entity.ToTable("Professor_PublicationLinker");

                entity.HasOne(d => d.Professor)
                    .WithMany(p => p.ProfessorPublicationLinker)
                    .HasForeignKey(d => d.ProfessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professor_PublicationLinker_Professor");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.ProfessorPublicationLinker)
                    .HasForeignKey(d => d.PublicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professor_PublicationLinker_Publication");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.Property(e => e.NamePublication)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PublishingId).HasColumnName("PublishingID");

                entity.HasOne(d => d.Publishing)
                    .WithMany(p => p.Publication)
                    .HasForeignKey(d => d.PublishingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Publication_Publishing");
            });

            modelBuilder.Entity<PublishingCollection>(entity =>
            {
                entity.Property(e => e.PublishingName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
