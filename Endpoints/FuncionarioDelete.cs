using CadastroFuncionariosAPI.Data;
using CadastroFuncionariosAPI.Models;

namespace CadastroFuncionariosAPI.Endpoints
{
    public static class FuncionarioDelete
    {
        public static void MapFuncionarioDelete(this WebApplication app)
        {
            // DELETE - Excluir funcionário por ID
            app.MapDelete("/api/funcionarios/{id}", async (int id, AppDbContext db) =>
            {
                var funcionario = await db.Funcionarios.FindAsync(id);
                if (funcionario is null) return Results.NotFound();

                db.Funcionarios.Remove(funcionario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
