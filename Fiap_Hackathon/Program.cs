using Microsoft.EntityFrameworkCore;
using Fiap_Hackathon.Context;
using Fiap_Hackathon.Services;
using Fiap_Hackathon.Repositories;
using Fiap_Hackathon.Service;

var builder = WebApplication.CreateBuilder(args);

// Configura o Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar os servi�os
builder.Services.AddScoped<CadastroService>();
builder.Services.AddScoped<ValidationService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ClinicaService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<NotificacaoConsultaJob>();

builder.Services.AddScoped<CadastroRepository>();

// Outras configura��es...
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Acess�vel apenas pelo servidor
    options.Cookie.IsEssential = true; // Necess�rio para cookies sem consentimento
});

var app = builder.Build();

// Configura��es do pipeline da aplica��o
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); 
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Definir rota padr�o para a p�gina de login

app.Run();
