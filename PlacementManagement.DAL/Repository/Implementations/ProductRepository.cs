using PlacementManagement.DAL;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PlacementManagementAppDbContext context) : base(context) { }
    }
}
