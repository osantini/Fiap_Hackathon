var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware de exceções e roteamento
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Definir rota padrão para a página de login

app.Run();
