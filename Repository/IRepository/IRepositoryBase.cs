using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        // Retrieve all entities of type T
        IEnumerable<T> GetAll();

        // Retrieve a single entity of type T by its primary key
        T GetById(int id);

        // Add a new entity of type T
        void AddRange(IEnumerable<T> entity);

        // Update an existing entity of type T
        void Update(T entity);

        // Delete an entity of type T
        void Delete(T entity);

        // Save changes to the underlying data store
        void SaveChanges();
        Task<List<T>> GetByUniqueId(int uniqueId);
    }
}
