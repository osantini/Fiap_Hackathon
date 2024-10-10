using Microsoft.EntityFrameworkCore;
using Fiap_Hackathon.Context;
using Fiap_Hackathon.Services;
using Fiap_Hackathon.Repositories;
using Fiap_Hackathon.Service;

var builder = WebApplication.CreateBuilder(args);

// Configura o Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar os serviços
builder.Services.AddScoped<CadastroService>();
builder.Services.AddScoped<ValidationService>();

builder.Services.AddScoped<CadastroRepository>();

// Outras configurações...
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurações do pipeline da aplicação
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Definir rota padrão para a página de login

app.Run();
