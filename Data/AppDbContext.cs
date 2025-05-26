using CadastroFuncionariosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroFuncionariosAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
}
