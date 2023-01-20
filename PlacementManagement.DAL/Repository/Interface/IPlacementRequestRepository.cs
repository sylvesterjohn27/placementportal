using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IPlacementRequestRepository
    {
        List<PlacementRequest> GetPlacementRequests();
        PlacementRequest GetPlacementRequestById(int id);
        void AddorEditPlacementRequest(PlacementRequest placementRequest);
        void DeletePlacementRequest(PlacementRequest placementRequest);
    }
}
