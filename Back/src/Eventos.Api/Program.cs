using System.Text.Json.Serialization;
using Eventos.Application;
using Eventos.Application.Contratos;
using Eventos.Application.Helpers;
using Eventos.Persistence;
using Eventos.Persistence.Contratos;
using Eventos.Persistence.Persists;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

// BUILDER
var builder = WebApplication.CreateBuilder();

builder.Services.AddControllers().AddJsonOptions(options =>
{
	//options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
}).AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<EventosContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(ProEventosProfile).Assembly);

builder.Services.AddScoped<ILotesService, LoteService>();
builder.Services.AddScoped<IEventosService, EventoService>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();

builder.Services.AddScoped<ILotePersist, LotePersist>();
builder.Services.AddScoped<IEventoPersist, EventoPersist>();

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
	});
}
else
{
	app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthorization();

app.UseCors(cors => cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
           );

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
	
app.Run();