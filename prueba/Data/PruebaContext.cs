using Microsoft.EntityFrameworkCore;
using prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.Data
{
    public class PruebaContext : DbContext
    {
        public PruebaContext() {

        }
        public PruebaContext(DbContextOptions options) : base(options)
        {


        }
        public virtual DbSet<T_Clientes> T_Clientes { get; set; }
        public virtual DbSet<T_Listas> T_Listas { get; set; }
        public virtual DbSet<T_ListasClientes> T_ListasClientes {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_Listas>(entity =>
            {
                entity.HasKey(r => r.ID_lista);
                entity.Property(r => r.ID_lista).HasDefaultValueSql("(newsequentialid())");

                entity.HasData(new T_Listas { ID_lista = Guid.NewGuid(), Descripcion = "Lista 1", Duracion = 2 },
                               new T_Listas { ID_lista = Guid.NewGuid(),  Descripcion = "Lista 2", Duracion = 3 });
            });
            
            modelBuilder.Entity<T_Clientes>(entity =>
            {
                entity.HasKey(r => r.ID_Cliente);
                entity.Property(r => r.ID_Cliente).HasDefaultValueSql("(newsequentialid())");
            });


            modelBuilder.Entity<T_ListasClientes>(entity =>
            {
                entity.HasKey(r => r.ID_ListaCliente);
                entity.Property(r => r.ID_ListaCliente).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(r => r.ID_ClientesNavigation)
                .WithMany(r => r.T_ListasClientes)
                .HasForeignKey(r => r.ID_Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull);


                entity.HasOne(r => r.ID_ListaNavigation)
                .WithMany(r => r.T_ListasClientes)
                .HasForeignKey(r => r.ID_Lista)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

           


        }
    }
}
