using PlacementManagement.BAL.Models;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IPlacementRequestServices
    {
        List<PlacementRequestViewModel> GetPlacementRequestsByCompanyOrCollegeId(int companyOrCollegeId, int accountTypeId);
        PlacementRequestViewModel GetPlacementRequestById(int id);
        void AddorEditPlacementRequest(PlacementRequestViewModel placementRequest);
        bool DeletePlacementRequest(int Id);
    }
}
