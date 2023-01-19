using PlacementManagement.DAL;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Implementations;
using PlacementManagement.DAL.Repository.Interface;

namespace CRUDwithGenericRepository.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlacementManagementAppDbContext context;

        public UnitOfWork(PlacementManagementAppDbContext context)
        {
            this.context = context;
            Employee = new EmployeeRepository(this.context);
            Product = new ProductRepository(this.context);
        }

        public IEmployeeRepository Employee
        {
            get;
            private set;
        }

        public IProductRepository Product
        {
            get;
            private set;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
