using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings.Usuario;

public class UsuarioMapping: IEntityTypeConfiguration<Domain.Models.Usuario>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(x => x.UsuarioId);

        builder.Property(x => x.UsuarioId)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Senha)
            .IsRequired()
            .HasMaxLength(200);
    }
}