using Consume.Services;
using Microsoft.AspNetCore.DataProtection;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


//Oturum s�resi ve cookie ayarlar�.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in
    options.Cookie.IsEssential = true; // Gerekli cookie
});

//Tokeni katman d��� saklama
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"))
    .SetApplicationName("EgeYurt");


// AuthService ve ProductService i�in adresler.
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("http://productapi:5001");
});
builder.Services.AddHttpClient<ProductService>(client =>
{
    client.BaseAddress = new Uri("http://productapi:5001");
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//401 Unauthorized hatas� i�in y�nlendirme.
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        context.HttpContext.Response.Redirect("/Auth/Unauthorized");
    }
});

app.UseSession(); 
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.UseStatusCodePages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
