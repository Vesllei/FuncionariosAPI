using Microsoft.EntityFrameworkCore;
using CadastroFuncionariosAPI.Models;
using CadastroFuncionariosAPI.Data;
using CadastroFuncionariosAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=funcionarios.db"));

// Configuração de CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            
                            .AllowAnyOrigin() 
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseDefaultFiles();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Funcionarios.Any())
    {
        db.Funcionarios.AddRange(new[]
        {
            new Funcionario { Nome = "João Silva", Cargo = "Gerente", Salario = 8000, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Ana Lima", Cargo = "Secretária", Salario = 3000, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Carlos Souza", Cargo = "Desenvolvedor", Salario = 6000, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Paula Rocha", Cargo = "Analista", Salario = 5500, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Marcos Pinto", Cargo = "Técnico", Salario = 4000, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Tatiane Reis", Cargo = "Financeiro", Salario = 4800, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Lucas Almeida", Cargo = "TI", Salario = 5300, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Fernanda Costa", Cargo = "RH", Salario = 4700, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Pedro Gonçalves", Cargo = "Marketing", Salario = 5100, DataAdmissao = DateTime.Now },
            new Funcionario { Nome = "Juliana Martins", Cargo = "Vendas", Salario = 4600, DataAdmissao = DateTime.Now }
        });
        db.SaveChanges();
    }
}

// endpoints
app.MapFuncionarioGet();
app.MapFuncionarioPost();
app.MapFuncionarioDelete();
app.MapFuncionarioPut();


app.Run();
