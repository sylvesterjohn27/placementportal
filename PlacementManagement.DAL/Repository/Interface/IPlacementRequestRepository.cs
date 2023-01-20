using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
