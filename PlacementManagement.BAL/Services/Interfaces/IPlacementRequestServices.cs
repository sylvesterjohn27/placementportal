﻿using PlacementManagement.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
