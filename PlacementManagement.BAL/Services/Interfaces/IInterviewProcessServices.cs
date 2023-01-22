using PlacementManagement.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IInterviewProcessServices
    {
        List<InteviewProcessViewModel> GetInterviewProcessByPlacementRequestId(int placementRequestId);
        bool AddCandidateForInterviewProcess(int placementRequestId, List<StudentViewModel> studentList);
    }
}
