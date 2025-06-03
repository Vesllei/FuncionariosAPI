using CadastroFuncionariosAPI.Data;
using CadastroFuncionariosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroFuncionariosAPI.Endpoints
{
    public static class FuncionarioPut
    {
        public static void MapFuncionarioPut(this WebApplication app)
        {
            // PUT - Atualizar funcionário existente
            app.MapPut("/api/funcionarios/{id}", async (int id, Funcionario updatedFuncionario, AppDbContext db) =>
            {
                var funcionario = await db.Funcionarios.FindAsync(id);

                if (funcionario is null)
                {
                    return Results.NotFound();
                }

                funcionario.Nome = updatedFuncionario.Nome;
                funcionario.Cargo = updatedFuncionario.Cargo;
                funcionario.Salario = updatedFuncionario.Salario;
                funcionario.DataAdmissao = updatedFuncionario.DataAdmissao;

                await db.SaveChangesAsync();

                return Results.Ok(funcionario);
            });
        }
    }
}
