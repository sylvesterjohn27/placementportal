using PlacementManagement.BAL.Models;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IPlacementRequestServices
    {
        List<PlacementRequestViewModel> GetPlacementRequests();
        PlacementRequestViewModel GetPlacementRequestById(int id);
        void AddorEditPlacementRequest(PlacementRequestViewModel placementRequest);
        bool DeletePlacementRequest(int Id);
    }
}
