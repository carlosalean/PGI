using Blazored.LocalStorage;
using PGI_AF.Services;
using Microsoft.AspNetCore.Components.Authorization;
using PGI_AF.Autentication;
using PGI_AF.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddHttpClient<CasosService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<TareasService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<AnalistasService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<AssetsService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<TipoAssetsService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<MaquinasService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<TipoIOCsService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});

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
