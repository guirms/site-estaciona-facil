using Domain.Models;
using Infra.Data.Context;
using Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class UsuarioRepository: BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ConfigContext contexto) : base(contexto)
    {
    }
    
    public int SalvarUsuario(Usuario lUsuario)
    {
        if (lUsuario.UsuarioId != 0)
           return Update(lUsuario);
        
        return Add(lUsuario);
    }

    public int? ConsultarUsuarioIdPorEmailESenha(string email, string senha)
    {
        return Contexto.Set<Usuario>().AsNoTracking().FirstOrDefault(u => u.Email == email && u.Senha == senha)?.UsuarioId;
    }
}