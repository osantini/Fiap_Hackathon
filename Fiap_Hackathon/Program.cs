var builder = WebApplication.CreateBuilder(args);

// Configura��es de servi�os
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware de exce��es e roteamento
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
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Definir rota padr�o para a p�gina de login

app.Run();
