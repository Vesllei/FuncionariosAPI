using CadastroFuncionariosAPI.Data;
using CadastroFuncionariosAPI.Models;

namespace CadastroFuncionariosAPI.Endpoints
{
    public static class FuncionarioPost
    {
        public static void MapFuncionarioPost(this WebApplication app)
        {
            // POST - Adicionar novo funcionário
            app.MapPost("/api/funcionarios", async (Funcionario funcionario, AppDbContext db) =>
            {
                db.Funcionarios.Add(funcionario);
                await db.SaveChangesAsync();
                return Results.Created($"/api/funcionarios/{funcionario.Id}", funcionario);
            });
        }
    }
}
