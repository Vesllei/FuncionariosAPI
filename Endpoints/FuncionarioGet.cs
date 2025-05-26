using CadastroFuncionariosAPI.Data;
using CadastroFuncionariosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroFuncionariosAPI.Endpoints
{
    public static class FuncionarioGet
    {
        public static void MapFuncionarioGet(this WebApplication app)
        {
            // GET - Listar todos os funcionários
            app.MapGet("/api/funcionarios", async (AppDbContext db) =>
                await db.Funcionarios.ToListAsync());

            // GET - Buscar funcionário por ID
            app.MapGet("/api/funcionarios/{id}", async (int id, AppDbContext db) =>
                await db.Funcionarios.FindAsync(id) is Funcionario f
                    ? Results.Ok(f)
                    : Results.NotFound());
        }
    }
}
