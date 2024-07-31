using BlazorAppAlexis.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppAlexis.Server;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Meta> Metas { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meta>().HasMany(m => m.Tareas).WithOne(t => t.Meta).HasForeignKey(t => t.MetaId);
        
        // Seeding data for Meta
        modelBuilder.Entity<Meta>().HasData(
            new Meta { Id = 1, Nombre = "Meta 1", FechaCreada = DateTime.Now, PorcentajeCumplimiento = 0 },
            new Meta { Id = 2, Nombre = "Meta 2", FechaCreada = DateTime.Now, PorcentajeCumplimiento = 0 }
        );

        // Seeding data for Tarea
        modelBuilder.Entity<Tarea>().HasData(
            new Tarea { Id = 1, MetaId = 1, Nombre = "Tarea 1.1", FechaCreada = DateTime.Now, Estado = "Abierta", EsImportante = false },
            new Tarea { Id = 2, MetaId = 1, Nombre = "Tarea 1.2", FechaCreada = DateTime.Now, Estado = "Abierta", EsImportante = true },
            new Tarea { Id = 3, MetaId = 2, Nombre = "Tarea 2.1", FechaCreada = DateTime.Now, Estado = "Abierta", EsImportante = false }
        );

        base.OnModelCreating(modelBuilder);
   
        
    }
}