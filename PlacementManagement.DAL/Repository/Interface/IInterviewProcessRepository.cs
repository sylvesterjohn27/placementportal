using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IInterviewProcessRepository
    {
        List<InterviewProcess> GetInterviewProcessByPlacementRequestId(int placementRequestId);
        void AddCandidateForInterviewProcess(InterviewProcess interviewProcess);
    }
}