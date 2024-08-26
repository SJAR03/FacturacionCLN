using FacturacionCLN.Models;
using Microsoft.EntityFrameworkCore;

namespace FacturacionCLN.Data
{
    public class FacturacionDbContext : DbContext
    {
        public FacturacionDbContext(DbContextOptions<FacturacionDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<TasaCambio> TasaCambios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuracion relaciones tablas
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(d => d.Factura)
                .WithMany(f => f.DetallesFactura)
                .HasForeignKey(d => d.IdFactura);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.IdProducto);
        }
    }
}
