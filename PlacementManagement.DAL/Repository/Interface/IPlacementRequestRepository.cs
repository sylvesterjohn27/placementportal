using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IPlacementRequestRepository
    {
        List<PlacementRequest> GetPlacementRequestsByCompanyOrCollegeId(int companyOrCollegeId, int accountTypeId);
        PlacementRequest GetPlacementRequestById(int id);
        void AddorEditPlacementRequest(PlacementRequest placementRequest);
        void DeletePlacementRequest(PlacementRequest placementRequest);
    }
}
