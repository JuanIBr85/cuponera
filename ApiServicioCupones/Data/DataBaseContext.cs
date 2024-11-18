using ApiServicioCupones.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServicioCupones.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<ArticuloModel> Articulos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<CuponModel> Cupones { get; set; }
        public DbSet<Cupon_CategoriaModel> Cupones_Categorias { get; set; }
        public DbSet<Cupon_ClienteModel> Cupones_Clientes { get; set; }
        public DbSet<Cupon_DetalleModel> Cupones_Detalle { get; set; }
        public DbSet<PrecioModel> Precios { get; set; }
        public DbSet<Tipo_CuponModel> Tipo_Cupon { get; set; }
        public DbSet<Cupon_HistorialModel> Cupones_Historial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Articulo - Precio
            modelBuilder.Entity<ArticuloModel>()
                .HasOne(a => a.Precio)
                .WithOne(p => p.Articulo)
                .HasForeignKey<PrecioModel>(p => p.Id_Articulo);

            // Relación Categoría - Cupon_Categoria
            modelBuilder.Entity<Cupon_CategoriaModel>()
                .HasOne(cc => cc.Categoria)
                .WithMany(c => c.Cupones_Categorias)
                .HasForeignKey(cc => cc.Id_Categoria)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de claves principales
            modelBuilder.Entity<CategoriaModel>().HasKey(c => c.Id_Categoria);
            modelBuilder.Entity<CuponModel>().HasKey(c => c.Id_Cupon);
            modelBuilder.Entity<Cupon_CategoriaModel>().HasKey(c => c.Id_Cupones_Categorias);
            modelBuilder.Entity<Cupon_ClienteModel>().HasKey(c => c.NroCupon);
            modelBuilder.Entity<Cupon_DetalleModel>().HasKey(c => new { c.Id_Cupon, c.Id_Articulo });
            modelBuilder.Entity<PrecioModel>().HasKey(c => c.Id_Precio);
            modelBuilder.Entity<Tipo_CuponModel>().HasKey(c => c.Id_Tipo_Cupon);
            modelBuilder.Entity<Cupon_HistorialModel>().HasKey(c => new { c.Id_Cupon, c.NroCupon });

            // Configuración de columnas decimales
            modelBuilder.Entity<PrecioModel>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CuponModel>()
                .Property(c => c.ImportePromo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CuponModel>()
                .Property(c => c.PorcentajeDto)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
