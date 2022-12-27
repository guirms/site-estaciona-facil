using Domain.Models;
using Infra.Data.Mappings.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context;

public class ConfigContext: DbContext
{
    public ConfigContext(DbContextOptions<ConfigContext> option): base(option)
    {
    }
    
    public DbSet<Usuario>? Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        
        base.OnModelCreating(modelBuilder);
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //
    //     modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigContext).Assembly);
    // }
}