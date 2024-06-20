using FullStackWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Pessoa> Pessoas { get; set; } = default!;

        public DbSet<Dependente> Dependente { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pessoa>()
                .HasMany(p => p.Dependentes)
                .WithOne(d => d.Pessoa)
                .HasForeignKey(d => d.PessoaId);
        }
    }
}
