using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using TimeSheetApp.Auth;
using TimeSheetApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAuthenticationCore();
builder.Services.AddServerSideBlazor();
//L'application va créer un object de ce service afin d'avoir accces au sessionStorage de l'utilisateur
//cette object va pouvoir être utiliser lors de l'instance du CustomAuthStateProvider
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<UserAccountService>();
//Connection avec la base de donnée
builder.Services.AddDbContext<UserAccountDbContext>(options => {options.UseSqlite("Data Source = TimeSheet.db");});
builder.Services.AddSingleton<Week>();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
