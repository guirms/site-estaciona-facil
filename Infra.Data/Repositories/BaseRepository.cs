using Infra.Data.Context;
using Infra.Data.Interfaces;

namespace Infra.Data.Repositories;

// Se precisar sobreescrever algum método, utilizar este como "virtual"
public class BaseRepository<T>: IBaseRepository<T> where T : class
{
    protected readonly ConfigContext Contexto;

    public BaseRepository(ConfigContext contexto)
    {
        Contexto = contexto;
    }

    public int Add(T objeto)
    {
        Contexto.Set<T>().Add(objeto);
        return Contexto.SaveChanges();
    }

    public int Update(T objeto)
    {
        Contexto.Set<T>().Update(objeto);
        return Contexto.SaveChanges();
    }

    public T? GetById(int id)
    {
        return Contexto.Set<T>().Find(id);
    }

    public IList<T> GetAll()
    {
        return Contexto.Set<T>().ToList();
    }

    public void Delete(int id)
    {
        var objeto = GetById(id);
        if (objeto != null) Contexto.Set<T>().Remove(objeto);
        Contexto.SaveChanges();        }
        
    public void Dispose()
    {
        Contexto.Dispose();
    }
}