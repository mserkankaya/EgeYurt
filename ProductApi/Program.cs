using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductApi.Business.Interfaces;
using ProductApi.Business.Mapping;
using ProductApi.Business.Middleware;
using ProductApi.Business.Services;
using ProductApi.Business.Validation;
using ProductApi.DataAccess.Concrete;
using ProductApi.DataAccess.Repositories;
using ProductApi.Dto.Product;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Serilog konfigrasyonu
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

builder.Host.UseSerilog(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });

	// JWT Authentication konfigrasyonu
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});




builder.Services.AddHttpLogging(logging =>
{
	logging.LoggingFields = HttpLoggingFields.All;
});


//Docker için appsettings içerisinde kullanýlan connection string uymadý, Context nesnesi içine ekledim. o sebeple burayý kapadým.

//builder.Services.AddDbContext<Context>(options =>
//	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<EgeYurtContext>();

//AddScoped: Her bir istek için yeni bir örnek oluþturur.
//AddSingleton: Uygulama ömrü boyunca tek bir örnek kullanýr..

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

//Validasyon
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);


builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./keys"))
    .SetApplicationName("ProductApi");

//JWT tabanlý kimlik doðrulama yapýlandýrmasý

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

//AddCors: CORS politikasý. Farklý kaynaklardan gelen isteklere izin vermek için yapýlandýrýlýr.
//AllowAnyOrigin, AllowAnyMethod, AllowAnyHeader: Herhangi bir kaynaktan, herhangi bir HTTP yöntemiyle gelen isteklere izin verir.

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Tanýmlamýþ olduðum CORS politikasýný uygulamak için kullanýlýr.
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Middleware uygulamak için
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<TokenValidationMiddleware>();

// Uygulama baþlamadan önce loglama
Log.Information("Uygulama baþlatýlýyor...");

app.Run();


// Uygulama kapatýldýðýnda loglama
AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.Information("Uygulama kapatýlýyor...");
