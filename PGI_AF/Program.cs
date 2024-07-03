using PGI_AF.Services;
using PGI_AF.Autentication;
using PGI_AF.Interfaces;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();

// Registrar HttpClient con la URL base de tu API para todos los servicios
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUri"]!)
    };
    return httpClient;
});

// Registrar AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// Registrar otros servicios
builder.Services.AddScoped<CasosService>();
builder.Services.AddScoped<TareasService>();
builder.Services.AddScoped<AnalistasService>();
builder.Services.AddScoped<AssetsService>();
builder.Services.AddScoped<TipoAssetsService>();
builder.Services.AddScoped<MaquinasService>();
builder.Services.AddScoped<TipoIOCsService>();

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
