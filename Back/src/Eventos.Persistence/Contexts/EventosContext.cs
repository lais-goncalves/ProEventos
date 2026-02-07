using Eventos.Domain;
using Eventos.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence;

public class EventosContext : IdentityDbContext<
	Usuario, 
	Papel, 
	int, 
	IdentityUserClaim<int>, 
	PapelUsuario, 
	IdentityUserLogin<int>, 
	IdentityRoleClaim<int>, 
	IdentityUserToken<int>
>
{
	public DbSet<Evento> Eventos { get; set; }
	public DbSet<Lote> Lotes { get; set; }
	public DbSet<Palestrante> Palestrantes { get; set; }
	public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
	public DbSet<RedeSocial> RedesSociais { get; set; }

	public EventosContext(DbContextOptions<EventosContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<PapelUsuario>(pu =>
		{
			pu.HasKey(pu => new { pu.UserId, pu.RoleId });
			
			pu.HasOne(pu => pu.Papel)
			  .WithMany(p => p.PapeisUsuario)
			  .HasForeignKey(pu => pu.RoleId)
			  .IsRequired();
			
			pu.HasOne(pu => pu.Usuario)
			  .WithMany(p => p.PapeisUsuario)
			  .HasForeignKey(pu => pu.UserId)
			  .IsRequired();
		});
		
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