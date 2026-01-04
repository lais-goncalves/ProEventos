using Eventos.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

// BUILDER
var builder = WebApplication.CreateBuilder();

builder.Services.AddControllers();

builder.Services.AddCors(options => { });

builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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