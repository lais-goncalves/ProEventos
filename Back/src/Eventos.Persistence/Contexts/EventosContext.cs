using Eventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence;

public class EventosContext : DbContext
{
	public DbSet<Evento> Eventos { get; set; }
	public DbSet<Lote> Lotes { get; set; }
	public DbSet<Palestrante> Palestrantes { get; set; }
	public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
	public DbSet<RedeSocial> RedesSociais { get; set; }

	public EventosContext(DbContextOptions<EventosContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// define chaves estrangeiras da classe,
		// relacionando os eventos (com base no nome "EventoId") com os palestrantes (com base no nome "PalestranteId")
		// Obs.: ele identifica automaticamente as colunas relacionadas com base no nome
		modelBuilder.Entity<PalestranteEvento>().HasKey(pe => new { pe.EventoId, pe.PalestranteId });

		modelBuilder.Entity<Evento>()
		            .HasMany(e => e.RedesSociais)
		            .WithOne(rs => rs.Evento)
		            .OnDelete(DeleteBehavior.Cascade);
		
		modelBuilder.Entity<Palestrante>()
		            .HasMany(p => p.RedeSociais)
		            .WithOne(rs => rs.Palestrante)
		            .OnDelete(DeleteBehavior.Cascade);
	}
}