using Microsoft.EntityFrameworkCore;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    // Classe que representa o contexto de conexão com o banco de dados usando Entity Framework Core.
    // É responsável por mapear as entidades (Models) para as tabelas do banco e configurar a conexão.
    public class ConnectionContext : DbContext
    {
        // Cada DbSet representa uma tabela no banco de dados.
        // Permite realizar operações CRUD (Create, Read, Update, Delete) através do EF Core.
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Chamado> Chamado { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Designacao_tecnico> Designacao_tecnico { get; set; }

        // Configuração da conexão com o banco de dados.
        // Caso ainda não esteja configurada, define o caminho do banco SQLite local.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Caminho absoluto do banco SQLite (criado na pasta onde o aplicativo está rodando)
                var dbPath = Path.Combine(AppContext.BaseDirectory, "WebChama.db");

                // Configura o EF Core para usar o banco SQLite no caminho especificado
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }
    }
}
