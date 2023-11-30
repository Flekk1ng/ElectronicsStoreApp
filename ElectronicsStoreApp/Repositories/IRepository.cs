using System.Collections.Generic;

namespace ElectronicsStoreApp.Repositories
{
    public interface IRepository<T>
    {
        int Create(T entity);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
        void UpdateById(T entity);
    }
}
