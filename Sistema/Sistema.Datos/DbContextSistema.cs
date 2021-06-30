using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Carteras;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Carteras;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Cartera> Carteras { get; set; }
        public DbSet<DetalleCartera> DetalleCarteras { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarteraMap());
            modelBuilder.ApplyConfiguration(new DetalleCarteraMap());
            modelBuilder.ApplyConfiguration(new GastoMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
