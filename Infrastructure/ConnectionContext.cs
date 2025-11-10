using Microsoft.EntityFrameworkCore;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        // DbSets representam as tabelas do seu banco de dados
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Chamado> Chamado { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Designacao_tecnico> Designacao_tecnico { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Caminho absoluto do banco SQLite (criado na pasta onde o app roda)
                var dbPath = Path.Combine(AppContext.BaseDirectory, "WebChama.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }
    }
}
