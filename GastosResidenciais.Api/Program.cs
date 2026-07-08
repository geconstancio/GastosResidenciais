using GastosResidenciais.Api.Data;
using GastosResidenciais.Api.Middlewares;
using GastosResidenciais.Api.Repositories;
using GastosResidenciais.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// EF Core + SQLite, string de conexão vinda do appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Libera o front-end React (porta do Vite) a consumir a API via navegador.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Registro de cada interface com sua implementação, Scoped usado porque é o mesmo tipo do DbContext.
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

var app = builder.Build();

app.UseCors("PermitirFrontend");

// Intercepta exceções de regra de negócio antes dos Controllers processarem a requisição.
app.UseMiddleware<TratamentoDeExcecoesMiddleware>();

app.MapControllers();

app.Run();
