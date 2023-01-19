namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IUnitOfWork:IDisposable
    {
        IEmployeeRepository Employee { get; }
        IProductRepository Product { get; }
        int Save();
    }
}
