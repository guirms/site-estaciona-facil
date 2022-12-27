namespace Infra.Data.Interfaces;

public interface IBaseRepository<T>: IDisposable where T: class
{
    int Add(T objeto);
    int Update(T objeto);
    T? GetById(int id);
    IList<T> GetAll();
    void Delete(int id);
}