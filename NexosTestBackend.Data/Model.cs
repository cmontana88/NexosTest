using Microsoft.EntityFrameworkCore;
using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data
{
    public partial class Model : DbContext
    {
        public Model(string connectionString) : base(GetOptions(connectionString))
        {

        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Editorial> Editorial { get; set; }
        public virtual DbSet<Libro> Libro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.CiudadNacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Libro)
                .WithOne(e => e.Autor)
                .IsRequired()
                .HasForeignKey(e => e.IdAutor);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.DirCorrespondencia)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .HasMany(e => e.Libro)
                .WithOne(e => e.Editorial)
                .IsRequired()
                .HasForeignKey(e => e.IdEditorial);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Genero)
                .IsFixedLength()
                .IsUnicode(false);
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
