using Eventos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Api.Data;

public class DataContext : DbContext
{
	public DbSet<Evento> Eventos { get; set; }

	public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}