namespace TaskV2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CRUDTaskDB : DbContext
    {
        public CRUDTaskDB()
            : base("name=CRUDTaskDB1")
        {
        }

        public virtual DbSet<Identidad> Identidad { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Identidad>()
                .Property(e => e.NombreIndentidad)
                .IsUnicode(false);

            modelBuilder.Entity<Identidad>()
                .Property(e => e.CorreoIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Contrasena)
                .IsUnicode(false);
        }
    }
}
