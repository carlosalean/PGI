using PGI_AF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient<CasosService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});
builder.Services.AddHttpClient<TareasService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});
builder.Services.AddHttpClient<AnalistasService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});
builder.Services.AddHttpClient<AssetsService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});
builder.Services.AddHttpClient<TipoAssetsService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});
builder.Services.AddHttpClient<MaquinasService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});
builder.Services.AddHttpClient<TipoIOCsService>(client =>
{
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiSettings:BaseUri"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
