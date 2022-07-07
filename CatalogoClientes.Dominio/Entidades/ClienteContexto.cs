using System.Data.Entity;

namespace CatalogoClientes.Dominio.Entidades
{
    public class ClienteContexto : DbContext
    {
        public ClienteContexto() : base("name=ConexaoClientes") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ClienteContexto>(new CreateDatabaseIfNotExists<ClienteContexto>());
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
