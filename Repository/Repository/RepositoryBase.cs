using Core.Data.DataContext;
using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        int UId = 0;
        public RepositoryBase(ApplicationDbContext context) { _context = context; this.dbSet = _context.Set<T>(); }
        public void AddRange(IEnumerable<T> entity)
        {
             _context.Set<T>().AddRange(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public Task<List<T>> GetByUniqueId(int uniqueId)
        {
            throw new NotImplementedException();
        }

        //public async Task< List<T>> GetByUniqueId(int uniqueId) where T : class
        //{
        //    return await _context.Set<T>().Where(x => x.GetType().GetProperty("UId")?.GetValue(x)?.Equals(uniqueId) ?? false).ToListAsync();

        //}

        public void SaveChanges()
        {
            _context.SaveChanges(); 
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
