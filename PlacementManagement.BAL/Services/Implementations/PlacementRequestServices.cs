using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.BAL.Services.Implementations
{
    public class PlacementRequestServices : IPlacementRequestServices
    {
        private readonly IPlacementRequestRepository _placementRequestRepository;

        public PlacementRequestServices(IPlacementRequestRepository placementRequestRepository)
        {
            _placementRequestRepository = placementRequestRepository;
        }

        public List<PlacementRequestViewModel> GetPlacementRequestsByCompanyOrCollegeId(int companyOrCollegeId, int accountTypeId)
        {
            var placementRequests = _placementRequestRepository.GetPlacementRequestsByCompanyOrCollegeId(companyOrCollegeId, accountTypeId);
            var placementRequestList = new List<PlacementRequestViewModel>();
            foreach (var placement in placementRequests)
            {
                var placementRequest = new PlacementRequestViewModel()
                {
                    Id = placement.Id,
                    PlacementDate = placement.PlacementDate,
                    CollegeId = placement.CollegeId,
                    Departments = placement.Departments,
                    CoreAreas = placement.CoreAreas,
                    CGPA = placement.CGPA,
                    CompanyId = placement.CompanyId,
                    IsApprovedByCollege = placement.IsApprovedByCollege
                };
                placementRequestList.Add(placementRequest);
            }
            return placementRequestList;
        }

        public PlacementRequestViewModel GetPlacementRequestById(int id)
        {
            var placementRequest = _placementRequestRepository.GetPlacementRequestById(id);
            if (placementRequest != null)
            {
                var placement = new PlacementRequestViewModel()
                {
                    Id = placementRequest.Id,
                    PlacementDate = placementRequest.PlacementDate,
                    CollegeId = placementRequest.CollegeId,
                    Departments = placementRequest.Departments,
                    CoreAreas = placementRequest.CoreAreas,
                    CGPA = placementRequest.CGPA,
                    CompanyId = placementRequest.CompanyId
                };
                return placement;
            }
            return null;
        }

        public void AddorEditPlacementRequest(PlacementRequestViewModel placementRequest)
        {
            var placement = new PlacementRequest
            {
                Id = placementRequest.Id,
                PlacementDate = placementRequest.PlacementDate,
                CollegeId = placementRequest.CollegeId,
                Departments = placementRequest.Departments,
                CoreAreas = placementRequest.CoreAreas,
                CGPA = placementRequest.CGPA,
                CompanyId= placementRequest.CompanyId
            };
            _placementRequestRepository.AddorEditPlacementRequest(placement);
        }

        public bool DeletePlacementRequest(int id)
        {
            var placement = _placementRequestRepository.GetPlacementRequestById(id);
            if (placement != null)
            {
                _placementRequestRepository.DeletePlacementRequest(placement);
                return true;
            }
            return false;
        }

        public void Approve_RejectPlacementRequest(PlacementRequestViewModel placementRequest)
        {
            var placement = new PlacementRequest
            {
                Id = placementRequest.Id,               
                IsApprovedByCollege = placementRequest.IsApprovedByCollege
            };
            _placementRequestRepository.Approve_RejectPlacementRequest(placement);
        }
    }
}
