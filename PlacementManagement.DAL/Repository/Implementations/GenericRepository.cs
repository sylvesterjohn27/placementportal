using PlacementManagement.DAL;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PlacementManagementAppDbContext _context;
        internal DbSet<T> _dbset;

        public GenericRepository(PlacementManagementAppDbContext context)
        {
            this._context = context;
            _dbset = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {            
            return _context.Set<T>().ToList();
        }

        public T GetById(object Id)
        {
            return _context.Set<T>().Find(Id);
        }

        public void Insert(T obj)
        {            
            _context.Set<T>().Add(obj);         
        }

        public void Update(T obj)
        {
            _context.Set<T>().Update(obj);            
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);            
        }
    }
}
