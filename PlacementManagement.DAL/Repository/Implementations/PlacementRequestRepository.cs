using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class PlacementRequestRepository : IPlacementRequestRepository
    {
        private readonly PlacementManagementAppDbContext _dbContext;

        public PlacementRequestRepository(PlacementManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PlacementRequest> GetPlacementRequests()
        {
            return _dbContext.PlacementRequest.ToList();
        }

        public PlacementRequest GetPlacementRequestById(int id)
        {
            return _dbContext.PlacementRequest.FirstOrDefault(c => c.Id == id);
        }

        public void AddorEditPlacementRequest(PlacementRequest placementRequest)
        {
            if (placementRequest.Id > 0)
                _dbContext.PlacementRequest.Update(placementRequest);
            else
                _dbContext.PlacementRequest.Add(placementRequest);
            _dbContext.SaveChanges();
        }

        public void DeletePlacementRequest(PlacementRequest placementRequest)
        {
            _dbContext.PlacementRequest.Remove(placementRequest);
            _dbContext.SaveChanges();
        }
    }
}
