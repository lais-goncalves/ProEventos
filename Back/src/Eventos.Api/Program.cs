using System.Text;
using System.Text.Json.Serialization;
using Eventos.Application;
using Eventos.Application.Contratos;
using Eventos.Application.Helpers;
using Eventos.Application.ServicesUsuario;
using Eventos.Domain.Identity;
using Eventos.Persistence;
using Eventos.Persistence.Contratos;
using Eventos.Persistence.Persists;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

// BUILDER
var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<EventosContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services
       .AddIdentityCore<Usuario>(options =>
       {
	       options.Password.RequireDigit = false;
	       options.Password.RequireNonAlphanumeric = false;
	       options.Password.RequireUppercase = false;
	       options.Password.RequireLowercase = false;
	       options.Password.RequiredLength = 4;
       })
       .AddRoles<Papel>()
       .AddRoleManager<RoleManager<Papel>>()
       .AddSignInManager<SignInManager<Usuario>>()
       .AddRoleValidator<RoleValidator<Papel>>()
       .AddEntityFrameworkStores<EventosContext>()
       .AddDefaultTokenProviders();

builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
	       options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	       options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
       });

builder.Services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
	       options.TokenValidationParameters = new TokenValidationParameters
	       {
		       ValidateIssuerSigningKey = true,
		       IssuerSigningKey =
			       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"] ?? string.Empty)),
		       ValidateIssuer = false,
		       ValidateAudience = false
	       };
       });


builder.Services.AddSwaggerGen(options => {
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		Description = "JWT Authorization header using the Bearer scheme.'"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement {{
		new OpenApiSecurityScheme {
			Reference = new OpenApiReference {
				Type = ReferenceType.SecurityScheme,
				Id = "Bearer"
			}
		},
		Array.Empty<string>()
	}});
});

builder.Services.AddAutoMapper(typeof(ProEventosProfile).Assembly);

builder.Services.AddScoped<ILotesService, LoteService>();
builder.Services.AddScoped<IEventosService, EventoService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IContaService, ContaService>();

builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<ILotePersist, LotePersist>();
builder.Services.AddScoped<IEventoPersist, EventoPersist>();
builder.Services.AddScoped<IUsuarioPersist, UsuarioPersist>();

builder.Services.AddCors();
builder.Services.AddOpenApi();


// APP
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi("/openapi/v1.json");
	app.MapScalarApiReference(options =>
	{
		options.BaseServerUrl = "/api";
		options.AddPreferredSecuritySchemes("BearerAuth");
	});
}
else
{
	app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(cors => cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
           );

app.UseStaticFiles(new StaticFileOptions()
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
	RequestPath = new PathString("/Resources")
});

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
	
app.Run();