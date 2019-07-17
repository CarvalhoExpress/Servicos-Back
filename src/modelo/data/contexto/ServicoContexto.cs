using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using src.modelo.entidades;

namespace src.modelo.data.contexto
{
    public class ServicoContexto : DbContext
    {
        private readonly IConfiguration _config;

        public ServicoContexto(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<PrestadorDeServico> PrestadorDeServico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.AsProperty().Builder.HasMaxLength(256, ConfigurationSource.Convention);
                property.Relational().ColumnType = "varchar(256)";
            }

            modelBuilder.Entity<Usuario>(x => x.ToTable("Usuario","Servico"));
            modelBuilder.Entity<Servico>(x => x.ToTable("Servico","Servico"));
            modelBuilder.Entity<PrestadorDeServico>(x => x.ToTable("PrestadorDeServico","Servico"));
            modelBuilder.Entity<PrestadorDeServico>(x => x.HasOne(y => y.Usuario).WithOne(z => z.PrestadorDeServico).HasForeignKey<PrestadorDeServico>(a => a.UsuarioId));
            modelBuilder.Entity<PrestadorDeServico>(x => x.HasOne(y => y.Servico).WithMany(z => z.PrestadoresDeServicos).HasForeignKey(a => a.ServicoId));
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // define the database to use
            optionsBuilder.UseSqlServer(_config.GetConnectionString("ServicoConnection"));
        }

    }
}